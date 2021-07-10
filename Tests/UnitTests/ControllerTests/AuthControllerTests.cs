using System.Threading.Tasks;
using BLL.DTOs;
using BLL.Interfaces;
using Moq;
using PL.Controllers;
using PL.Models;
using Xunit;

namespace Tests.UnitTests.ControllerTests
{
    public class AuthControllerTests
    {
        private readonly AuthorizationController _authController;
        private readonly Mock<IUserService> _userServiceMock;

        public AuthControllerTests()
        {
            _userServiceMock = new Mock<IUserService>();
            _authController = new AuthorizationController(_userServiceMock.Object);
        }

        [Fact]
        public async Task Login_ServiceInvoke()
        {
            //Act
            await _authController.UserAuthorize(It.IsAny<AuthorizationDto>());

            //Assert
            _userServiceMock.Verify(s => s.CheckUserForm(It.IsAny<AuthorizationDto>()));
        }

        [Fact]
        public async Task Registration_ServiceInvoke()
        {
            //Assert
            var registrationModel = new RegistrationModel
            {
                Name = "UserName",
                Surname = "UserSurname",
                Password = "UserPassword"
            };

            //Act
            await _authController.CreateUser(registrationModel);

            //Assert
            _userServiceMock.Verify(s => s.CreateUser(It.IsAny<UserDto>()));
        }
    }
}