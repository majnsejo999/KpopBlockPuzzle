using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace QuickEngine.Extensions
{
	public static class StringExtensions
	{
		public static bool IsNumeric(this string str)
		{
			return !string.IsNullOrEmpty(str) && new Regex("^-?[0-9]*\\.?[0-9]+$").IsMatch(str.Trim());
		}

		public static bool ContainsNumeric(this string str)
		{
			return !string.IsNullOrEmpty(str) && new Regex("[0-9]+").IsMatch(str);
		}

		public static bool IsNullOrEmpty(this string str)
		{
			return string.IsNullOrEmpty(str);
		}

		public static string ToTitleCase(this string str)
		{
			if (str.IsNullOrEmpty())
			{
				return str;
			}
			CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
			TextInfo textInfo = currentCulture.TextInfo;
			return textInfo.ToTitleCase(str);
		}

		public static string UnPascalCase(this string text)
		{
			if (string.IsNullOrEmpty(text))
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder(text.Length * 2);
			stringBuilder.Append(text[0]);
			for (int i = 1; i < text.Length; i++)
			{
				bool flag = char.IsUpper(text[i]);
				bool flag2 = char.IsUpper(text[i - 1]);
				bool flag3 = (text.Length <= i + 1) ? flag2 : (char.IsUpper(text[i + 1]) || char.IsWhiteSpace(text[i + 1]));
				bool flag4 = char.IsWhiteSpace(text[i - 1]);
				if (flag && !flag4 && (!flag3 || !flag2))
				{
					stringBuilder.Append(' ');
				}
				stringBuilder.Append(text[i]);
			}
			return stringBuilder.ToString();
		}

		public static string RemoveDiacritics(this string stIn)
		{
			string text = stIn.Normalize(NormalizationForm.FormD);
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < text.Length; i++)
			{
				switch (CharUnicodeInfo.GetUnicodeCategory(text[i]))
				{
				case UnicodeCategory.NonSpacingMark:
				case UnicodeCategory.SpacingCombiningMark:
				case UnicodeCategory.EnclosingMark:
					continue;
				}
				stringBuilder.Append(text[i]);
			}
			return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
		}

		public static char GetAccent(this string stIn)
		{
			string text = stIn.Normalize(NormalizationForm.FormD);
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < text.Length; i++)
			{
				UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(text[i]);
				if (unicodeCategory == UnicodeCategory.NonSpacingMark)
				{
					stringBuilder.Append(text[i]);
				}
			}
			if (stringBuilder.Length > 0)
			{
				return stringBuilder.ToString().Normalize(NormalizationForm.FormC)[0];
			}
			return '\0';
		}

		public static bool IsDiacriticsed(this string stIn)
		{
			string text = stIn.Normalize(NormalizationForm.FormD);
			for (int i = 0; i < text.Length; i++)
			{
				UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(text[i]);
				if (unicodeCategory == UnicodeCategory.NonSpacingMark)
				{
					return true;
				}
			}
			return false;
		}

		public static string FixNewLine(this string s)
		{
			return s.Replace("\r", "\n").Replace('\u0003'.ToString(), "\n");
		}

		public static string StripTagsRegex(this string source)
		{
			return Regex.Replace(source, "<.*?>", string.Empty);
		}

		public static string StripTagsCharArray(this string source)
		{
			char[] array = new char[source.Length];
			int num = 0;
			bool flag = false;
			foreach (char c in source)
			{
				switch (c)
				{
				case '<':
					flag = true;
					continue;
				case '>':
					flag = false;
					continue;
				}
				if (!flag)
				{
					array[num] = c;
					num++;
				}
			}
			return new string(array, 0, num);
		}

		public static string[] Split(this string s, string separator, StringSplitOptions splitOptions = StringSplitOptions.None)
		{
			return s.Split(new string[1]
			{
				separator
			}, splitOptions);
		}

		public static int OccurenceCount(this string str, string val)
		{
			int num = 0;
			int startIndex = 0;
			while ((startIndex = str.IndexOf(val, startIndex)) >= 0)
			{
				num++;
				startIndex++;
			}
			return num;
		}

		public static int NthIndexOf(this string target, string value, int n)
		{
			string[] array = target.Split(value);
			n--;
			if (n >= 0 && n < array.Length)
			{
				int num = 0;
				for (int i = 0; i <= n; i++)
				{
					num += array[i].Length + value.Length;
				}
				return num - value.Length;
			}
			return -1;
		}

		public static bool Contains(this string source, string toCheck, StringComparison comp = StringComparison.Ordinal)
		{
			return source.IndexOf(toCheck, comp) >= 0;
		}

		public static bool EndsWith(this string a, string b)
		{
			int num = a.Length - 1;
			int num2 = b.Length - 1;
			while (num >= 0 && num2 >= 0 && a[num] == b[num2])
			{
				num--;
				num2--;
			}
			return (num2 < 0 && a.Length >= b.Length) || (num < 0 && b.Length >= a.Length);
		}

		public static bool StartsWith(this string a, string b)
		{
			int length = a.Length;
			int length2 = b.Length;
			int num = 0;
			int num2 = 0;
			while (num < length && num2 < length2 && a[num] == b[num2])
			{
				num++;
				num2++;
			}
			return (num2 == length2 && length >= length2) || (num == length && length2 >= length);
		}

		public static bool RegexMatch(this string a, string b)
		{
			Regex regex = new Regex(a);
			return regex.Match(b).Success;
		}
	}
}
