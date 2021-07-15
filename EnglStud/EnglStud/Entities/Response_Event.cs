using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglStud.Entities
{
    class Response_Event
    {
        public string message { get; set; }
        public int Id { get; set; }

        public Response_Event() { }
        public Response_Event(string message) { this.message = message; }
        public Response_Event(int Id) { this.Id = Id; }
        public Response_Event(int Id, string message) 
        { 
            this.Id = Id;
            this.message = message;
        }
    }
}
