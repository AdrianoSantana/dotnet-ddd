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
    public class UsuarioCrudCompleto : BaseTest, IClassFixture<DbTest>
    {
        private ServiceProvider _serviceProdive;

        public UsuarioCrudCompleto(DbTest dbTest)
        {
            _serviceProdive = dbTest.ServiceProvider;
        }

        [Fact(DisplayName = "Crud de usuário")]
        [Trait("CRUD", "UserEntity")]
        public async Task E_Possivel_Realizar_Crid_Usuario()
        {
            using (var context = _serviceProdive.GetService<MyContext>())
            {
                UserImplementation _repository = new UserImplementation(context);
                UserEntity _entity = new UserEntity
                {
                    Email = Faker.Internet.Email(),
                    Name = Faker.Name.FullName()
                };

                var registroCriado = await _repository.InsertAsync(_entity);
                Assert.NotNull(registroCriado); // o registro não deve ser null
                Assert.Equal(_entity.Email, registroCriado.Email);
                Assert.Equal(_entity.Name, registroCriado.Name);
                Assert.False(registroCriado.Id == Guid.Empty);


                _entity.Name = Faker.Name.First();
                var registroAtualizado = await _repository.UpdateAsync(_entity);

                Assert.NotNull(registroAtualizado);
                Assert.Equal(_entity.Name, registroAtualizado.Name);
                Assert.Equal(_entity.Email, registroCriado.Email);
                Assert.Equal(registroCriado.Id, registroAtualizado.Id);

                var _registroExiste = await _repository.ExistsAsync(registroAtualizado.Id);
                Assert.True(_registroExiste);

                var _registroSelecionado = await _repository.SelectAsync(registroAtualizado.Id);
                Assert.NotNull(_registroSelecionado);
                Assert.Equal(registroAtualizado.Name, _registroSelecionado.Name);
                Assert.Equal(registroAtualizado.Email, _registroSelecionado.Email);
                Assert.Equal(registroAtualizado.Id, _registroSelecionado.Id);

                var _todosRegistros = await _repository.SelectAsync();
                Assert.NotNull(_todosRegistros);
                Assert.True(_todosRegistros.Count() > 0);

                var _removeu = await _repository.DeleteAsync(_registroSelecionado.Id);
                Assert.True(_removeu);

            }
        }

        [Fact(DisplayName = "Find By Login")]
        [Trait("Login", "FindByLogin")]
        public async Task E_Possivel_Realizar_Login()
        {
            using (var context = _serviceProdive.GetService<MyContext>())
            {
                UserImplementation _repository = new UserImplementation(context);
                var _entity = new UserEntity
                {
                    Name = Faker.Name.FullName(),
                    Email = Faker.Internet.Email()
                };

                var _registroCriado = await _repository.InsertAsync(_entity);
                Assert.NotNull(_registroCriado); // o registro não deve ser null
                Assert.Equal(_entity.Email, _registroCriado.Email);
                Assert.Equal(_entity.Name, _registroCriado.Name);
                Assert.False(_registroCriado.Id == Guid.Empty);

                var _usuarioLogin = await _repository.FindByLogin(_registroCriado.Email);
                Assert.NotNull(_usuarioLogin);
                Assert.Equal(_registroCriado.Email, _usuarioLogin.Email);
            }
        }
    }
}