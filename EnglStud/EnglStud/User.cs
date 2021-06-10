using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglStud
{
    class User
    {
        public int id { get; set; }
        private string login, pass, email;

        public string Login
        {
            get { return login; }
            set { login = value; }
        } 
        public string Email
        {
            get { return email; }
            set { email = value; }
        } 
        public string Pass
        {
            get { return pass; }
            set { pass = value; }
        }

        public User() { }

        public User(string login, string email, string pass)
        {
            this.login = login;
            this.pass = pass;
            this.email = email;
        }



        //public override string ToString()
        //{
        //    return "User: " + Login + " Email: " + Email;
        //}
    }
}
