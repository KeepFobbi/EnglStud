using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglStud_Server.Entities
{
    public class User
    {
        [NotMapped]
        public int type { get; set; }
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public User() { }
        public User(string Login, string Password, string Email)
        {
            this.Login = Login;
            this.Password = Password;
            this.Email = Email;
        }
        public User(int Type, string Login, string Password, string Email)
        {
            type = Type;
            this.Login = Login;
            this.Password = Password;
            this.Email = Email;
        }
        public User(int Type, string Login, string Password)
        {
            type = Type;
            this.Login = Login;
            this.Password = Password;            
        }
    }
}
