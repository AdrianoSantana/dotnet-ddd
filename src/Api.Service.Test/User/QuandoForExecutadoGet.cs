using System;
using System.Threading.Tasks;
using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services.User;
using FluentAssertions;
using Moq;
using Xunit;

namespace Api.Service.Test.User
{
    public class QuandoForExecutadoGet : UserTest
    {
        private IUserService _service;

        // Faz uma imitação das informações.Não chega a inserir os dados no banco
        private Mock<IUserService> _serviceMock;

        [Fact(DisplayName = "É possivel Executar o Método Get")]
        public async Task E_Possivel_Executar_Metodo_Get()
        {
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(mock => mock.Get(IdUsuario)).ReturnsAsync(userDto);
            _service = _serviceMock.Object; // Agora o service possui o objeto serviceMock

            var result = await _service.Get(IdUsuario);
            result.Should().NotBeNull(because: "Existe um usuário com este id");
            (EmailUsuario == result.Email).Should().BeTrue();
            (result.Name).Should().Equals(NomeUsuario);

            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(mock => mock.Get(It.IsAny<Guid>())).Returns(Task.FromResult((UserDto)null));
            _service = _serviceMock.Object;

            var _record = await _service.Get(IdUsuario);
            _record.Should().BeNull();
        }
    }
}