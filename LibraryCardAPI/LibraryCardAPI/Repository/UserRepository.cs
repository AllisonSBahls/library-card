using LibraryCardAPI.Models;
using LibraryCardAPI.Repository.Context;
using LibraryCardAPI.Repository.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryCardAPI.Repository
{
    public class UserRepository : GenericRepository, IUserRepository
    {
        public UserRepository(LibraryCardContext context) : base(context) { }
        
        public async Task<User> FindByIdAsync(int id)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task<List<User>> FindByNameAsync(string name, int size, int offset)
        {
            IQueryable<User> result = _context.Users;

            if (!string.IsNullOrWhiteSpace(name))
            {
                result = result.Where(x => x.FullName.Contains(name));
            }
            result = result.OrderBy(d => d.FullName).Skip(offset).Take(size);

            return await result.ToListAsync();
        }

        public int GetCount(string name)
        {
            int result;

            if (string.IsNullOrWhiteSpace(name))
            {
                result = _context.Users.Count();
            }
            else
            {
                result = _context.Users.Where(x => x.FullName.Contains(name)).Count();
            }

            return result;
        }
    }
}
