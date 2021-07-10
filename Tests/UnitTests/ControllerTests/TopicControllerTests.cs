using System;
using System.Threading.Tasks;
using BLL.DTOs;
using BLL.Interfaces;
using Moq;
using PL.Controllers;
using Xunit;

namespace Tests.UnitTests.ControllerTests
{
    public class TopicControllerTests
    {
        private readonly TopicController _topicController;
        private readonly Mock<ITopicService> _topicServiceMock;

        public TopicControllerTests()
        {
            _topicServiceMock = new Mock<ITopicService>();
            _topicController = new TopicController(_topicServiceMock.Object);
        }

        [Fact]
        public async Task GetTopic_ServiceInvoke()
        {
            //Act
            await _topicController.GetTopic(It.IsAny<Guid>());

            //Assert
            _topicServiceMock.Verify(s => s.GetTopic(It.IsAny<Guid>()));
        }

        [Fact]
        public async Task GetTopics_ServiceInvoke()
        {
            //Act
            await _topicController.GetTopics();

            //Assert
            _topicServiceMock.Verify(s => s.GetTopics());
        }

        [Fact]
        public async Task CreateTopic_ServiceInvoke()
        {
            //Act
            await _topicController.CreateTopic(It.IsAny<TopicDto>());

            //Assert
            _topicServiceMock.Verify(s => s.CreateTopic(It.IsAny<TopicDto>()));
        }

        [Fact]
        public async Task UpdateTopic_ServiceInvoke()
        {
            //Act
            await _topicController.UpdateTopic(It.IsAny<Guid>(), It.IsAny<TopicDto>());

            //Assert
            _topicServiceMock.Verify(s => s.UpdateTopic(It.IsAny<Guid>(), It.IsAny<TopicDto>()));
        }

        [Fact]
        public async Task DeleteTopic_ServiceInvoke()
        {
            //Act
            await _topicController.DeleteTopic(It.IsAny<Guid>());

            //Assert
            _topicServiceMock.Verify(s => s.DeleteTopic(It.IsAny<Guid>()));
        }

        [Fact]
        public async Task GetMessages_ServiceInvoke()
        {
            //Act
            await _topicController.GetMessages(It.IsAny<Guid>());

            //Arrange
            _topicServiceMock.Verify(s => s.GetMessages(It.IsAny<Guid>()));
        }
    }
}