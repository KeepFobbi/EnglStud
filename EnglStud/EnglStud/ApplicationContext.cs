using EnglStud.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace EnglStud
{
    class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Word> Words { get; set; }

        public ApplicationContext() : base("DefaultConnection") { }

        public void RowCount()
        {
            try
            {
                var configuration = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                using (SQLiteConnection conn = new SQLiteConnection(configuration))
                {
                    var command = new SQLiteCommand(conn);
                    command.CommandText = "SELECT COUNT(Id) FROM Words";
                    command.CommandType = CommandType.Text;
                    int сount = (int)command.ExecuteScalar();
                    Console.WriteLine(сount);
                }
            }
            catch (Exception) { }
        }
    }
}
