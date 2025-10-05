using System;

namespace QuickEngine.Extensions
{
	public static class DateTimeExtensions
	{
		public static bool IsBetween(this DateTime date, DateTime from, DateTime to)
		{
			return from <= date && to >= date;
		}

		public static DateTime Midnight(this DateTime date)
		{
			return new DateTime(date.Year, date.Month, date.Day);
		}

		public static DateTime FirstOfMonth(this DateTime date)
		{
			return new DateTime(date.Year, date.Month, 1);
		}

		public static DateTime EndOfMonth(this DateTime date)
		{
			return new DateTime(date.Year, date.Month, 1).AddMonths(1).AddDays(-1.0);
		}

		public static DateTime Yesterday(this DateTime date)
		{
			return date.AddDays(-1.0);
		}

		public static DateTime YesterdayMidnight(this DateTime date)
		{
			return date.Yesterday().Midnight();
		}

		public static DateTime Tomorrow(this DateTime date)
		{
			return date.AddDays(1.0);
		}

		public static DateTime TomorrowMidnight(this DateTime date)
		{
			return date.Tomorrow().Midnight();
		}

		public static bool IsSameDay(this DateTime date, DateTime compareDate)
		{
			return date.Midnight().Equals(compareDate.Midnight());
		}

		public static bool IsLaterDate(this DateTime date, DateTime compareDate)
		{
			return date > compareDate;
		}

		public static bool IsOlderDate(this DateTime date, DateTime compareDate)
		{
			return date < compareDate;
		}

		public static bool IsToday(this DateTime date)
		{
			return date.Date == DateTime.Now.Date;
		}

		public static bool IsTomorrow(this DateTime date)
		{
			return date.Date == DateTime.Now.Date.AddDays(1.0);
		}

		public static bool IsYesterday(this DateTime date)
		{
			return date.Date == DateTime.Now.Date.AddDays(-1.0);
		}

		public static string ToDdMmYySlash(this DateTime date)
		{
			return date.ToString("dd/MM/yy");
		}

		public static string ToDdMmYyDot(this DateTime date)
		{
			return date.ToString("dd.MM.yy");
		}

		public static string ToDdMmYyHyphen(this DateTime date)
		{
			return date.ToString("dd-MM-yy");
		}

		public static string ToDdMmYyWithSep(this DateTime date, string separator)
		{
			return date.ToString(string.Format("dd{0}MM{0}yy", separator));
		}

		public static string ToDdMmYyyySlash(this DateTime date)
		{
			return date.ToString("dd/MM/yyyy");
		}

		public static string ToDdMmYyyyDot(this DateTime date)
		{
			return date.ToString("dd.MM.yyyy");
		}

		public static string ToDdMmYyyyHyphen(this DateTime date)
		{
			return date.ToString("dd-MM-yyyy");
		}

		public static string ToDdMmYyyyWithSep(this DateTime date, string separator)
		{
			return date.ToString(string.Format("dd{0}MM{0}yyyy", separator));
		}
	}
}
