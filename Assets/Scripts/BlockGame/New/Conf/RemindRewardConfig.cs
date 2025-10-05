using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

namespace BlockGame.Nova.Conf
{
	public static class RemindRewardConfig
	{
		private static Dictionary<int, int> rewardInfo = new Dictionary<int, int>();

		public static Dictionary<int, int> RewardInfo => rewardInfo;

		public static void LoadRemindRewardConfig()
		{
			string path = "Infos/RemindRewardConfig";
			TextAsset textAsset = Resources.Load(path) as TextAsset;
			Dictionary<string, object> dictionary = JsonConvert.DeserializeObject(textAsset.text) as Dictionary<string, object>;
			List<object> list = (List<object>)dictionary["data"];
			for (int i = 0; i < list.Count; i++)
			{
				Dictionary<string, object> dictionary2 = (Dictionary<string, object>)list[i];
				rewardInfo.Add(int.Parse(dictionary2["Days"].ToString()), int.Parse(dictionary2["Reward"].ToString()));
			}
		}
	}
}
