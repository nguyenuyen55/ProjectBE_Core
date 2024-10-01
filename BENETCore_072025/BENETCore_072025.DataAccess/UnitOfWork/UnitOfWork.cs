using BENETCore_072025.DataAccess.DBContext;
using BENETCore_072025.DataAccess.Reponsitory.Implement;
using BENETCore_072025.DataAccess.Reponsitory.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BENETCore_072025.DataAccess.UnitOfWork
{
    public class UnitOfWork : IunitOfWork, IDisposable
    {
        private readonly DBHotelContext _context;
        public UnitOfWork(DBHotelContext context)
        {
            
            _context = context;
        }
        public IGenericRepository<T> Repository<T>() where T : class
        {
            return new GenericRepository<T>(_context);
        }

        public async Task<int> SaveChangesAsync()
        {
           return await _context.SaveChangesAsync();
        }

        void IDisposable.Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
