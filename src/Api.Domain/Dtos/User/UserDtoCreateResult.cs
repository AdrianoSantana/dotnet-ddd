using System;

namespace Api.Domain.Dtos.User
{
    public class UserDtoCreateResult
    {
        public string Nome { get; set; }
        public string Email { get; set; }

        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}