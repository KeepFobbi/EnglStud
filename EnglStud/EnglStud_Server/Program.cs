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
            OpenConnection connection = new OpenConnection();
        }
    }
}
