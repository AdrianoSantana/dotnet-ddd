using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.Mapping
{
    public class CepMap : IEntityTypeConfiguration<CepEntity>
    {
        public void Configure(EntityTypeBuilder<CepEntity> builder)
        {
            builder.ToTable("Cep");
            builder.HasKey(cep => cep.Id);
            builder.HasIndex(cep => cep.Cep);
            builder.HasOne(cep => cep.Municipio)
                    .WithMany(municipio => municipio.Ceps);
        }
    }
}