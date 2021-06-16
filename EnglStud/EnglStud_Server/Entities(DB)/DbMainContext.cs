using EnglStud_Server.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EnglStud_Server
{
    class DbMainContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbMainContext() : base("EnSt_Connection") { }        
    }
}
