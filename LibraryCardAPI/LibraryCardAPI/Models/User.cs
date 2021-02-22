using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryCardAPI.Models
{
    public class User : IdentityUser<int>
    {
        public int FullName { get; set; }
    }
}
