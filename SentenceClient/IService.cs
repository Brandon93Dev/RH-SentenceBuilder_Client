using SentenceClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentenceClient
{
    public interface IService
    {
        Task<WordsSection> GetWordSectionAsync(WordTypes wordType);
    }
}
