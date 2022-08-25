using System.Collections.Generic;

namespace TextAnalysis
{
	static class FrequencyAnalysisTask
	{
		public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
		{
			var result = new Dictionary<string, string>();
			var bigramDictionary = GetBigramDictionary(text);
			var trigramDictionary = GetTrigramDictionary(text);
			result = GetOverallDictionary(bigramDictionary, trigramDictionary);
			return result;
		}

		public static Dictionary<string, string> GetBigramDictionary(List<List<string>> text)
        {
			var freqBigram = new Dictionary<string, int>();
			var bigram = new Dictionary<string, string>();
			foreach (var sentence in text)
			{
				for (int i = 0; i < sentence.Count - 1; i++)
				{
					var indexFreq = sentence[i] + ' ' + sentence[i + 1];
					var indexBigram = sentence[i];
					var newValue = sentence[i + 1];
					if (!freqBigram.ContainsKey(indexFreq))
						freqBigram[indexFreq] = 1;
					else freqBigram[indexFreq]++;

					if (!bigram.ContainsKey(sentence[i]))
						bigram[sentence[i]] = newValue;
					else if (freqBigram[indexFreq] > freqBigram[indexBigram + ' ' + bigram[indexBigram]])
						bigram[indexBigram] = newValue;
					else if ((freqBigram[indexFreq] == freqBigram[indexBigram + ' ' + bigram[indexBigram]]) && (string.CompareOrdinal(newValue, bigram[indexBigram]) < 0))
						bigram[indexBigram] = newValue;
				}
			}
			return bigram;
		}

		public static Dictionary<string, string> GetTrigramDictionary(List<List<string>> text)
		{
			var freqTrigram = new Dictionary<string, int>();
			var trigram = new Dictionary<string, string>();
			foreach (var sentence in text)
			{
				for (int i = 0; i < sentence.Count - 2; i++)
				{
					var indexFreq = sentence[i] + ' ' + sentence[i + 1] + ' ' + sentence[i + 2];
					var indexTrigram = sentence[i] + ' ' + sentence[i + 1];
					var newValue = sentence[i + 2];

					if (!freqTrigram.ContainsKey(indexFreq))
						freqTrigram[indexFreq] = 1;
					else freqTrigram[indexFreq]++;

					if (!trigram.ContainsKey(indexTrigram))
						trigram[indexTrigram] = newValue;
					else if (freqTrigram[indexFreq] > freqTrigram[indexTrigram + ' ' + trigram[indexTrigram]])
						trigram[indexTrigram] = newValue;
					else if ((freqTrigram[indexFreq] == freqTrigram[indexTrigram + ' ' + trigram[indexTrigram]]) && (string.CompareOrdinal(newValue, trigram[indexTrigram]) < 0))
						trigram[indexTrigram] = newValue;
				}
			}
			return trigram;
		}

		public static Dictionary<string, string> GetOverallDictionary(Dictionary<string, string> bigramDict, Dictionary<string, string> trigramDict)
        {
			var result = new Dictionary<string, string>();
			foreach (var trigram in trigramDict)
				result.Add(trigram.Key, trigram.Value);
			foreach (var trigram in bigramDict)
				result.Add(trigram.Key, trigram.Value);
			return result;
        }
	}
}