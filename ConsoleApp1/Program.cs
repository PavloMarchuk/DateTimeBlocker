using System;
using System.Linq;

using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
		private static void Main(string[] args)
		{
			DateTime minDate = new DateTime(2015, 1, 1), maxDate = DateTime.Today;

			var availableRanges = new[]
			{
				new[]
				{
					new DateTime(2015, 2, 4),
					new DateTime(2015, 2, 12),
				},
				new[]
				{
					new DateTime(2015, 8, 20),
					new DateTime(2015, 8, 20),
				},
				new[]
				{
					new DateTime(2015, 4, 15),
					new DateTime(2015, 5, 5),
				},
				new[]
				{
					new DateTime(2015, 10, 5),
					DateTime.Today.AddDays(-1)
				}
			};

			var blockedRanges = invertDateRanges(availableRanges, minDate, maxDate);

			foreach (var range in blockedRanges)
			{
				Console.WriteLine("{0:dd.MM.yyyy} - {1:dd.MM.yyyy}", range[0], range[1]);
			}
			Console.ReadKey();

		}
		private static DateTime[][] invertDateRanges(DateTime[][] inputRanges, DateTime minDate, DateTime maxDate)
		{
			//сортування масиву по першій даті
			IOrderedEnumerable<DateTime[]> inputOrdered = inputRanges.OrderBy(d => d.First());

			//ініціалізація результуючої колекції
			List<DateTime[]> result = new List<DateTime[]>();

			foreach (DateTime[] item in inputOrdered)
			{
				//якщо перші періоди менше мінімального, пропустити ітерацію.
				// або дозволені періоди накладаються один на одного
				if (minDate > item[0].AddDays(-1))
				{
					minDate = item[1].AddDays(1);
					continue;
				}
				// створення заблокованого проміжку
				DateTime[] blocked = { minDate, item[0].AddDays(-1) };
				// обчислення і перенесення першого дня наступного проміжку на наступну ітерацю.
				minDate = item[1].AddDays(1);

				result.Add(blocked);
			}
			// додавання останнього проміжку часу
			if (maxDate > result.Last()[1])
			{
				DateTime[] last = { result.Last()[1], maxDate };
				result.Add(last);
			}
			return result.ToArray();
		}
	}
}
