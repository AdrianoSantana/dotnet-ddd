using System;
using System.Threading.Tasks;
using Api.Domain.Interfaces.Services.User;
using FluentAssertions;
using Moq;
using Xunit;

namespace Api.Service.Test.User
{
    public class QuandoForExecutadoDelete : UserTest
    {
        private IUserService _service;
        private Mock<IUserService> _serviceMock;

        [Fact(DisplayName = "É possivel deletar um usuário")]

        public async Task E_Possivel_Executar_Metodo_Deletar()
        {
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(mock => mock.Delete(It.IsAny<Guid>()))
                        .ReturnsAsync(true);
            _service = _serviceMock.Object;

            var deletado = await _service.Delete(IdUsuario);
            deletado.Should().BeTrue();
        }
    }
}