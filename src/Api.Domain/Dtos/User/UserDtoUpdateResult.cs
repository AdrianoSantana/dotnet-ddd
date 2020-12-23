using System;

namespace Api.Domain.Dtos.User
{
    public class UserDtoUpdateResult
    {
        public string Nome { get; set; }
        public string Email { get; set; }

        public Guid Id { get; set; }

        public DateTime UpdateAt { get; set; }
    }
}