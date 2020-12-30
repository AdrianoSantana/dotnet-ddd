using System;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Data.Implementations;
using Api.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Api.Data.Test
{
    public class Municipio : BaseTest, IClassFixture<DbTest>
    {
        private ServiceProvider _serviceProvider;

        public Municipio(DbTest dbTest)
        {
            _serviceProvider = dbTest.ServiceProvider;
        }

        [Fact(DisplayName = "Crud Municipio")]
        public async Task Crud_Municipio()
        {
            using (var context = _serviceProvider.GetService<MyContext>())
            {
                MunicipioImplementation _repositorio = new MunicipioImplementation(context);
                MunicipioEntity _entity = new MunicipioEntity
                {
                    Nome = Faker.Address.City(),
                    CodIBGE = Faker.RandomNumber.Next(1000000, 9999999),
                    UfId = new Guid("ad5969bd-82dc-4e23-ace2-d8495935dd2e")
                };

                var _registroCriado = await _repositorio.InsertAsync(_entity);
                Assert.NotNull(_registroCriado);
                Assert.Equal(_entity.Nome, _registroCriado.Nome);
                Assert.Equal(_entity.CodIBGE, _registroCriado.CodIBGE);
                Assert.Equal(_entity.UfId, _registroCriado.UfId);
                Assert.False(_registroCriado.Id == Guid.Empty);

                // Update
                _entity.Nome = Faker.Address.City();
                _entity.Id = _registroCriado.Id;
                var _registroAtualizado = await _repositorio.UpdateAsync(_entity);
                Assert.NotNull(_registroAtualizado);
                Assert.Equal(_entity.Nome, _registroAtualizado.Nome);
                Assert.Equal(_entity.CodIBGE, _registroAtualizado.CodIBGE);
                Assert.Equal(_entity.UfId, _registroAtualizado.UfId);
                Assert.Equal(_entity.Id, _registroAtualizado.Id);

                // Exist
                var _registroExiste = await _repositorio.ExistsAsync(_registroAtualizado.Id);
                Assert.True(_registroExiste);

                // Select with Id
                var _registroSelecionado = await _repositorio.SelectAsync(_registroAtualizado.Id);
                Assert.NotNull(_registroSelecionado);
                Assert.Equal(_registroAtualizado.Nome, _registroSelecionado.Nome);
                Assert.Equal(_registroAtualizado.CodIBGE, _registroSelecionado.CodIBGE);
                Assert.Equal(_registroAtualizado.UfId, _registroSelecionado.UfId);
                Assert.Null(_registroSelecionado.Uf);

                // Get Complete By Ibge
                _registroSelecionado = await _repositorio.GetCompleteByIBGE(_registroAtualizado.CodIBGE);
                Assert.NotNull(_registroSelecionado);
                Assert.Equal(_registroAtualizado.Nome, _registroSelecionado.Nome);
                Assert.Equal(_registroAtualizado.CodIBGE, _registroSelecionado.CodIBGE);
                Assert.Equal(_registroAtualizado.UfId, _registroSelecionado.UfId);
                Assert.NotNull(_registroSelecionado.Uf);

                // Get Complete By Id
                _registroSelecionado = await _repositorio.GetCompleteById(_registroAtualizado.Id);
                Assert.NotNull(_registroSelecionado);
                Assert.Equal(_registroAtualizado.Nome, _registroSelecionado.Nome);
                Assert.Equal(_registroAtualizado.CodIBGE, _registroSelecionado.CodIBGE);
                Assert.Equal(_registroAtualizado.UfId, _registroSelecionado.UfId);
                Assert.NotNull(_registroSelecionado.Uf);
            }
        }

    }
}