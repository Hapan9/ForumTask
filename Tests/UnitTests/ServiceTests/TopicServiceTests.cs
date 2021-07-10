using System;
using System.Linq;
using System.Threading.Tasks;
using BLL.DTOs;
using BLL.Interfaces;
using BLL.Services;
using DAL.Interfaces;
using DAL.Models;
using Moq;
using Xunit;

namespace Tests.UnitTests.ServiceTests
{
    public class TopicServiceTests
    {
        private readonly FakeData _fakeData;
        private readonly Mock<IMessageRepository> _messageRepositoryMock;
        private readonly Mock<ITopicRepository> _topicRepositoryMock;
        private readonly ITopicService _topicService;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public TopicServiceTests()
        {
            _fakeData = new FakeData();

            var userRepositoryMock = new Mock<IUserRepository>();
            _topicRepositoryMock = new Mock<ITopicRepository>();
            _messageRepositoryMock = new Mock<IMessageRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            _unitOfWorkMock.Setup(uow => uow.Users)
                .Returns(userRepositoryMock.Object);
            _unitOfWorkMock.Setup(uow => uow.Topics)
                .Returns(_topicRepositoryMock.Object);
            _unitOfWorkMock.Setup(uow => uow.Messages)
                .Returns(_messageRepositoryMock.Object);

            var topic = _fakeData.Topics.Last();

            _topicRepositoryMock.Setup(r => r.Get(It.IsAny<Guid>()))
                .ReturnsAsync(topic);
            userRepositoryMock.Setup(r => r.Get(It.IsAny<Guid>()))
                .ReturnsAsync(_fakeData.Users.First(u => u.Id == topic.UserId));
            
            _topicService = new TopicService(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Get_RepositoryInvokes()
        {
            //Act
            await _topicService.GetTopic(It.IsAny<Guid>());

            //Assert
            _topicRepositoryMock.Verify(r => r.Get(It.IsAny<Guid>()));
            _unitOfWorkMock.Verify(uow => uow.Topics);
        }

        [Fact]
        public async Task GetAll_RepositoryInvokes()
        {
            //Act
            await _topicService.GetTopics();

            //Assert
            _topicRepositoryMock.Verify(r => r.GetAll());
            _unitOfWorkMock.Verify(uow => uow.Topics);
        }

        [Fact]
        public async Task Create_RepositoryInvokes()
        {
            //Arrange
            var topic = _fakeData.Topics.Last();

            var topicDto = new TopicDto
            {
                Name = topic.Name,
                UserId = topic.UserId
            };

            //Act
            await _topicService.CreateTopic(topicDto);

            //Assert
            _topicRepositoryMock.Verify(r => r.Create(It.IsAny<Topic>()));
            _unitOfWorkMock.Verify(uow => uow.Users);
            _unitOfWorkMock.Verify(uow => uow.Topics);
        }

        [Fact]
        public async Task Update_RepositoryInvokes()
        {
            //Arrange
            var topic = _fakeData.Topics.Last();

            var topicDto = new TopicDto
            {
                Name = topic.Name,
                UserId = topic.UserId
            };

            var topicId = topic.Id;

            //Act
            await _topicService.UpdateTopic(topicId, topicDto);

            //Assert
            _topicRepositoryMock.Verify(r => r.Update(It.IsAny<Topic>()));
            _unitOfWorkMock.Verify(uow => uow.Users);
            _unitOfWorkMock.Verify(uow => uow.Topics);
        }

        [Fact]
        public async Task Delete_RepositoryInvokes()
        {
            //Act
            await _topicService.DeleteTopic(It.IsAny<Guid>());

            //Assert
            _topicRepositoryMock.Verify(r => r.Delete(It.IsAny<Guid>()));
            _unitOfWorkMock.Verify(uow => uow.Topics);
        }

        [Fact]
        public async Task GetMessages_RepositoryInvokes()
        {
            //Act
            await _topicService.GetMessages(It.IsAny<Guid>());

            //Assert
            _messageRepositoryMock.Verify(r => r.GetAll());
            _topicRepositoryMock.Verify(r => r.Get(It.IsAny<Guid>()));
            _unitOfWorkMock.Verify(uow => uow.Topics);
            _unitOfWorkMock.Verify(uow => uow.Messages);
        }
    }
}