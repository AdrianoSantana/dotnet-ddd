using System;
using System.Collections.Generic;
using Api.Domain.Dtos.User;

namespace Api.Service.Test.User
{
    public class UserTest
    {
        public static string NomeUsuario { get; set; }
        public static string EmailUsuario { get; set; }

        public static string NomeUsuarioAlterado { get; set; }
        public static string EmailUsuarioAlterado { get; set; }

        public static Guid IdUsuario { get; set; }

        public List<UserDto> listaUserDto = new List<UserDto>();

        public UserDto userDto;

        public UserDtoCreateResult userDtoCreateResult;

        public UserDtoUpdateResult userDtoUpdateResult;

        public UserTest()
        {
            IdUsuario = Guid.NewGuid();
            NomeUsuario = Faker.Name.FullName();
            EmailUsuario = Faker.Internet.Email();
            NomeUsuarioAlterado = Faker.Name.FullName();
            EmailUsuarioAlterado = Faker.Internet.Email();

            for (int i = 0; i < 10; i++)
            {
                var dto = new UserDto()
                {
                    Name = Faker.Name.FullName(),
                    Email = Faker.Internet.Email()
                };
                listaUserDto.Add(dto);
            }

            userDto = new UserDto()
            {
                Name = NomeUsuario,
                Email = EmailUsuario
            };

            userDtoCreateResult = new UserDtoCreateResult()
            {
                Id = IdUsuario,
                Nome = NomeUsuario,
                Email = EmailUsuario,
                CreateAt = DateTime.UtcNow
            };

            userDtoUpdateResult = new UserDtoUpdateResult()
            {
                Id = IdUsuario,
                Nome = NomeUsuarioAlterado,
                Email = EmailUsuarioAlterado,
                UpdateAt = DateTime.UtcNow
            };
        }
    }
}