using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Entities
{
    public abstract class BaseEntity
    {
        [Key] // Usamos a Data annotation [Key] para informar que a próxima variável sera um id
        public Guid Id { get; set; }

        private DateTime? _createdAt;
        public DateTime? CreatedAt
        {
            get { return _createdAt; }
            set { _createdAt = (value == null ? DateTime.UtcNow : value); }
        }

        public DateTime? UpdatedAt { get; set; }

    }
}
