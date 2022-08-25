using System;
using System.Collections.Generic;
using System.Text;

namespace TextAnalysis
{
    static class SentencesParserTask
    {
        public static List<List<string>> ParseSentences(string text)
        {
            var sentencesList = new List<List<string>>();
            var separatorsSentences = new char[] { '.', '!', '?', ':', ';', '(', ')' };
            var sentencesArray = text.Split(separatorsSentences, StringSplitOptions.RemoveEmptyEntries);
            foreach (var sentence in sentencesArray)
                sentencesList.Add(ParseWords(sentence));
            sentencesList.RemoveAll(x => x == null || x.Count == 0);
            return sentencesList;
        }

        public static List<string> ParseWords(string sentence)
        {
            var wordsList = new List<string>();
            var sepsWords = new char[] { ',', '—', '^', '#', '$', '-', '+', '=', '1', ' ', '\t', '\n', '\r', '\"', '…' };
            var wordsArray = sentence.Split(sepsWords, StringSplitOptions.RemoveEmptyEntries);
            foreach (string word in wordsArray)
            {
                string rightWord = WordRight(word);
                if (rightWord.Length > 0)
                    wordsList.Add(WordRight(word).ToLower());
            }
            return wordsList;
        }

        public static string WordRight(string word)
        {
            var result = new StringBuilder();
            foreach (var c in word)
                if ((char.IsLetter(c)) || (c == '\''))
                    result.Append(c);
            return result.ToString();
        }
    }
}