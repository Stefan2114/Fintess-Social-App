namespace TestingProject.Tests
{
    using System;
    using System.Collections.Generic;
    using NSubstitute;
    using NUnit.Framework;
    using ServerLibraryProject.Enums;
    using ServerLibraryProject.Interfaces;
    using ServerLibraryProject.Models;
    using ServerLibraryProject.Services;

    /// <summary>
    /// Contains unit tests for the ReactionService class.
    /// </summary>
    public class ReactionServiceTests
    {
        private IReactionRepository reactionRepository;
        private ReactionService reactionService;

        /// <summary>
        /// Sets up the test environment.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.reactionRepository = Substitute.For<IReactionRepository>();
            this.reactionService = new ReactionService(this.reactionRepository);
        }

        /// <summary>
        /// Validates that AddReaction_byUserAndPost updates the reaction if one already exists.
        /// </summary>
        [Test]
        public void AddReaction_byUserAndPost_ReactionExists_UpdatesReaction()
        {
            // Arrange
            long userId = 1;
            long postId = 10;
            ReactionType newType = ReactionType.Like;
            var reaction = new Reaction { UserId = userId, PostId = postId, Type = newType };


            var existingReaction = new Reaction { UserId = userId, PostId = postId, Type = ReactionType.Anger };
            this.reactionRepository.GetReaction(userId, postId).Returns(existingReaction);

            // Act
            this.reactionService.AddReaction(reaction);

            // Assert
            this.reactionRepository.Received(1).Update(userId, postId, newType);
            this.reactionRepository.Received(2).GetReaction(userId, postId); // once before, once after update
        }

        /// <summary>
        /// Validates that AddReaction_byUserAndPost adds a new reaction if one does not exist.
        /// </summary>
        [Test]
        public void AddReaction_byUserAndPost_ReactionDoesNotExist_CreatesReaction()
        {
            // Arrange
            long userId = 1;
            long postId = 10;
            ReactionType type = ReactionType.Love;
            var reaction = new Reaction { UserId = userId, PostId = postId, Type = type };

            this.reactionRepository.GetReaction(userId, postId).Returns(null as Reaction);

            // Act
            this.reactionService.AddReaction(reaction);

            // Assert
            this.reactionRepository.Received(1).Add(Arg.Is<Reaction>(r =>
                r.UserId == userId && r.PostId == postId && r.Type == type));

        }

        /// <summary>
        /// Validates that DeleteReaction_byUserAndPost throws an exception if the reaction does not exist.
        /// </summary>
        //[Test]
        //public void DeleteReaction_byUserAndPost_ReactionDoesNotExist_ThrowsException()
        //{
        //    // Arrange
        //    long userId = 1;
        //    long postId = 10;

        //    this.reactionRepository.GetReaction(userId, postId).Returns(null as Reaction);

        //    // Act & Assert
        //    var ex = Assert.Throws<Exception>(() => this.reactionService.DeleteReaction(userId, postId));
        //    Assert.That(ex.Message, Is.EqualTo("Reaction does not exist"));
        //}

        /// <summary>
        /// Validates that DeleteReaction_byUserAndPost deletes the reaction if it exists.
        /// </summary>
        //[Test]
        //public void DeleteReaction_byUserAndPost_ReactionExists_DeletesReaction()
        //{
        //    // Arrange
        //    long userId = 1;
        //    long postId = 10;

        //    this.reactionRepository.GetReaction(userId, postId).Returns(new Reaction { UserId = userId, PostId = postId });

        //    // Act
        //    this.reactionService.DeleteReaction(userId, postId);

        //    // Assert
        //    this.reactionRepository.Received(1).Delete(userId, postId);
        //}

        /// <summary>
        /// Validates that GetAllReactions returns all reactions.
        /// </summary>
        //[Test]
        //public void GetAllReactions_ReturnsAllReactions()
        //{
        //    // Arrange
        //    var reactions = new List<Reaction>
        //    {
        //        new Reaction { UserId = 1, PostId = 1, Type = ReactionType.Like },
        //        new Reaction { UserId = 2, PostId = 2, Type = ReactionType.Anger },
        //    };
        //    this.reactionRepository.GetAllReactions().Returns(reactions);

        //    // Act
        //    var result = this.reactionService.GetAllReactions();

        //    // Assert
        //    Assert.That(result, Is.EqualTo(reactions));
        //}

        /// <summary>
        /// Validates that GetReactionsForPost returns the correct reactions.
        /// </summary>
        [Test]
        public void GetReactionsForPost_ReturnsCorrectReactions()
        {
            // Arrange
            long postId = 1;
            var reactions = new List<Reaction>
            {
                new Reaction { UserId = 1, PostId = postId, Type = ReactionType.Like },
                new Reaction { UserId = 2, PostId = postId, Type = ReactionType.Love },
            };
            this.reactionRepository.GetReactionsByPostId(postId).Returns(reactions);

            // Act
            var result = this.reactionService.GetReactionsByPostId(postId);

            // Assert
            Assert.That(result, Is.EqualTo(reactions));
        }

        [Test]
        public void GetReaction_CallsRepositoryAndReturnsReaction()
        {
            // Arrange
            long userId = 1;
            long postId = 10;
            var expectedReaction = new Reaction { UserId = userId, PostId = postId, Type = ReactionType.Like };

            this.reactionRepository.GetReaction(userId, postId).Returns(expectedReaction);

            // Act
            var result = this.reactionService.GetReaction(userId, postId);

            // Assert
            Assert.AreEqual(expectedReaction, result);
            this.reactionRepository.Received(1).GetReaction(userId, postId);
        }
        //[Test]
        //public void UpdateReaction_CallsRepositoryWithCorrectValues()
        //{
        //    // Arrange
        //    var reaction = new Reaction
        //    {
        //        UserId = 1,
        //        PostId = 10,
        //        Type = ReactionType.Love
        //    };

        //    // Act
        //    this.reactionService.UpdateReaction(reaction);

        //    // Assert
        //    this.reactionRepository.Received(1).Update(reaction.UserId, reaction.PostId, reaction.Type);
        //}
        [Test]
        public void GetReaction_WhenNoReactionExists_ReturnsNull()
        {
            // Arrange
            this.reactionRepository.GetReaction(1, 10).Returns((Reaction)null);

            // Act
            var result = this.reactionService.GetReaction(1, 10);

            // Assert
            Assert.IsNull(result);
        }




    }
}
