using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

namespace BlockGame.Nova.Conf
{
	public static class ExtraWordConfig
	{
		private static Dictionary<string, int>[] extraWordInfo;

		public static Dictionary<string, int>[] ExtraWordInfo => extraWordInfo;

		public static void LoadExtraWordConfig()
		{
			string path = "Infos/ExtraWordConfig";
			TextAsset textAsset = Resources.Load(path) as TextAsset;
			Dictionary<string, object> dictionary = JsonConvert.DeserializeObject(textAsset.text) as Dictionary<string, object>;
			List<object> list = (List<object>)dictionary["data"];
			extraWordInfo = new Dictionary<string, int>[list.Count];
			for (int i = 0; i < list.Count; i++)
			{
				Dictionary<string, object> dictionary2 = (Dictionary<string, object>)list[i];
				Dictionary<string, int> dictionary3 = new Dictionary<string, int>();
				foreach (KeyValuePair<string, object> item in dictionary2)
				{
					string key = item.Key;
					int value = int.Parse(item.Value.ToString());
					dictionary3.Add(key, value);
					extraWordInfo[i] = dictionary3;
				}
			}
		}

		public static int GetRequiredWord(int extraLevel)
		{
			int result = 0;
			for (int i = 0; i < ExtraWordInfo.Length; i++)
			{
				if (i < ExtraWordInfo.Length - 1)
				{
					if (extraLevel < ExtraWordInfo[i + 1]["MinLevel"])
					{
						result = ExtraWordInfo[i]["RequiredWord"];
						break;
					}
					continue;
				}
				result = ExtraWordInfo[ExtraWordInfo.Length - 1]["RequiredWord"];
				break;
			}
			return result;
		}

		public static int GetReward(int extraLevel)
		{
			int result = 0;
			for (int i = 0; i < ExtraWordInfo.Length; i++)
			{
				if (i < ExtraWordInfo.Length - 1)
				{
					if (extraLevel < ExtraWordInfo[i + 1]["MinLevel"])
					{
						result = ExtraWordInfo[i]["Reward"];
						break;
					}
					continue;
				}
				result = ExtraWordInfo[ExtraWordInfo.Length - 1]["Reward"];
				break;
			}
			return result;
		}
	}
}
