namespace TestingProject.Tests
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestPlatform.ObjectModel;
    using NSubstitute;
    using NUnit.Framework;
    using ServerLibraryProject.Interfaces;
    using ServerLibraryProject.Models;
    using ServerLibraryProject.Services;

    /// <summary>
    /// Contains unit tests for the GroupService class.
    /// </summary>
    public class GroupServiceTests
    {
        private IGroupRepository groupRepository;
        private IUserRepository userRepository;
        private GroupService groupService;

        /// <summary>
        /// Sets up the test environment.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.groupRepository = Substitute.For<IGroupRepository>();
            this.userRepository = Substitute.For<IUserRepository>();
            this.groupService = new GroupService(this.groupRepository, this.userRepository);
        }

        /// <summary>
        /// Validates that the ValidateUpdate method throws an exception when the group does not exist.
        /// </summary>
        //[Test]
        //public void ValidateUpdate_GroupDoesNotExist_ThrowsArgumentException()
        //{
        //    // Arrange
        //    long groupId = 1;
        //    string name = "GroupName";
        //    string desc = "Description";
        //    string image = "Image";
        //    long adminId = 1;

        //    this.groupRepository.GetGroupById(groupId).Returns((Group)null);

        //    // Act & Assert
        //    var ex = Assert.Throws<ArgumentException>(() => this.groupService.UpdateGroup(groupId, name, desc, image, adminId));
        //    Assert.That(ex.Message, Is.EqualTo("Group with ID 1 does not exist (Parameter 'groupId')"));
        //}

        /// <summary>
        /// Validates that the ValidateUpdate method throws an exception when the user does not exist.
        /// </summary>
        //[Test]
        //public void ValidateUpdate_UserDoesNotExist_ThrowsArgumentException()
        //{
        //    // Arrange
        //    long groupId = 1;
        //    string name = "GroupName";
        //    string desc = "Description";
        //    string image = "Image";
        //    long adminId = 1;

        //    this.groupRepository.GetGroupById(groupId).Returns(new Group { Name = "GroupName", Image = "Image", Description = "Description", AdminId = 1 });
        //    this.userRepository.GetById(adminId).Returns((User)null);

        //    // Act & Assert
        //    var ex = Assert.Throws<ArgumentException>(() => this.groupService.UpdateGroup(groupId, name, desc, image, adminId));
        //    Assert.That(ex.Message, Is.EqualTo("User with ID 1 does not exist (Parameter 'adminUserId')"));
        //}

        /// <summary>
        /// Validates that the ValidateUpdate method throws an exception when the group name is empty.
        /// </summary>
        //[Test]
        //public void ValidateUpdate_GroupNameIsEmpty_ThrowsArgumentException()
        //{
        //    // Arrange
        //    long groupId = 1;
        //    string name = string.Empty;
        //    string desc = "Description";
        //    string image = "Image";
        //    long adminId = 1;

        //    this.groupRepository.GetGroupById(groupId).Returns(new Group { Name = "GroupName", Image = "Image", Description = "Description", AdminId = 1 });
        //    this.userRepository.GetById(adminId).Returns(new User { Username = "Username", Password = "PasswordHash", Image = "Image" });

        //    // Act & Assert
        //    var ex = Assert.Throws<ArgumentException>(() => this.groupService.UpdateGroup(groupId, name, desc, image, adminId));
        //    Assert.That(ex.Message, Is.EqualTo("Group name cannot be empty or whitespace (Parameter 'groupName')"));
        //}

        /// <summary>
        /// Validates that the ValidateUpdate method updates the group when provided with valid arguments.
        /// </summary>
        //[Test]
        //public void ValidateUpdate_ValidArguments_UpdatesGroup()
        //{
        //    // Arrange
        //    long groupId = 1;
        //    string name = "GroupName";
        //    string desc = "Description";
        //    string image = "Image";
        //    long adminId = 1;

        //    this.groupRepository.GetGroupById(groupId).Returns(new Group { Name = "GroupName", Image = "Image", Description = "Description", AdminId = 1 });
        //    this.userRepository.GetById(adminId).Returns(new User { Username = "Username", Password = "PasswordHash", Image = "Image" });

        //    // Act
        //    this.groupService.UpdateGroup(groupId, name, desc, image, adminId);

        //    // Assert
        //    this.groupRepository.Received(1).UpdateGroup(groupId, name, image, desc, adminId);
        //}

        /// <summary>
        /// Validates that the GetAll method returns all groups.
        /// </summary>
        [Test]
        public void GetAll_ReturnsAllGroups()
        {
            // Arrange
            var groups = new List<Group> { new Group { Name = "Group1", Description = "Description1" }, new Group { Name = "Group2", Description = "Description2" } };
            this.groupRepository.GetAllGroups().Returns(groups);

            // Act
            var result = this.groupService.GetAllGroups();

            // Assert
            Assert.That(result, Is.EqualTo(groups));
        }

        /// <summary>
        /// Validates that the GetById method returns the correct group.
        /// </summary>
        [Test]
        public void GetById_ReturnsCorrectGroup()
        {
            // Arrange
            long groupId = 1;
            var group = new Group { Id = groupId, Name = "GroupName", Description = "Description" };
            this.groupRepository.GetGroupById(groupId).Returns(group);

            // Act
            var result = this.groupService.GetGroupById(groupId);

            // Assert
            Assert.That(result, Is.EqualTo(group));
        }

        /// <summary>
        /// Validates that the GetUsersFromGroup method returns the correct users.
        /// </summary>
        [Test]
        public void GetUsersFromGroup_ReturnsCorrectUsers()
        {
            // Arrange
            long groupId = 1;
            var users = new List<User>
            {
                new User { Username = "User1", Password = "PasswordHash1", PhotoURL = "Image1" },
                new User { Username = "User2", Password = "PasswordHash2", PhotoURL = "Image2" }
            };
            this.groupRepository.GetGroupById(groupId).Returns(new Group { Id = groupId });
            this.groupRepository.GetUsersFromGroup(groupId).Returns(users);

            // Act
            var result = this.groupService.GetUsersFromGroup(groupId);

            // Assert
            Assert.That(result, Is.EqualTo(users));
        }

        /// <summary>
        /// Validates that the GetGroupsForUser method returns the correct groups.
        /// </summary>
        [Test]
        public void GetGroupsForUser_ReturnsCorrectGroups()
        {
            // Arrange
            long userId = 1;
            var groups = new List<Group>
            {
                new Group { Name = "Group1", Description = "Description1"},
                new Group { Name = "Group2", Description = "Description2"}
            };
            this.userRepository.GetById(userId).Returns(new User { Id = userId });
            this.groupRepository.GetGroupsForUser(userId).Returns(groups);

            // Act
            var result = this.groupService.GetUserGroups(userId);

            // Assert
            Assert.That(result, Is.EqualTo(groups));
        }

        /// <summary>
        /// Validates that AddGroup creates a group when provided with valid arguments.
        /// </summary>
        [Test]
        public void AddGroup_ValidArguments_CreatesGroup()
        {
            // Arrange
            string name = "NewGroup";
            string desc = "NewDescription";
            string image = "NewImage";
            long adminId = 1;
            var adminUser = new User { Id = adminId, Username = "Admin", Password = "PasswordHash", PhotoURL = "AdminImage" };
            this.userRepository.GetById(adminId).Returns(adminUser);

            // Act
            var result = this.groupService.AddGroup(name, desc);

            // Assert
            Assert.That(result.Name, Is.EqualTo(name));
            Assert.That(result.Description, Is.EqualTo(desc));
            this.groupRepository.Received(1).SaveGroup(Arg.Is<Group>(g => g.Name == name && g.Description == desc));
        }

        /// <summary>
        /// Validates that AddGroup throws an exception when the group name is empty.
        /// </summary>
        [Test]
        public void AddGroup_EmptyName_ThrowsArgumentException()
        {
            // Arrange
            string name = string.Empty;
            string desc = "Description";
            string image = "Image";
            long adminId = 1;

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => this.groupService.AddGroup(name, desc));
            Assert.That(ex.Message, Is.EqualTo("Group name cannot be empty"));
        }

        /// <summary>
        /// Validates that AddGroup throws an exception when the admin user does not exist.
        /// </summary>
        [Test]
        public void AddGroup_AdminDoesNotExist_ThrowsArgumentException()
        {
            // Arrange
            string name = "NewGroup";
            string desc = "Description";
            string image = "Image";
            long adminId = 1;
            this.userRepository.GetById(adminId).Returns((User)null);

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => this.groupService.AddGroup(name, desc));
            Assert.That(ex.Message, Is.EqualTo("User does not exist"));
        }
    }
}
