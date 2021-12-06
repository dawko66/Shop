using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VODUser.Models.Identity;

namespace VODUser.Models
{
    public class Database : IdentityDbContext<User>
    {
        public Database(DbContextOptions<Database> options) : base(options)
        {

        }
    }
}
