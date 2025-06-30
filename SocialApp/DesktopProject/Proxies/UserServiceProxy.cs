namespace DesktopProject.Proxies
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Net.Http;
    using System.Net.Http.Json;
    using ServerLibraryProject.Interfaces;
    using ServerLibraryProject.Models;

    /// <summary>
    /// A proxy that communicates with the user microservice and implements IUserService.
    /// Provides user data handling and relationship management.
    /// </summary>
    public class UserServiceProxy : IUserService
    {
        private readonly HttpClient httpClient;

        public UserServiceProxy()
        {
            this.httpClient = new HttpClient();
            this.httpClient.BaseAddress = new Uri("https://localhost:7106/api/users/");
        }

        public List<User> GetAllUsers()
        {
            var response = this.httpClient.GetAsync(string.Empty).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadFromJsonAsync<List<User>>().Result ?? new List<User>();
            }

            Debug.WriteLine($"Failed to get users. Status: {response.StatusCode}");
            return new List<User>();
        }


        public User GetById(long id)
        {
            var response = this.httpClient.GetAsync($"{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadFromJsonAsync<User>().Result;
            }

            Debug.WriteLine($"User not found by ID {id}. Status: {response.StatusCode}");
            return null;
        }

        public User GetUserByUsername(string username)
        {
            var response = this.httpClient.GetAsync($"user?username={username}").Result;
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                if (!string.IsNullOrWhiteSpace(content))
                {
                    var user = response.Content.ReadFromJsonAsync<User>().Result;
                    return user;
                }
            }

            return null;
        }

        public List<User> SearchUsersByUsername(long userId, string query)
        {
            return [];
        }

        //public List<User> GetUserFollowers(long id)
        //{
        //    var response = this.httpClient.GetAsync($"users/{id}/followers").Result;
        //    if (response.IsSuccessStatusCode)
        //    {
        //        return response.Content.ReadFromJsonAsync<List<User>>().Result ?? new List<User>();
        //    }

        //    Debug.WriteLine($"Failed to get followers for user {id}. Status: {response.StatusCode}");
        //    return new List<User>();
        //}

        public List<User> GetUserFollowing(long id)
        {
            var response = this.httpClient.GetAsync($"{id}/following").Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadFromJsonAsync<List<User>>().Result ?? new List<User>();
            }

            Debug.WriteLine($"Failed to get following for user {id}. Status: {response.StatusCode}");
            return new List<User>();
        }



        public List<User> SearchUsersById(long userId, string query)
        {
            // this endpoint doesn't even exist
            throw new Exception();
        }
        public void FollowUserById(long userId, long whoToFollowId)
        {
            var response = this.httpClient.PostAsJsonAsync($"{userId}/followers", whoToFollowId).Result;
            if (!response.IsSuccessStatusCode)
            {
                Debug.WriteLine($"Failed to follow. Status: {response.StatusCode}");
            }
        }

        public void UnfollowUserById(long userId, long whoToUnfollowId)
        {
            var response = this.httpClient.DeleteAsync($"{userId}/followers/{whoToUnfollowId}").Result;
            if (!response.IsSuccessStatusCode)
            {
                Debug.WriteLine($"Failed to unfollow. Status: {response.StatusCode}");
            }
        }

        public long AddUser(string username, string password, string image)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentException("Username cannot be empty", nameof(username));
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Password cannot be empty", nameof(password));
            }

            var user = new User
            {
                Username = username,
                Password = password,
                PhotoURL = image,
            };

            var response = this.httpClient.PostAsJsonAsync(string.Empty, user).Result;
            if (!response.IsSuccessStatusCode)
            {
                Debug.WriteLine($"Failed to add user. Status: {response.StatusCode}");
            }

            return -1;
        }

        public void JoinGroup(long userId, long groupId)
        {
            var client = new HttpClient();

            var response = client.PostAsJsonAsync($"https://localhost:7106/api/users/{userId}/groups/{groupId}", "").Result;

            if (response.IsSuccessStatusCode)
            {
                return;              
            }

            throw new Exception($"Failed to join group: {response.StatusCode}");
        }

        public void ExitGroup(long userId, long groupId)
        {
            var client = new HttpClient();

            var response = client.DeleteAsync($"https://localhost:7106/api/users/{userId}/groups/{groupId}").Result;

            if (response.IsSuccessStatusCode)
            {
                return;
            }

            throw new Exception($"Failed to exit group: {response.StatusCode}");
        }
    }
}
