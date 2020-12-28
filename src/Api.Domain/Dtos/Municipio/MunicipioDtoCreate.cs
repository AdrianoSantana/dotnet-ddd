using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos.Municipio
{
    public class MunicipioDtoCreate
    {
        [Required(ErrorMessage = "Nome é obrigatório!")]
        [StringLength(60, ErrorMessage = "Nome deve conter no máximo {1} caracteres")]
        public string Nome { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Código IBGE inválido")]
        public int CodIBGE { get; set; }

        [Required(ErrorMessage = "Código UF é obrigatório!")]
        public Guid Ufid { get; set; }

    }
}