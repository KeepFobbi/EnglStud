using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using EnglStud_Server.Entities;

namespace EnglStud_Server
{
    class Program
    {
        
        static void Main(string[] args)
        {
            //OpenConnection connection = new OpenConnection(); 
            DbMainContext db = new DbMainContext();

            User user = new User("test4", "test4", "test4@ukr.net");
            User user2 = new User(1, "test", "test", "test@ukr.net");

            db.Users.Add(user);
            //db.Users.Add(user2);
            db.SaveChanges();
            Console.WriteLine("Objects was added...");

            Console.ReadLine();

            var users = db.Users;
            Console.WriteLine("Object List");
            foreach (User u in users)
            {
                Console.WriteLine("{0}.{1} - {2} - {3}", u.Id, u.Login, u.Password, u.Email);
            }
            Console.WriteLine(user2.getType());
        }
    }
}
