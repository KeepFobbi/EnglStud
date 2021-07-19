using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglStud.Entities
{
    class WordsToServer
    {
        public int Id { get; set; }
        public int IdKnowWords { get; set; }
        public int IdWordUnStudy { get; set; }
        public int UserId { get; set; }


        public WordsToServer() { }
        public WordsToServer(int IdKnowWords, int IdWordUnStudy, int UserId)
        {
            this.IdKnowWords = IdKnowWords;
            this.IdWordUnStudy = IdWordUnStudy;
            this.UserId = UserId;
        }
    }
}
