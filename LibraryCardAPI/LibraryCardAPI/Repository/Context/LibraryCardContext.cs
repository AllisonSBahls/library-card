using LibraryCardAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryCardAPI.Repository.Context
{
    public class LibraryCardContext : IdentityDbContext
    {
        public LibraryCardContext(){ }

        public LibraryCardContext(DbContextOptions<LibraryCardContext> options) : base(options){}

        public DbSet<Student> Students { get; set; }
    }
}
