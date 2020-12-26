using System;
using Api.Domain.Entities;
using Api.Domain.Models;
using FluentAssertions;
using Xunit;

namespace Api.Service.Test.AutoMapper
{
    public class UsuarioMapper : BaseTestService
    {
        [Fact(DisplayName = "É possivel Mapear os Modelos")]

        public void E_Possivel_Mapear_Os_Modelos()
        {
            var model = new UserModel
            {
                Id = Guid.NewGuid(),
                Name = Faker.Name.FullName(),
                Email = "ans3@cin.ufpe.br",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            // Converter Model ==> Entity
            var dtoToEntity = Mapper.Map<UserEntity>(model);
            Assert.Equal(dtoToEntity.Id, model.Id);
            Assert.Equal(dtoToEntity.Name, model.Name);
            Assert.Equal(dtoToEntity.Email, model.Email);
            /*
            (dtoToEntity.Id == model.Id).Should().BeTrue();
            (dtoToEntity.Name == model.Name).Should().BeTrue();
            (dtoToEntity.Email == model.Email).Should().BeTrue();
            */
        }
    }
}