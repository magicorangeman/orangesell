using System;
using System.Collections.Generic;

namespace TextAnalysis
{
    static class TextGeneratorTask
    {
        public static string ContinuePhrase(
            Dictionary<string, string> nextWords,
            string phraseBeginning,
            int wordsCount)
        {
            var sepsWords = new char[] { ',', '—', '^', '#', '$', '-', '+', '=', '1', ' ', '\t', '\n', '\r', '\"', '…' };
            var wordsArray = phraseBeginning.Split(sepsWords, StringSplitOptions.RemoveEmptyEntries);
            string lastWord = "";
            string precedingWord = "";

            if (wordsArray.Length > 1)
            {
                lastWord = wordsArray[wordsArray.Length - 1];
                precedingWord = wordsArray[wordsArray.Length - 2];
                
            }
            else if (wordsArray.Length == 1)
            {
                lastWord = wordsArray[wordsArray.Length - 1];
            }

            for (int i = 0; i < wordsCount; i++)
            {
                if ((precedingWord.Length > 0) && (lastWord.Length > 0))
                {
                    var temp = lastWord;
                    lastWord = GetNextWord(nextWords, precedingWord, lastWord);
                    precedingWord = temp;
                    if (lastWord.Length > 0) phraseBeginning += ' ' + lastWord;
                }
  
                else if (lastWord.Length > 0)
                {
                    var temp = lastWord;
                    lastWord = GetNextWord(nextWords, lastWord);
                    precedingWord = temp;
                    if (lastWord.Length > 0) phraseBeginning += ' ' + lastWord;
                }

                else break;
            }

            return phraseBeginning;
        }

        public static string GetNextWord(Dictionary <string, string> nextWords, string precedingWord, string lastWord)
        {
            if (nextWords.ContainsKey(precedingWord + ' ' + lastWord))
                return nextWords[precedingWord + ' ' + lastWord];
            else if (nextWords.ContainsKey(lastWord))
                return nextWords[lastWord];
            else return "";
        }

        public static string GetNextWord(Dictionary<string, string> nextWords, string lastWord)
        {
            if (nextWords.ContainsKey(lastWord))
                return nextWords[lastWord];
            else return "";
        }
    }
}