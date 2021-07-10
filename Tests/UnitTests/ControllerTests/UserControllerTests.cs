using System;
using System.Threading.Tasks;
using BLL.DTOs;
using BLL.Interfaces;
using Moq;
using PL.Controllers;
using Xunit;

namespace Tests.UnitTests.ControllerTests
{
    public class UserControllerTests
    {
        private readonly UserController _userController;
        private readonly Mock<IUserService> _userServiceMock;

        public UserControllerTests()
        {
            _userServiceMock = new Mock<IUserService>();
            _userController = new UserController(_userServiceMock.Object);
        }

        [Fact]
        public async Task GetUser_ServiceInvoke()
        {
            //Act
            await _userController.GetUser(It.IsAny<Guid>());

            //Assert
            _userServiceMock.Verify(s => s.GetUser(It.IsAny<Guid>()));
        }

        [Fact]
        public async Task GetUsers_ServiceInvoke()
        {
            //Act
            await _userController.GetUsers();

            //Assert
            _userServiceMock.Verify(s => s.GetUsers());
        }

        [Fact]
        public async Task CreateUser_ServiceInvoke()
        {
            //Act
            await _userController.CreateUser(It.IsAny<UserDto>());

            //Assert
            _userServiceMock.Verify(s => s.CreateUser(It.IsAny<UserDto>()));
        }

        [Fact]
        public async Task UpdateUser_ServiceInvoke()
        {
            //Act
            await _userController.UpdateUser(It.IsAny<Guid>(), It.IsAny<UserDto>());

            //Assert
            _userServiceMock.Verify(s => s.UpdateUser(It.IsAny<Guid>(), It.IsAny<UserDto>()));
        }

        [Fact]
        public async Task DeleteUser_ServiceInvoke()
        {
            //Act
            await _userController.DeleteUser(It.IsAny<Guid>());

            //Assert
            _userServiceMock.Verify(s => s.DeleteUser(It.IsAny<Guid>()));
        }

        [Fact]
        public async Task GetTopics_ServiceInvoke()
        {
            //Act
            await _userController.GetTopics(It.IsAny<Guid>());

            //Arrange
            _userServiceMock.Verify(s => s.GetTopics(It.IsAny<Guid>()));
        }

        [Fact]
        public async Task GetMessages_ServiceInvoke()
        {
            //Act
            await _userController.GetMessages(It.IsAny<Guid>());

            //Arrange
            _userServiceMock.Verify(s => s.GetMessages(It.IsAny<Guid>()));
        }
    }
}