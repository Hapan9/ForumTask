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
    public class MessageServiceTests
    {
        private readonly FakeData _fakeData;
        private readonly Mock<IMessageRepository> _messageRepositoryMock;
        private readonly IMessageService _messageService;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public MessageServiceTests()
        {
            _fakeData = new FakeData();

            var userRepositoryMock = new Mock<IUserRepository>();
            var topicRepositoryMock = new Mock<ITopicRepository>();
            _messageRepositoryMock = new Mock<IMessageRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            _unitOfWorkMock.Setup(uow => uow.Users)
                .Returns(userRepositoryMock.Object);
            _unitOfWorkMock.Setup(uow => uow.Topics)
                .Returns(topicRepositoryMock.Object);
            _unitOfWorkMock.Setup(uow => uow.Messages)
                .Returns(_messageRepositoryMock.Object);

            var message = _fakeData.Messages.Last();

            _messageRepositoryMock.Setup(r => r.Get(It.IsAny<Guid>()))
                .ReturnsAsync(message);
            topicRepositoryMock.Setup(r => r.Get(It.IsAny<Guid>()))
                .ReturnsAsync(_fakeData.Topics.First(t => t.Id == message.TopicId));
            userRepositoryMock.Setup(r => r.Get(It.IsAny<Guid>()))
                .ReturnsAsync(_fakeData.Users.First(u => u.Id == message.UserId));

            _messageService = new MessageService(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Get_RepositoryInvokes()
        {
            //Act
            await _messageService.GetMessage(It.IsAny<Guid>());

            //Assert
            _messageRepositoryMock.Verify(r => r.Get(It.IsAny<Guid>()));
            _unitOfWorkMock.Verify(uow => uow.Messages);
        }

        [Fact]
        public async Task GetAll_RepositoryInvokes()
        {
            //Act
            await _messageService.GetMessages();

            //Assert
            _messageRepositoryMock.Verify(r => r.GetAll());
            _unitOfWorkMock.Verify(uow => uow.Messages);
        }

        [Fact]
        public async Task Create_RepositoryInvokes()
        {
            //Arrange
            var message = _fakeData.Messages.Last();

            var messageDto = new MessageDto
            {
                Text = message.Text,
                TopicId = message.TopicId,
                UserId = message.UserId
            };

            //Act
            await _messageService.CreateMessage(messageDto);

            //Assert
            _messageRepositoryMock.Verify(r => r.Create(It.IsAny<Message>()));
            _unitOfWorkMock.Verify(uow => uow.Users);
            _unitOfWorkMock.Verify(uow => uow.Topics);
            _unitOfWorkMock.Verify(uow => uow.Messages);
        }

        [Fact]
        public async Task Update_RepositoryInvokes()
        {
            //Arrange
            var message = _fakeData.Messages.Last();

            var messageDto = new MessageDto
            {
                Text = message.Text,
                TopicId = message.TopicId,
                UserId = message.UserId
            };

            var messageId = message.Id;

            //Act
            await _messageService.UpdateMessage(messageId, messageDto);

            //Assert
            _messageRepositoryMock.Verify(r => r.Update(It.IsAny<Message>()));
            _unitOfWorkMock.Verify(uow => uow.Users);
            _unitOfWorkMock.Verify(uow => uow.Topics);
            _unitOfWorkMock.Verify(uow => uow.Messages);
        }

        [Fact]
        public async Task Delete_RepositoryInvokes()
        {
            //Act
            await _messageService.DeleteMessage(It.IsAny<Guid>());

            //Assert
            _messageRepositoryMock.Verify(r => r.Delete(It.IsAny<Guid>()));
            _unitOfWorkMock.Verify(uow => uow.Messages);
        }
    }
}