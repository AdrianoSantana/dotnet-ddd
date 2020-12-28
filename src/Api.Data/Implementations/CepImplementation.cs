using System.Threading.Tasks;
using Api.Data.Context;
using Api.Data.Repository;
using Api.Domain.Entities;
using Api.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Implementations
{
    public class CepImplementation : BaseRepository<CepEntity>, ICepRepository
    {
        private DbSet<CepEntity> _dataset;

        public CepImplementation(MyContext context) : base(context)
        {
            _dataset = context.Set<CepEntity>();

        }

        public async Task<CepEntity> SelectAsync(string cep)
        {
            return await _dataset.Include(cep => cep.Municipio)
                                .ThenInclude(MunicipioEntity => MunicipioEntity.Uf)
                                .SingleOrDefaultAsync(u => u.Cep.Equals(cep));
        }
    }
}
