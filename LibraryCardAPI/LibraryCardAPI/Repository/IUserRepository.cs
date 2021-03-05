﻿using LibraryCardAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryCardAPI.Repository
{
    public interface IUserRepository
    {
        Task<User> FindByIdAsync(int id);
        Task<List<User>> FindByNameAsync(string name, int size, int offset);
        int GetCount(string name);
               
    }
}
