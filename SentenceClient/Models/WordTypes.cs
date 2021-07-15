using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SentenceClient.Models
{
    public enum WordTypes: int
    {
        Adjectives = 0,
        Adverbs = 1,
        Conjunctions = 2,
        Determiners = 3,
        Exclamations = 4,
        Nouns = 5,
        Prepositions = 6,
        Pronouns = 7,
        Verbs = 8
    }
}