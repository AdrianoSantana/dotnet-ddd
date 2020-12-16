using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly MyContext _context;
        private DbSet<T> _dataset;
        public BaseRepository(MyContext mycontext)
        {
            _context = mycontext;
            _dataset = _context.Set<T>(); // Evita fazer o _context.TABLE
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var result = await _dataset.SingleOrDefaultAsync(objeto => objeto.Id.Equals(id));
                if (result == null)
                    return false;

                _context.Remove(result);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _dataset.AnyAsync(objeto => objeto.Id.Equals(id));
        }

        public async Task<T> InsertAsync(T entity)
        {
            try
            {
                if (entity.Id == Guid.Empty)
                {
                    entity.Id = Guid.NewGuid();
                }
                entity.CreatedAt = DateTime.UtcNow;
                _dataset.Add(entity); // _context.Tabela.Add()

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return entity;
        }

        public async Task<T> SelectAsync(Guid id)
        {
            try
            {
                return await _dataset.SingleOrDefaultAsync(objeto => objeto.Id.Equals(id));
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IEnumerable<T>> SelectAsync()
        {
            try
            {
                return await _dataset.ToListAsync(); // Select sem where
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<T> UpdateAsync(T entity)
        {
            try
            {
                var result = await _dataset.SingleOrDefaultAsync(objeto => objeto.Id.Equals(entity.Id));
                if (result == null)
                {
                    return null;
                }
                entity.UpdatedAt = DateTime.UtcNow;
                entity.CreatedAt = result.CreatedAt;

                _context.Entry(result).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return entity;
        }
    }
}