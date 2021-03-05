using LibraryCardAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryCardAPI.Repository.Context
{
    public class LibraryCardContext : IdentityDbContext<User, IdentityRole<int>, int, IdentityUserClaim<int>, IdentityUserRole<int>, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public LibraryCardContext(){ }

        public LibraryCardContext(DbContextOptions<LibraryCardContext> options) : base(options){}

        public DbSet<Student> Students { get; set; }
        public override DbSet<User> Users { get; set; }
    }
}
