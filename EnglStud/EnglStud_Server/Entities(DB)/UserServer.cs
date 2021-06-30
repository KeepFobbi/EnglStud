using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglStud_Server.Entities
{
    public class User
    {
        private int type;
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public void setType(int Type)
        {
            type = Type;
        }
        public int getType()
        {
            return type;
        }

        public User() { }
        public User(string Login, string Password, string Email)
        {
            this.Login = Login;
            this.Password = Password;
            this.Email = Email;
        }
        public User(int Type, string Login, string Password, string Email)
        {
            setType (Type);
            this.Login = Login;
            this.Password = Password;
            this.Email = Email;
        }
        public User(int Type, string Login, string Password)
        {
            setType(Type);
            this.Login = Login;
            this.Password = Password;            
        }
    }
}
