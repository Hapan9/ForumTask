using System;
using System.Threading.Tasks;
using BLL.DTOs;
using BLL.Interfaces;
using Moq;
using PL.Controllers;
using Xunit;

namespace Tests.UnitTests.ControllerTests
{
    public class MessageControllerTests
    {
        private readonly MessageController _messageController;
        private readonly Mock<IMessageService> _messageServiceMock;

        public MessageControllerTests()
        {
            _messageServiceMock = new Mock<IMessageService>();
            _messageController = new MessageController(_messageServiceMock.Object);
        }

        [Fact]
        public async Task GetMessage_ServiceInvoke()
        {
            //Act
            await _messageController.GetMessage(It.IsAny<Guid>());

            //Assert
            _messageServiceMock.Verify(s => s.GetMessage(It.IsAny<Guid>()));
        }

        [Fact]
        public async Task GetMessages_ServiceInvoke()
        {
            //Act
            await _messageController.GetMessages();

            //Assert
            _messageServiceMock.Verify(s => s.GetMessages());
        }

        [Fact]
        public async Task CreateMessage_ServiceInvoke()
        {
            //Act
            await _messageController.CreateMessage(It.IsAny<MessageDto>());

            //Assert
            _messageServiceMock.Verify(s => s.CreateMessage(It.IsAny<MessageDto>()));
        }

        [Fact]
        public async Task UpdateMessage_ServiceInvoke()
        {
            //Act
            await _messageController.UpdateMessage(It.IsAny<Guid>(), It.IsAny<MessageDto>());

            //Assert
            _messageServiceMock.Verify(s => s.UpdateMessage(It.IsAny<Guid>(), It.IsAny<MessageDto>()));
        }

        [Fact]
        public async Task DeleteMessage_ServiceInvoke()
        {
            //Act
            await _messageController.DeleteMessage(It.IsAny<Guid>());

            //Assert
            _messageServiceMock.Verify(s => s.DeleteMessage(It.IsAny<Guid>()));
        }
    }
}