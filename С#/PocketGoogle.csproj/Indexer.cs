using System;
using System.Collections.Generic;
using System.Linq;

namespace PocketGoogle
{
    public class Indexer : IIndexer
    {
        public List<Document> Data;
        public void Add(int id, string documentText)
        {
            var wordsSentences = new char[] { ' ', '.', ',', '!', '?', ':', '-', '\r', '\n' };
            var wordsCatalog = documentText.Split(wordsSentences, StringSplitOptions.RemoveEmptyEntries);
            var catalog =  wordsCatalog
                            .GroupBy(word => word)
                            .ToDictionary(group => group.Key,
                                                   group => group
                                                            .Select(x => Array.IndexOf(wordsCatalog, x))
                                                            .ToList());
            if (Data is null) Data = new List<Document> { new Document(id, catalog) };
            else Data.Add(new Document(id, catalog));
        }

        public List<int> GetIds(string word)
        {
            return Data
                .Where(document => document.Catalog.ContainsKey(word))
                .Select(document => document.Id).OrderBy(x => x)
                .ToList();
        }

        public List<int> GetPositions(int id, string word)
        {
            return Data
                .Where(document => document.Id == id)
                .Select(document => document.Catalog)
                .SelectMany(x => x[word])
                .ToList();
        }

        public void Remove(int id)
        {
            var newData = Data;
            foreach (var document in newData)
                if (document.Id == id)
                    newData.Remove(document);
            Data = newData;
        }
    }

    public class Document
    {
	    public int Id;
	    public Dictionary<string, List<int>> Catalog;

        public Document(int id, Dictionary<string, List<int>> catalog)
        {
            Id = id;
            Catalog = catalog;
        }
    }
}
