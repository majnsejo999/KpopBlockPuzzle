using System;
using System.Collections.Generic;

namespace QuickEngine.Extensions
{
	public static class DictionaryExtenstions
	{
		public static bool AddIfKeyNotPresent<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value)
		{
			try
			{
				dict.Add(key, value);
			}
			catch (Exception)
			{
				return false;
			}
			return true;
		}

		public static void AddOrUpdate<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value)
		{
			try
			{
				dict.Add(key, value);
			}
			catch (Exception)
			{
				dict[key] = value;
			}
		}

		public static bool TryAddKey<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value)
		{
			return dict.AddIfKeyNotPresent(key, value);
		}
	}
}
