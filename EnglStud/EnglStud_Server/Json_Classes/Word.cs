using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglStud_Server.Json_Classes
{
    class Word
    {
        public int Id { get; set; }
        public int IdKnowWords { get; set; }
        public int IdWordUnStudy { get; set; }
        public int UserId { get; set; }       


        public Word() { }
        public Word(int IdKnowWords, int IdWordUnStudy, int UserId)
        {
            this.IdKnowWords = IdKnowWords;
            this.IdWordUnStudy = IdWordUnStudy;
            this.UserId = UserId;
        }
    }
}
