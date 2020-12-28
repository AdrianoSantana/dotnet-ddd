using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.Mapping
{
    public class MunicipioMap : IEntityTypeConfiguration<MunicipioEntity>
    {
        public void Configure(EntityTypeBuilder<MunicipioEntity> builder)
        {
            builder.ToTable("Municipio");
            builder.HasKey(municipio => municipio.Id);
            builder.HasIndex(municipio => municipio.CodIBGE);
            builder.HasOne(uf => uf.Uf)
                    .WithMany(municipio => municipio.Municipios);
        }
    }
}