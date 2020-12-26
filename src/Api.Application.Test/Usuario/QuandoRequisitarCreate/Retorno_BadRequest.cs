using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.Usuario.QuandoRequisitarCreate
{
    public class Retorno_BadRequest
    {
        private UsersController _controller;

        [Fact(DisplayName = "Acontece o badRequest")]

        public async Task Acontece_Bad_Request()
        {
            var serviceMock = new Mock<IUserService>();
            var nome = Faker.Name.FullName();
            var email = Faker.Internet.Email();

            serviceMock.Setup(m => m.Post(It.IsAny<UserDto>())).ReturnsAsync(
                    new UserDtoCreateResult
                    {
                        Id = Guid.NewGuid(),
                        Nome = nome,
                        Email = email,
                        CreateAt = DateTime.UtcNow
                    }
                );

            _controller = new UsersController(serviceMock.Object); // Nosso controller recebe um IUserService
            Mock<IUrlHelper> url = new Mock<IUrlHelper>();

            url.Setup(x => x.Link(It.IsAny<string>(), It.IsAny<Object>())).Returns("http://localhost:5000");

            _controller.Url = url.Object;
            _controller.ModelState.AddModelError("Name", "É um campo obrigatório");

            var userDto = new UserDto
            {
                Name = nome,
                Email = email
            };

            var result = await _controller.Post(userDto);
            Assert.True(result is BadRequestObjectResult);
        }

    }
}