using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace QuickEngine.Extensions
{
	public static class IListExtensions
	{
		public static bool IsNullOrEmpty<T>(this IList<T> items)
		{
			return items == null || !items.Any();
		}

		public static bool NotNullOrEmpty<T>(this IList<T> items)
		{
			return items?.Any() ?? false;
		}

		public static void Shuffle<T>(this IList<T> list)
		{
			RNGCryptoServiceProvider rNGCryptoServiceProvider = new RNGCryptoServiceProvider();
			int num = list.Count;
			while (num > 1)
			{
				byte[] array = new byte[1];
				do
				{
					rNGCryptoServiceProvider.GetBytes(array);
				}
				while (array[0] >= num * (255 / num));
				int index = (int)array[0] % num;
				num--;
				T value = list[index];
				list[index] = list[num];
				list[num] = value;
			}
		}
	}
}
