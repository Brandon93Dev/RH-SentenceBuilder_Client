using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SentenceClient.Models
{
    public class WordList
    {
        public List<WordsSection> WordSections { get; set; }
    }

    public class WordsSection
    {
        public WordTypes WordType { get; set; }
        public List<string> Words { get; set; }
    }
}