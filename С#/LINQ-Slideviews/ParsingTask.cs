using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace linq_slideviews;

public class ParsingTask
{
	/// <param name="lines">все строки файла, которые нужно распарсить. Первая строка заголовочная.</param>
	/// <returns>Словарь: ключ — идентификатор слайда, значение — информация о слайде</returns>
	/// <remarks>Метод должен пропускать некорректные строки, игнорируя их</remarks>
	public static IDictionary<int, SlideRecord> ParseSlideRecords(IEnumerable<string> lines)
	{
		if (lines.Count() == 0) return new Dictionary<int, SlideRecord>();
		var list = lines.Skip(1).ToList();
		var result = new Dictionary<int, SlideRecord>();
		SlideType slideRecord;
		result = list
			.Select(x => x.Split(';'))
			.Where(x => x.Length == 3)
			.Where(x => int.TryParse(x[0], out _))
            .Where(x => x[1].Any(y => char.IsLetter(y) && x[1].Length > 1))
            .Where(x => Enum.TryParse(Capitalize(x[1]), true, out slideRecord))
			.Select(x => (int.Parse(x[0]), (SlideType)Enum.Parse(typeof(SlideType), Capitalize(x[1])), x[2]))
			.ToDictionary(x => x.Item1, x => new SlideRecord(x.Item1, x.Item2, x.Item3));
		return result;
	}

	/// <param name="lines">все строки файла, которые нужно распарсить. Первая строка — заголовочная.</param>
	/// <param name="slides">Словарь информации о слайдах по идентификатору слайда. 
	/// Такой словарь можно получить методом ParseSlideRecords</param>
	/// <returns>Список информации о посещениях</returns>
	/// <exception cref="FormatException">Если среди строк есть некорректные</exception>
	public static IEnumerable<VisitRecord> ParseVisitRecords(
		IEnumerable<string> lines, IDictionary<int, SlideRecord> slides)
	{
		var result = lines
			.Skip(1)
			.Select(x => x.Split(";"))
			.Where(x => IsCorrectFormatVisits(x, slides))
			.Select(x => (int.Parse(x[0]), int.Parse(x[1]),	
					DateTime.ParseExact(x[2] + " " + x[3], "yyyy-MM-dd HH:mm:ss", default)))
			.Select(x => new VisitRecord(x.Item1, x.Item2, x.Item3, slides[x.Item2].SlideType))
			.ToList();
		return result;
	}

	public static string Capitalize(string word) => char.ToUpper(word[0]) + word[1..];
	public static bool IsCorrectFormatVisits(string[] line, IDictionary<int, SlideRecord> slides)
    {
        var message = string.Format("Wrong line [{0}]", string.Join(";", line));
		if (line.Length == 4 && int.TryParse(line[0], out _) && int.TryParse(line[1], out _) &&
			DateTime.TryParseExact(line[2] + " " + line[3], "yyyy-MM-dd HH:mm:ss", default, DateTimeStyles.None, out _) &&
			slides.ContainsKey(int.Parse(line[1])))
				return true;

        throw new FormatException(message);
	}
}