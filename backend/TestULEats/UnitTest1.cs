using backend.Controllers;
using backend.Core;
using DataModel;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Numerics;
using Xunit;

namespace TestULEats
{
    public class ClientControllerTests
    {
        private readonly Mock<ClientService> _mockClientService;
        private readonly ClientController _controller;

        public ClientControllerTests()
        {
            _mockClientService = new Mock<ClientService>(MockBehavior.Strict, (UlEatsDb)null);

            _controller = new ClientController(_mockClientService.Object);
        }

        [Fact]
        public void Login_WithInvalidCredentials_ReturnsUnauthorized()
        {
            var loginRequest = new ClientController.LoginRequest
            {
                Email = "wrong@example.com",
                Password = "wrongpass"
            };

            _mockClientService.Setup(s => s.LoginAndGetUser(loginRequest.Email, loginRequest.Password))
                              .Returns((User)null);

            var result = _controller.Login(loginRequest);

            Assert.IsType<UnauthorizedObjectResult>(result);
        }
        [Fact]
        public void Register_WithMissingFields_ReturnsBadRequest()
        {
            var request = new ClientController.RegisterRequest
            {
                Email = "",
                Password = "12345678",
                FirstName = "",
                LastName = "Alvarez",
                Phone = "123123123",
                UserType = "Customer"
            };

            var result = _controller.Register(request);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("All fields must be filled!", badRequest.Value);
        }

        [Fact]
        public void Register_WithInvalidEmail_ReturnsBadRequest()
        {
            var request = new ClientController.RegisterRequest
            {
                Email = "invalid-email",
                Password = "12345678",
                FirstName = "Carlos",
                LastName = "Alvarez",
                Phone = "123123213",
                UserType = "Customer"
            };

            var result = _controller.Register(request);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Email is not in a valid pattern", badRequest.Value);
        }

        [Fact]
        public void Register_WithShortPassword_ReturnsBadRequest()
        {
            var request = new ClientController.RegisterRequest
            {
                Email = "xu@gmail.com",
                Password = "123",
                FirstName = "Xu",
                LastName = "Chen",
                Phone = "123123123",
                UserType = "Customer"
            };

            var result = _controller.Register(request);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Password is weak, please enter more characters", badRequest.Value);
        }

        [Fact]
        public void Register_WithInvalidPhoneLength_ReturnsBadRequest()
        {
            var request = new ClientController.RegisterRequest
            {
                Email = "carlos@gmail.com",
                Password = "12345678",
                FirstName = "Carlos",
                LastName = "Alvarez",
                Phone = "12345",
                UserType = "Customer"
            };

            var result = _controller.Register(request);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Phone must have 9 digits!", badRequest.Value);
        }

        [Fact]
        public void Register_WithAlreadyRegisteredEmail_ReturnsBadRequest()
        {
            var request = new ClientController.RegisterRequest
            {
                Email = "carlos@gmail.com",
                Password = "12345678",
                FirstName = "Carlos",
                LastName = "Alvarez",
                Phone = "123123123",
                UserType = "Customer"
            };

            _mockClientService.Setup(s => s.RegisterAndGetUser(
                request.FirstName,
                request.Password,
                request.Email,
                request.LastName,
                request.Phone,
                request.UserType
            )).Returns((User)null);

            var result = _controller.Register(request);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Email is already registered", badRequest.Value);
            
        }
    }
}