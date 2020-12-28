using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.Usuario.QuandoRequisitarUpdate
{
    public class Retorno_Update
    {
        private UsersController _controller;

        [Fact(DisplayName = "É possivel realizar Update")]

        public async Task E_possivel_realizar_update()
        {
            var serviceMock = new Mock<IUserService>();
            var nome = Faker.Name.FullName();
            var email = Faker.Internet.Email();

            serviceMock.Setup(m => m.Put(It.IsAny<UserDto>())).ReturnsAsync(
                value: new UserDtoUpdateResult
                {
                    Id = Guid.NewGuid(),
                    Nome = nome,
                    Email = email,
                    UpdateAt = DateTime.UtcNow
                }
            );

            _controller = new UsersController(serviceMock.Object);
            Mock<IUrlHelper> url = new Mock<IUrlHelper>();

            var userDto = new UserDto
            {
                Name = nome,
                Email = email
            };

            var result = await _controller.Update(userDto);
            Assert.True(result is OkObjectResult);
        }
    }
}