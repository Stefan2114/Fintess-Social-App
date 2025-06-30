using ServerLibraryProject.Models;

namespace ServerLibraryProject.Interfaces
{
    public interface IUserService
    {
        void FollowUserById(long userId, long whoToFollowId);

        List<User> GetAllUsers();

        User GetById(long id);

        List<User> GetUserFollowing(long id);

        User GetUserByUsername(string username);

        List<User> SearchUsersByUsername(long userId, string query);

        void UnfollowUserById(long userId, long whoToUnfollowId);

        long AddUser(string username, string password, string image);

        void JoinGroup(long userId, long groupId);

        void ExitGroup(long userId, long groupId);
    }
}