namespace QuickEngine.Extensions
{
	public static class BoolExtensions
	{
		public static bool IsTrue(this bool @bool)
		{
			return @bool;
		}

		public static bool IsFalse(this bool @bool)
		{
			return !@bool;
		}

		public static bool Toggle(this bool @bool)
		{
			return !@bool;
		}

		public static int ToInt(this bool @bool)
		{
			return @bool ? 1 : 0;
		}

		public static string ToLowerString(this bool @bool)
		{
			return @bool.ToString().ToLower();
		}

		public static string ToString(this bool @bool, string trueString, string falseString)
		{
			return @bool.ToType(trueString, falseString);
		}

		public static T ToType<T>(this bool @bool, T trueValue, T falseValue)
		{
			return (!@bool) ? falseValue : trueValue;
		}
	}
}
