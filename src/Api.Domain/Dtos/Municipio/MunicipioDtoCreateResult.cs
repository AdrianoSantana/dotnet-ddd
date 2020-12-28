using System;

namespace Api.Domain.Dtos.Municipio
{
    public class MunicipioDtoCreateResult
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public int CodIBGE { get; set; }
        public Guid Ufid { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}