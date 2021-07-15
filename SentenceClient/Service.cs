using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SentenceClient.Models;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Configuration;
using SentenceClient.Helpers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SentenceClient
{
    public class Service : IService
    {
        public async Task<WordsSection> GetWordSectionAsync(WordTypes wordType)
        {
            string address = ConfigurationManager.AppSettings["HostAddress"];
            int wordTypeVal = DataMapper.GetWordTypeValue(wordType);
            WordsSection section = new WordsSection();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(address);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string endpoint = $"api/sentence/{wordTypeVal}";
                HttpResponseMessage response = await client.GetAsync(endpoint);
                if (response.IsSuccessStatusCode)
                {
                    var unserialised = response.Content.ReadAsStringAsync().Result;
                    section.Words = JsonConvert.DeserializeObject<List<string>>(unserialised);
                }
            }
            return section;
        }

        public WordList GetAllWordLists()
        {
            WordList wl = new WordList();
            wl.WordSections.AddRange(new List<WordsSection>()
            {
                new WordsSection() { WordType = WordTypes.Adjectives, Words = GetWordSectionAsync(WordTypes.Adjectives).Result.Words}
                ,
                //new WordsSection() { WordType = WordTypes.Adverbs, Words = GetWordSectionAsync(WordTypes.Adverbs).Result.Words},
                //new WordsSection() { WordType = WordTypes.Conjunctions, Words = GetWordSectionAsync(WordTypes.Conjunctions).Result.Words},
                //new WordsSection() { WordType = WordTypes.Determiners, Words = GetWordSectionAsync(WordTypes.Determiners).Result.Words},
                //new WordsSection() { WordType = WordTypes.Exclamations, Words = GetWordSectionAsync(WordTypes.Exclamations).Result.Words},
                //new WordsSection() { WordType = WordTypes.Nouns, Words = GetWordSectionAsync(WordTypes.Nouns).Result.Words},
                //new WordsSection() { WordType = WordTypes.Prepositions, Words = GetWordSectionAsync(WordTypes.Prepositions).Result.Words},
                //new WordsSection() { WordType = WordTypes.Pronouns, Words = GetWordSectionAsync(WordTypes.Pronouns).Result.Words},
                //new WordsSection() { WordType = WordTypes.Verbs, Words = GetWordSectionAsync(WordTypes.Verbs).Result.Words}
            });

            return wl;
        }
    }
}