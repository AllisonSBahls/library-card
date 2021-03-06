using LibraryCardAPI.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryCardAPI.Repository.Generic
{
    public class GenericRepository : IRepository
    {
        protected readonly LibraryCardContext _context;

        public GenericRepository(LibraryCardContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public void DeleteRange<T>(T[] entity) where T : class
        {
            _context.RemoveRange(entity);
        }
        public void Update<T>(T item, T entity) where T : class
        {
            _context.Entry(item).CurrentValues.SetValues(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
           return (await _context.SaveChangesAsync())> 0;
        }


    }
}
