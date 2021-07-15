using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SentenceClient.Models;

namespace SentenceClient.Helpers
{
    public class DataMapper
    {
        //public static WordsSection MapWordSections()
        //{

        //}

        public static int GetWordTypeValue(WordTypes wordType)
        {
            int value = (int)Enum.Parse(typeof(WordTypes), wordType.ToString());
            return value;
        }

        public static string GetWordTypeName(WordTypes wordType)
        {
            string name = Enum.GetName(typeof(WordTypes), wordType);
            return name;
        }
    }
}