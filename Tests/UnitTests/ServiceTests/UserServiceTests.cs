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
    public class UserServiceTests
    {
        private readonly FakeData _fakeData;
        private readonly Mock<IMessageRepository> _messageRepositoryMock;
        private readonly Mock<ITopicRepository> _topicRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly IUserService _userService;

        public UserServiceTests()
        {
            _fakeData = new FakeData();

            _userRepositoryMock = new Mock<IUserRepository>();
            _topicRepositoryMock = new Mock<ITopicRepository>();
            _messageRepositoryMock = new Mock<IMessageRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            _unitOfWorkMock.Setup(uow => uow.Users)
                .Returns(_userRepositoryMock.Object);
            _unitOfWorkMock.Setup(uow => uow.Topics)
                .Returns(_topicRepositoryMock.Object);
            _unitOfWorkMock.Setup(uow => uow.Messages)
                .Returns(_messageRepositoryMock.Object);
            _userRepositoryMock.Setup(r => r.Get(It.IsAny<Guid>()))
                .ReturnsAsync(_fakeData.Users.Last());

            _userService = new UserService(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Get_RepositoryInvokes()
        {
            //Act
            await _userService.GetUser(It.IsAny<Guid>());

            //Assert
            _userRepositoryMock.Verify(r => r.Get(It.IsAny<Guid>()));
            _unitOfWorkMock.Verify(uow => uow.Users);
        }

        [Fact]
        public async Task GetAll_RepositoryInvokes()
        {
            //Act
            await _userService.GetUsers();

            //Assert
            _userRepositoryMock.Verify(r => r.GetAll());
            _unitOfWorkMock.Verify(uow => uow.Users);
        }

        [Fact]
        public async Task Create_RepositoryInvokes()
        {
            //Arrange
            var user = _fakeData.Users.Last();

            var userDto = new UserDto
            {
                Login = user.Login,
                Password = user.Password.ToString(),
                Role = user.Role,
                Name = user.Name,
                Surname = user.Surname
            };

            //Act
            await _userService.CreateUser(userDto);

            //Assert
            _userRepositoryMock.Verify(r => r.Create(It.IsAny<User>()));
            _unitOfWorkMock.Verify(uow => uow.Users);
        }

        [Fact]
        public async Task Update_RepositoryInvokes()
        {
            //Arrange
            var user = _fakeData.Users.Last();

            var userDto = new UserDto
            {
                Login = user.Login,
                Password = user.Password.ToString(),
                Role = user.Role,
                Name = user.Name,
                Surname = user.Surname
            };

            var userId = user.Id;

            //Act
            await _userService.UpdateUser(userId, userDto);

            //Assert
            _userRepositoryMock.Verify(r => r.Update(It.IsAny<User>()));
            _unitOfWorkMock.Verify(uow => uow.Users);
        }

        [Fact]
        public async Task Delete_RepositoryInvokes()
        {
            //Act
            await _userService.DeleteUser(It.IsAny<Guid>());

            //Assert
            _userRepositoryMock.Verify(r => r.Delete(It.IsAny<Guid>()));
            _unitOfWorkMock.Verify(uow => uow.Users);
        }

        [Fact]
        public async Task GetTopics_RepositoryInvokes()
        {
            //Act
            await _userService.GetTopics(It.IsAny<Guid>());

            //Assert
            _topicRepositoryMock.Verify(r => r.GetAll());
            _userRepositoryMock.Verify(r => r.Get(It.IsAny<Guid>()));
            _unitOfWorkMock.Verify(uow => uow.Topics);
            _unitOfWorkMock.Verify(uow => uow.Users);
        }

        [Fact]
        public async Task GetMessages_RepositoryInvokes()
        {
            //Act
            await _userService.GetMessages(It.IsAny<Guid>());

            //Assert
            _messageRepositoryMock.Verify(r => r.GetAll());
            _userRepositoryMock.Verify(r => r.Get(It.IsAny<Guid>()));
            _unitOfWorkMock.Verify(uow => uow.Users);
            _unitOfWorkMock.Verify(uow => uow.Messages);
        }

        [Fact]
        public async Task CheckForm_RepositoryInvokes()
        {
            //Arrange
            _userRepositoryMock.Setup(r => r.GetAll())
                .ReturnsAsync(_fakeData.Users);
            var user = _fakeData.Users.Last();
            var authDto = new AuthorizationDto
            {
                Login = user.Login,
                Password = user.Password.ToString()
            };

            //Act
            await _userService.CheckUserForm(authDto);

            //Assert
            _userRepositoryMock.Verify(r => r.GetAll());
            _unitOfWorkMock.Verify(uow => uow.Users);
        }
    }
}