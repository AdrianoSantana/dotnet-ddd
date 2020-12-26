using System;
using System.Threading.Tasks;
using Api.Domain.Interfaces.Services.User;
using FluentAssertions;
using Moq;
using Xunit;

namespace Api.Service.Test.User
{
    public class QuandoForExecutadoCreate : UserTest
    {
        private IUserService _service;
        private Mock<IUserService> _serviceMock;

        [Fact(DisplayName = "É possivel executar o método create")]

        public async Task E_Possivel_Executar_Metodo_Create()
        {
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(mock => mock.Post(userDto)).ReturnsAsync(userDtoCreateResult);
            _service = _serviceMock.Object;

            var result = await _service.Post(userDto);
            result.Should().NotBeNull();
            (result.Nome == userDto.Name).Should().BeTrue();
            (result.Email == userDto.Email).Should().BeTrue();
        }
    }
}