namespace TestingProject.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using NSubstitute;
    using ServerLibraryProject.Interfaces;
    using ServerLibraryProject.Models;
    using ServerLibraryProject.Services;

    /// <summary>
    /// Contains unit tests for the UserService class.
    /// </summary>
    public class UserServiceTests
    {
        /// <summary>
        /// Validates that the AddUser method successfully creates a user when provided with valid arguments.
        /// </summary>
        [Test]
        public void ValidateAddUser_WithValidArguments_CreatesUser()
        {
            // Arrange
            var userRepository = Substitute.For<IUserRepository>();
            var userService = new UserService(userRepository);

            string username = "testuser";
            string email = "testuser@gmail.com";
            string password = "password123";
            string image = "testimage.png";

            var expectedUser = new User { Username = username, Password = password };
            userRepository.Save(Arg.Any<User>()).Returns(expectedUser);

            // Act
            var result = userService.AddUser(username, password, image);

            // Assert
            userRepository.Received(1).Save(Arg.Is<User>(u =>
                u.Username == username &&
                u.Password == password));
            Assert.That(result, Is.EqualTo(expectedUser.Id));
        }

        /// <summary>
        /// Validates that the AddUser method throws an exception when provided with an empty username.
        /// </summary>
        [Test]
        public void AddUser_WithEmptyUsername_ThrowsException()
        {
            // Arrange
            var userRepository = Substitute.For<IUserRepository>();
            var userService = new UserService(userRepository);

            // Act & Assert
            var ex = Assert.Throws<Exception>(() =>
                userService.AddUser(string.Empty, "password", "image"));
            Assert.That(ex.Message, Is.EqualTo("Username cannot be empty"));

            userRepository.DidNotReceive().Save(Arg.Any<User>());
        }

        /// <summary>
        /// Validates that the AddUser method throws an exception when provided with an empty email.
        /// </summary>
        //[Test]
        //public void AddUser_WithEmptyEmail_ThrowsException()
        //{
        //    // Arrange
        //    var userRepository = Substitute.For<IUserRepository>();
        //    var userService = new UserService(userRepository);

        //    // Act & Assert
        //    var ex = Assert.Throws<Exception>(() =>
        //        userService.AddUser("validuser", "password", "image"));
        //    Assert.That(ex.Message, Is.EqualTo("Email cannot be empty"));

        //    userRepository.DidNotReceive().Save(Arg.Any<User>());
        //}

        /// <summary>
        /// Validates that the AddUser method throws an exception when provided with an empty password.
        /// </summary>
        [Test]
        public void AddUser_WithEmptyPassword_ThrowsException()
        {
            // Arrange
            var userRepository = Substitute.For<IUserRepository>();
            var userService = new UserService(userRepository);

            // Act & Assert
            var ex = Assert.Throws<Exception>(() =>
                userService.AddUser("validuser", string.Empty, "image"));
            Assert.That(ex.Message, Is.EqualTo("Password cannot be empty"));

            userRepository.DidNotReceive().Save(Arg.Any<User>());
        }

        [Test]
        public void AddUser_WithNullUsername_ThrowsException()
        {
            // Arrange
            var userRepository = Substitute.For<IUserRepository>();
            var userService = new UserService(userRepository);

            // Act & Assert
            var ex = Assert.Throws<Exception>(() =>
                userService.AddUser(null, "password", "image"));
            Assert.That(ex.Message, Is.EqualTo("Username cannot be empty"));

            userRepository.DidNotReceive().Save(Arg.Any<User>());
        }

        //[Test]
        //public void AddUser_WithNullEmail_ThrowsException()
        //{
        //    // Arrange
        //    var userRepository = Substitute.For<IUserRepository>();
        //    var userService = new UserService(userRepository);

        //    // Act & Assert
        //    var ex = Assert.Throws<Exception>(() =>
        //        userService.AddUser("validuser", "password", "image"));
        //    Assert.That(ex.Message, Is.EqualTo("Email cannot be empty"));

        //    userRepository.DidNotReceive().Save(Arg.Any<User>());
        //}

        [Test]
        public void AddUser_WithNullPassword_ThrowsException()
        {
            // Arrange
            var userRepository = Substitute.For<IUserRepository>();
            var userService = new UserService(userRepository);

            // Act & Assert
            var ex = Assert.Throws<Exception>(() =>
                userService.AddUser("validuser", null, "image"));
            Assert.That(ex.Message, Is.EqualTo("Password cannot be empty"));

            userRepository.DidNotReceive().Save(Arg.Any<User>());
        }

        [Test]
        public void FollowUser_ValidUsers_Success()
        {
            // Arrange
            var userRepository = Substitute.For<IUserRepository>();
            var userService = new UserService(userRepository);
            var followerId = 1;
            var followedId = 2;

            userRepository.GetById(followerId).Returns(new User { Id = followerId });
            userRepository.GetById(followedId).Returns(new User { Id = followedId });

            // Act & Assert
            Assert.DoesNotThrow(() => userService.FollowUserById(followerId, followedId));
            userRepository.Received(1).Follow(followerId, followedId);
        }

        [Test]
        public void FollowUser_FollowerDoesNotExist_ThrowsException()
        {
            // Arrange
            var userRepository = Substitute.For<IUserRepository>();
            var userService = new UserService(userRepository);
            var nonExistentFollowerId = 1;
            var followedId = 2;

            userRepository.GetById(nonExistentFollowerId).Returns((User)null);
            userRepository.GetById(followedId).Returns(new User { Id = followedId });

            // Act & Assert
            var ex = Assert.Throws<Exception>(() => userService.FollowUserById(nonExistentFollowerId, followedId));
            Assert.That(ex.Message, Is.EqualTo("User does not exist"));
            userRepository.DidNotReceive().Follow(Arg.Any<long>(), Arg.Any<long>());
        }
      
      
      [Test]
        public void FollowUser_FollowedDoesNotExist_ThrowsException()
        {
            // Arrange
            var userRepository = Substitute.For<IUserRepository>();
            var userService = new UserService(userRepository);
            var followerId = 1;
            var nonExistentFollowedId = 2;

            userRepository.GetById(followerId).Returns(new User { Id = followerId });
            userRepository.GetById(nonExistentFollowedId).Returns((User)null);

            // Act & Assert
            var ex = Assert.Throws<Exception>(() => userService.FollowUserById(followerId, nonExistentFollowedId));
            Assert.That(ex.Message, Is.EqualTo("User to follow does not exist"));
            userRepository.DidNotReceive().Follow(Arg.Any<long>(), Arg.Any<long>());
        }

        [Test]
        public void UnfollowUser_ValidUsers_Success()
        {
            // Arrange
            var userRepository = Substitute.For<IUserRepository>();
            var userService = new UserService(userRepository);
            var followerId = 1;
            var followedId = 2;

            userRepository.GetById(followerId).Returns(new User { Id = followerId });
            userRepository.GetById(followedId).Returns(new User { Id = followedId });

            // Act & Assert
            Assert.DoesNotThrow(() => userService.UnfollowUserById(followerId, followedId));
            userRepository.Received(1).Unfollow(followerId, followedId);
        }
      
      
      
        [Test]
        public void UnfollowUser_FollowerDoesNotExist_ThrowsException()
        {
            // Arrange
            var userRepository = Substitute.For<IUserRepository>();
            var userService = new UserService(userRepository);
            var nonExistentFollowerId = 1;
            var followedId = 2;

            userRepository.GetById(nonExistentFollowerId).Returns((User)null);
            userRepository.GetById(followedId).Returns(new User { Id = followedId });

            // Act & Assert
            var ex = Assert.Throws<Exception>(() => userService.UnfollowUserById(nonExistentFollowerId, followedId));
            Assert.That(ex.Message, Is.EqualTo("User does not exist"));
            userRepository.DidNotReceive().Unfollow(Arg.Any<long>(), Arg.Any<long>());
        }

        [Test]
        public void UnfollowUser_FollowedDoesNotExist_ThrowsException()
        {
            // Arrange
            var userRepository = Substitute.For<IUserRepository>();
            var userService = new UserService(userRepository);
            var followerId = 1;
            var nonExistentFollowedId = 2;

            userRepository.GetById(followerId).Returns(new User { Id = followerId });
            userRepository.GetById(nonExistentFollowedId).Returns((User)null);

            // Act & Assert
            var ex = Assert.Throws<Exception>(() => userService.UnfollowUserById(followerId, nonExistentFollowedId));
            Assert.That(ex.Message, Is.EqualTo("User to unfollow does not exist"));
            userRepository.DidNotReceive().Unfollow(Arg.Any<long>(), Arg.Any<long>());
        }

        [Test]
        public void SearchUsersByUsername_ReturnsMatchingUsers()
        {
            // Arrange
            var userRepository = Substitute.For<IUserRepository>();
            var userService = new UserService(userRepository);
            var userId = 1;
            var query = "test";

            var followingUsers = new List<User>
            {
                new User { Id = 2, Username = "testuser1" },
                new User { Id = 3, Username = "testuser2" },
                new User { Id = 4, Username = "otheruser" }
            };

            userRepository.GetUserFollowing(userId).Returns(followingUsers);

            // Act
            var result = userService.SearchUsersByUsername(userId, query);

            // Assert
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.All(u => u.Username.Contains(query, StringComparison.OrdinalIgnoreCase)));
        }

        //[Test]
        //public void Login_ValidCredentials_ReturnsUserId()
        //{
        //    // Arrange
        //    var userRepository = Substitute.For<IUserRepository>();
        //    var userService = new UserService(userRepository);
        //    var username = "testuser";
        //    var password = "password123";
        //    var userId = 1;

        //    userRepository.GetByUsername(username).Returns(new User { Id = userId, Username = username, Password = password });

        //    // Act
        //    var result = userService.Login(username, password);

        //    // Assert
        //    Assert.That(result, Is.EqualTo(userId));
        //}

        //[Test]
        //public void Login_InvalidPassword_ReturnsMinusOne()
        //{
        //    // Arrange
        //    var userRepository = Substitute.For<IUserRepository>();
        //    var userService = new UserService(userRepository);
        //    var username = "testuser";
        //    var password = "password123";

        //    userRepository.GetByUsername(username).Returns(new User { Id = 1, Username = username, Password = "wrongpassword" });

        //    // Act
        //    var result = userService.Login(username, password);

        //    // Assert
        //    Assert.That(result, Is.EqualTo(-1));
        //}

        //[Test]
        //public void Login_UserNotFound_ReturnsMinusTwo()
        //{
        //    // Arrange
        //    var userRepository = Substitute.For<IUserRepository>();
        //    var userService = new UserService(userRepository);
        //    var username = "nonexistentuser";
        //    var password = "password123";

        //    userRepository.GetByUsername(username).Returns((User)null);

        //    // Act
        //    var result = userService.Login(username, password);

        //    // Assert
        //    Assert.That(result, Is.EqualTo(-2));
        //}
    }
}
