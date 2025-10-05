using System.Text.RegularExpressions;

namespace QuickEngine.Utils
{
	public static class QIPValidator
	{
		public static bool IsValidIPAddress(string str)
		{
			return Regex.IsMatch(str, "\\b(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\b");
		}
	}
}
