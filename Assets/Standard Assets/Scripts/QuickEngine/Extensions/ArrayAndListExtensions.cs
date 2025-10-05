using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace QuickEngine.Extensions
{
	public static class ArrayAndListExtensions
	{
		public static bool IsNullOrEmpty<T>(this T[] array)
		{
			return array == null || array.Length == 0;
		}

		public static bool IsNullOrEmpty<T>(this List<T> list)
		{
			return list == null || list.Count == 0;
		}

		public static bool IsNullOrEmpty<TKey, TValue>(this Dictionary<TKey, TValue> dict)
		{
			return dict == null || dict.Count == 0;
		}

		public static T GetRandomElement<T>(this T[] array)
		{
			return array[Random.Range(0, array.Length)];
		}

		public static T GetRandomElement<T>(this List<T> list)
		{
			return list[Random.Range(0, list.Count)];
		}

		public static void ShuffleArray<T>(this T[] array)
		{
			for (int num = array.Length - 1; num > 0; num--)
			{
				int num2 = Random.Range(0, num);
				T val = array[num];
				array[num] = array[num2];
				array[num2] = val;
			}
		}

		public static void ShuffleList<T>(this List<T> list)
		{
			for (int num = list.Count - 1; num > 0; num--)
			{
				int index = Random.Range(0, num);
				T value = list[num];
				list[num] = list[index];
				list[index] = value;
			}
		}

		public static string ToString<T>(this T[] array, string separator)
		{
			if (array.IsNullOrEmpty())
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < array.Length - 1; i++)
			{
				stringBuilder.Append(array[i].ToString());
				stringBuilder.Append(separator);
			}
			stringBuilder.Append(array[array.Length - 1].ToString());
			return stringBuilder.ToString();
		}

		public static string ToString<T>(this List<T> list, string separator)
		{
			if (list.IsNullOrEmpty())
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < list.Count - 1; i++)
			{
				stringBuilder.Append(list[i].ToString());
				stringBuilder.Append(separator);
			}
			stringBuilder.Append(list[list.Count - 1].ToString());
			return stringBuilder.ToString();
		}
	}
}
