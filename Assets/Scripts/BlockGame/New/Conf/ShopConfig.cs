using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

namespace BlockGame.Nova.Conf
{
	public static class ShopConfig
	{
		private static int[] coinNum;

		private static string[] coinPrice;

		public static int[] CoinNum => coinNum;

		public static string[] CoinPrice => coinPrice;

		public static void LoadShopConfig()
		{
			string path = "Infos/ShopConfig";
			TextAsset textAsset = Resources.Load(path) as TextAsset;
			Dictionary<string, object> dictionary = JsonConvert.DeserializeObject(textAsset.text) as Dictionary<string, object>;
			List<object> list = (List<object>)dictionary["data"];
			coinNum = new int[list.Count];
			coinPrice = new string[list.Count];
			for (int i = 0; i < list.Count; i++)
			{
				Dictionary<string, object> dictionary2 = (Dictionary<string, object>)list[i];
				foreach (KeyValuePair<string, object> item in dictionary2)
				{
					string key = item.Key;
					if (key == "coinNum")
					{
						coinNum[i] = int.Parse(item.Value.ToString());
					}
					else
					{
						coinPrice[i] = item.Value.ToString();
					}
				}
			}
		}
	}
}
