using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SentenceClient.Models;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Web.Configuration;
using SentenceClient.Helpers;
using System.Threading.Tasks;
using Microsoft.Ajax.Utilities;
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
                HttpResponseMessage response = await client.GetAsync(endpoint).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var unserialized = response.Content.ReadAsStringAsync().Result;
                    section.Words = JsonConvert.DeserializeObject<List<string>>(unserialized);
                }
            }
            return section;
        }

        public bool PostSentence(string sentence)
        {
            string address = ConfigurationManager.AppSettings["HostAddress"];
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(address);
                //POST
                var post = client.PostAsJsonAsync("api/sentence", 
                    new PostVals() { value = sentence }).Result;
                if (post.IsSuccessStatusCode)
                    return true;
                else
                    return false;
            }
        }

        public WordList GetAllWordLists()
        {
            WordList wl = new WordList();
            wl.WordSections = new List<WordsSection>();

            wl.WordSections.AddRange(new List<WordsSection>()
            {
                new WordsSection() { WordType = WordTypes.Adjectives, Words = Task.Run(() => GetWordSectionAsync(WordTypes.Adjectives)).Result.Words},
                new WordsSection() { WordType = WordTypes.Adverbs, Words = Task.Run(() => GetWordSectionAsync(WordTypes.Adverbs)).Result.Words},
                new WordsSection() { WordType = WordTypes.Conjunctions, Words = Task.Run(() => GetWordSectionAsync(WordTypes.Conjunctions)).Result.Words},
                new WordsSection() { WordType = WordTypes.Determiners, Words = Task.Run(() => GetWordSectionAsync(WordTypes.Determiners)).Result.Words},
                new WordsSection() { WordType = WordTypes.Exclamations, Words = Task.Run(() => GetWordSectionAsync(WordTypes.Exclamations)).Result.Words},
                new WordsSection() { WordType = WordTypes.Nouns, Words = Task.Run(() => GetWordSectionAsync(WordTypes.Nouns)).Result.Words},
                new WordsSection() { WordType = WordTypes.Prepositions, Words = Task.Run(() => GetWordSectionAsync(WordTypes.Prepositions)).Result.Words},
                new WordsSection() { WordType = WordTypes.Pronouns, Words = Task.Run(() => GetWordSectionAsync(WordTypes.Pronouns)).Result.Words},
                new WordsSection() { WordType = WordTypes.Verbs, Words = Task.Run(() => GetWordSectionAsync(WordTypes.Verbs)).Result.Words}
            });

            return wl;
        }

        public SentenceList GetAllSentences()
        {
            SentenceList sl = new SentenceList();
            sl.Sentences = Task.Run(() => GetSentenceList()).Result;
            return sl;
        }

        public async Task<List<Sentence>> GetSentenceList()
        {
            List<Sentence> ls = new List<Sentence>();
            List<string> init = new List<string>();
            string address = ConfigurationManager.AppSettings["HostAddress"];
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(address);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string endpoint = $"api/sentence";
                HttpResponseMessage response = await client.GetAsync(endpoint).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var unserialised = response.Content.ReadAsStringAsync().Result;
                    init = JsonConvert.DeserializeObject<List<string>>(unserialised);
                }

                foreach (var i in init)
                {
                    ls.Add(new Sentence() { SentenceItem = i });
                }
            }

            return ls;
        }
    }

    public class PostVals
    {
        public string value { get; set; }
    }
}