using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SentenceClient.Models
{
    public class SentenceList
    {
        public List<Sentence> Sentences { get; set; }
    }

    public class Sentence
    {
        public string SentenceItem { get; set; }
    }
}