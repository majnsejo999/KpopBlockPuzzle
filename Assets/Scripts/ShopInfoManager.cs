using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ShopInfoManager
{
	private const string SHOP_CONFIG_PATH = "Configs/ShopConfig";

	public static Dictionary<string, string>[] ShopConfig;

	public static void Init()
	{
		LoadShopConfig();
	}

	public static void LoadShopConfig()
	{
		TextAsset textAsset = Resources.Load("Configs/ShopConfig") as TextAsset;
		JArray jArray = JArray.Parse(textAsset.text);
		int count = jArray.Count;
		ShopConfig = new Dictionary<string, string>[count];
		for (int i = 0; i < count; i++)
		{
			JToken jToken = JToken.Parse(jArray[i].ToString());
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			ShopConfig[i] = dictionary;
			foreach (JProperty item in (IEnumerable<JToken>)jToken)
			{
				string name = item.Name;
				string text = (!(name == "item") && !(name == "num")) ? item.Value.ToObject<float>().ToString() : item.Value.ToObject<int>().ToString();
				dictionary.Add(name, text);
			}
			if (dictionary["item"] == "3")
			{
				dictionary["num"] = "No Ads";
			}
		}
	}
}
