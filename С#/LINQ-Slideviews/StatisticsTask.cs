using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews;

public class StatisticsTask
{
	public static double GetMedianTimePerSlide(List<VisitRecord> visits, SlideType slideType)
	{
		return visits
			.OrderBy(x => x.DateTime)
			.GroupBy(x => x.UserId)
			.Select(group => group.Bigrams())
			.SelectMany(group => group)
			.Where(x => x.First.SlideType == slideType)
			.Select(x => x.Second.DateTime.Subtract(x.First.DateTime))
			.Select(x => x.TotalMinutes)
			.Where(x => x >= 1 && x <= 120)
			.DefaultIfEmpty()
			.Median();
	}
}