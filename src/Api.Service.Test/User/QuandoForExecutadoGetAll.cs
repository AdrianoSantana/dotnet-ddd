using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services.User;
using FluentAssertions;
using Moq;
using Xunit;

namespace Api.Service.Test.User
{
    public class QuandoForExecutadoGetAll : UserTest
    {
        private IUserService _service;
        private Mock<IUserService> _serviceMock;


        [Fact(DisplayName = "É possivel executar o método Get All")]

        public async Task E_Possivel_Executar_Metodo_Get_All()
        {
            _serviceMock = new Mock<IUserService>();
            // configura quem será retornado
            _serviceMock.Setup(mock => mock.GetAll()).ReturnsAsync(listaUserDto);
            _service = _serviceMock.Object;

            var _result = await _service.GetAll();
            _result.Should().NotBeNull(because: "Existem 10 usuários inseridos");
            _result.Should().HaveCount(10, because: "Existem 10 usuários na lista");

            // Verificando a lista vázia

            var _listaUsuariosVazio = new List<UserDto>();
            _serviceMock.Setup(mock => mock.GetAll()).ReturnsAsync(_listaUsuariosVazio);
            _service = _serviceMock.Object;

            var resultVazio = await _service.GetAll();
            resultVazio.Should().BeEmpty();
            (resultVazio.Count() == 0).Should().BeTrue();
        }
    }
}