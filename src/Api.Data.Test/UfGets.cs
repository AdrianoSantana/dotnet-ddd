using System;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Data.Implementations;
using Api.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Api.Data.Test
{
    public class UfGets : BaseTest, IClassFixture<DbTest>
    {
        private ServiceProvider _serviceProdive;
        public UfGets(DbTest dbTeste)
        {
            _serviceProdive = dbTeste.ServiceProvider;
        }

        [Fact(DisplayName = "Gets de UF")]
        [Trait("Gets", "UfEntity")]

        public async Task E_Possivel_Realizar_Gets_UF()
        {
            using (var context = _serviceProdive.GetService<MyContext>())
            {
                UfImplementation _repositorio = new UfImplementation(context);
                UfEntity _entity = new UfEntity
                {
                    Id = new Guid("ad5969bd-82dc-4e23-ace2-d8495935dd2e"),
                    Sigla = "PE",
                    Nome = "Pernambuco"
                };

                var _registroExiste = await _repositorio.ExistsAsync(_entity.Id);
                Assert.True(_registroExiste);

                var _registroSelecionado = await _repositorio.SelectAsync(_entity.Id);
                Assert.NotNull(_registroSelecionado);
                Assert.Equal(_entity.Nome, _registroSelecionado.Nome);
                Assert.Equal(_entity.Id, _registroSelecionado.Id);
                Assert.Equal(_entity.Sigla, _registroSelecionado.Sigla);

                var _todosRegistros = await _repositorio.SelectAsync();
                Assert.NotNull(_todosRegistros);
                Assert.True(_todosRegistros.Count() == 27);
            }
        }

    }
}