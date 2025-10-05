using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

namespace BlockGame.Nova.Conf
{
	public static class SuperSaleConfig
	{
		private static int superSaleTime;

		private static int superSaleCDTime;

		private static Dictionary<string, int>[] sales;

		public static int SuperSaleTime => superSaleTime;

		public static int SuperSaleCDTime => superSaleCDTime;

		public static Dictionary<string, int>[] Sales => sales;

		public static void Init()
		{
			LoadSuperSaleTime();
			LoadSuperSaleConfig();
		}

		public static void LoadSuperSaleTime()
		{
			string path = "Infos/SuperSaleTime";
			TextAsset textAsset = Resources.Load(path) as TextAsset;
			Dictionary<string, object> dictionary = JsonConvert.DeserializeObject(textAsset.text) as Dictionary<string, object>;
			List<object> list = (List<object>)dictionary["data"];
			for (int i = 0; i < list.Count; i++)
			{
				Dictionary<string, object> dictionary2 = (Dictionary<string, object>)list[i];
				if (i == 0)
				{
					superSaleTime = int.Parse(dictionary2["Time"].ToString());
				}
				else
				{
					superSaleCDTime = int.Parse(dictionary2["Time"].ToString());
				}
			}
		}

		public static void LoadSuperSaleConfig()
		{
			string path = "Infos/SuperSaleConfig";
			TextAsset textAsset = Resources.Load(path) as TextAsset;
			Dictionary<string, object> dictionary = JsonConvert.DeserializeObject(textAsset.text) as Dictionary<string, object>;
			List<object> list = (List<object>)dictionary["data"];
			sales = new Dictionary<string, int>[list.Count];
			for (int i = 0; i < list.Count; i++)
			{
				Dictionary<string, object> dictionary2 = (Dictionary<string, object>)list[i];
				Dictionary<string, int> dictionary3 = new Dictionary<string, int>();
				foreach (KeyValuePair<string, object> item in dictionary2)
				{
					string key = item.Key;
					int value = int.Parse(item.Value.ToString());
					dictionary3.Add(key, value);
					sales[i] = dictionary3;
				}
			}
		}
	}
}
