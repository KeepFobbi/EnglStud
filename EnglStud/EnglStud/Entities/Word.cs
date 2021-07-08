using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglStud.Entities
{
    class Word
    {
        public int Id { get; set; }
        public string WordInEnglish { get; set; }
        public string Translation { get; set; }

        public Word() { }

        public Word(string WordInEnglish, string Translation)
        {
            this.WordInEnglish = WordInEnglish;
            this.Translation = Translation;
        }
    }
}
