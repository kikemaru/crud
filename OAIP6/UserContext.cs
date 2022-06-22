using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace OAIP6
{
    class UserContext : DbContext
    {
        public UserContext() : base("DefaultConnection") { }
        public DbSet<User> Izdatelstvo { get; set; }
    }
}
