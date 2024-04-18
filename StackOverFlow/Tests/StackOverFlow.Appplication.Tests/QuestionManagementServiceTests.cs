using Autofac.Extras.Moq;
using Moq;
using StackOverFlow.Application;
using StackOverFlow.Application.Features.Questions;
using StackOverFlow.Domain.Entities;
using StackOverFlow.Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace StackOverFlow.Appplication.Tests
{
    public class QuestionManagementServiceTests
    {
        private AutoMock _mock;
        private Mock<IQuestionRepository> _questionRepositoryMock;
        private Mock<ITagRepository> _tagRepositoryMock;
        private Mock<IAnswerRepository> _answerRepositoryMock;
        private Mock<IApplicationUnitOfWork> _unitOfWorkMock;
        private QuestionManagementService _questionManagementService;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _mock = AutoMock.GetLoose();
        }

        [SetUp]
        public void Setup()
        {
            _questionRepositoryMock = _mock.Mock<IQuestionRepository>();
            _tagRepositoryMock = _mock.Mock<ITagRepository>();
            _answerRepositoryMock = _mock.Mock<IAnswerRepository>();
            _unitOfWorkMock = _mock.Mock<IApplicationUnitOfWork>();
            _questionManagementService = _mock.Create<QuestionManagementService>();
        }

        [TearDown]
        public void TearDown()
        {
            _questionRepositoryMock?.Reset();
            _tagRepositoryMock?.Reset();
            _answerRepositoryMock?.Reset();
            _unitOfWorkMock?.Reset();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _mock?.Dispose();
        }

        [Test]
        public async Task CreateAnswerAsync_AnswerIncrease_CreateNewAnswer()
        {
            // Arrange
            var questionId = Guid.NewGuid();
            var answerBody = "<p>You Can Try This</p>";
            var UserId = Guid.NewGuid();
            var userEmail = "Safi20@gmail.com";

            var answer = new Answer
            {
                QuestionId = questionId,
                AnswerTime = DateTime.Now,
                AnsweredByUserId = UserId,
                AnsweredByCreatorEmail = userEmail,
                Body = answerBody,
            };

            var question = new Question
            {
                Id = questionId,
                AnswerCount = 0, 
                Answers = new List<Answer>()
            };

            _unitOfWorkMock.SetupGet(x => x.QuestionRepository).Returns(_questionRepositoryMock.Object).Verifiable();
            _questionRepositoryMock.Setup(x => x.GetAsync(questionId)).ReturnsAsync(question);
            _unitOfWorkMock.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask).Verifiable();
             
            // Act
            await _questionManagementService.CreateAnswerAsync(questionId, answerBody, UserId, userEmail);

            // Assert
            _unitOfWorkMock.VerifyAll();
            _questionRepositoryMock.VerifyAll();
            Assert.AreEqual(1, question.AnswerCount);
            Assert.AreEqual(1, question.Answers.Count);
        }


        [Test]
        public async Task CreateQuestionAsync_AddsQuestionToRepository()
        {
            // Arrange
            var title = "Test Question";
            var details = "Test details for the question.";
            var triedApproach = "Test approach for solving the problem.";
            var tags = new List<string> { "tag1", "tag2", "tag3" };
            var userId = Guid.NewGuid();
            var userEmail = "test@example.com";

            var allTags = new List<Tag>
            {
                new Tag { Id = Guid.NewGuid(), Name = "tag1",Description="ss" },
                new Tag { Id = Guid.NewGuid(), Name = "tag2",Description="sdadsdsad" },
                new Tag { Id = Guid.NewGuid(), Name = "tag3",Description= "sdsadsaqewqewqe" },
                new Tag { Id = Guid.NewGuid(), Name = "tag4",Description="qeweweqweqwe"} 
            };

            _unitOfWorkMock.Setup(u => u.TagRepository.GetAll()).Returns(allTags).Verifiable();

            _unitOfWorkMock.SetupGet(x => x.QuestionRepository).Returns(_questionRepositoryMock.Object).Verifiable();
            _questionRepositoryMock.Setup(r => r.AddAsync(It.IsAny<Question>())).Returns(Task.CompletedTask).Verifiable();

            _unitOfWorkMock.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask).Verifiable();

            // Act
            await _questionManagementService.CreateQuestionAsync(title, details, triedApproach, tags, userId, userEmail);

            // Assert
            _unitOfWorkMock.VerifyAll();
            _questionRepositoryMock.VerifyAll();
        }


    }
}
