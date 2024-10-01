using BENETCore_072025.DataAccess.DBContext;
using BENETCore_072025.DataAccess.Reponsitory.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BENETCore_072025.DataAccess.Reponsitory.Implement
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DBHotelContext _dbHotelContext;


        public GenericRepository(DBHotelContext dbHotelContext) {
        _dbHotelContext = dbHotelContext;
        }
        public async Task AddAsync(T entity)
        {
            _dbHotelContext.Set<T>().Add(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dbHotelContext.Set<T>().Remove(entity);
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
           return _dbHotelContext.Set<T>().ToList();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return  _dbHotelContext.Set<T>().Find(id);
        }

        public async Task UpdateAsync(T entity)
        {
            _dbHotelContext.Update(entity);
        }
    }
}
