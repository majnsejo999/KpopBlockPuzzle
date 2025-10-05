using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace QuickEngine.Utils
{
	public static class QEmailValidator
	{
		private static bool isInvalid;

		[CompilerGenerated]
		private static MatchEvaluator _003C_003Ef__mg_0024cache0;

		public static bool IsValidEmail(string emailString)
		{
			isInvalid = false;
			if (string.IsNullOrEmpty(emailString))
			{
				return false;
			}
			emailString = Regex.Replace(emailString, "(@)(.+)$", DomainMapper, RegexOptions.None);
			if (isInvalid)
			{
				return false;
			}
			return Regex.IsMatch(emailString, "^(?(\")(\".+?(?<!\\\\)\"@)|(([0-9a-z]((\\.(?!\\.))|[-!#\\$%&'\\*\\+/=\\?\\^`\\{\\}\\|~\\w])*)(?<=[0-9a-z])@))(?(\\[)(\\[(\\d{1,3}\\.){3}\\d{1,3}\\])|(([0-9a-z][-\\w]*[0-9a-z]*\\.)+[a-z0-9][\\-a-z0-9]{0,22}[a-z0-9]))$", RegexOptions.IgnoreCase);
		}

		private static string DomainMapper(Match match)
		{
			IdnMapping idnMapping = new IdnMapping();
			string text = match.Groups[2].Value;
			try
			{
				text = idnMapping.GetAscii(text);
			}
			catch (Exception)
			{
				isInvalid = true;
			}
			return match.Groups[1].Value + text;
		}
	}
}
