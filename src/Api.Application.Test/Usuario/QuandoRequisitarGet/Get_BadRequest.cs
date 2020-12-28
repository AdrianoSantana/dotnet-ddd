using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.Usuario.QuandoRequisitarGet
{
    public class Result_Bad
    {
        private UsersController _controller;

        [Fact(DisplayName = "Erro ao realizar Get")]

        public async Task E_possivel_realizar_get()
        {
            var serviceMock = new Mock<IUserService>();
            var nome = Faker.Name.FullName();
            var email = Faker.Internet.Email();

            serviceMock.Setup(m => m.Get(It.IsAny<Guid>())).ReturnsAsync(
                new UserDto
                {
                    Name = nome,
                    Email = email,
                }
            );

            _controller = new UsersController(serviceMock.Object);

            _controller.ModelState.AddModelError("Id", "formato inválido");

            var result = await _controller.GetById(Guid.NewGuid());

            Assert.True(result is BadRequestObjectResult);
        }
    }
}