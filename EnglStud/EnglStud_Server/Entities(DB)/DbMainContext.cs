using EnglStud_Server.Entities;
using EnglStud_Server.Json_Classes;
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
        public DbSet<Word> Words { get; set; }
        public DbMainContext() : base("EnSt_Connection") { }        
    }
}
