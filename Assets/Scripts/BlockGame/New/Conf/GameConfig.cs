using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

namespace BlockGame.Nova.Conf
{
	public static class GameConfig
	{
		private static int totalLevel;

		private static int totalChapter;

		private static int rewardVideoLimit = 5;

		private static int[] startLevels;

		public static int RewardVideoStartLevel;

		public static int SuperSaleStartLevel = 1;

		public static int ExtraWordShowLevel = 2;

		public static int MinimumLevelOfSpin = 3;

		public static int MinimumLevelOfDaily = 4;

		public static int InterstitialAd = 5;

		private static int[] prices;

		public static int StartCoin;

		public static int LetterCoin = 1;

		public static int RewardVideoCoinNum = 2;

		public static int RewardUpdateCoinNum = 3;

		public static int HintPrice = 4;

		public static int DailyReward = 5;

		public static int FacebookLoginReward = 6;

		public static int SendGiftReward = 7;

		private static string[] chapterThemes;

		private static int[] chapterRewards;

		private static int[] chapterStartLevels;

		private static int[] chapterEndLevels;

		private static int[] chapterLettersNum;

		public static int TotalLevel => totalLevel;

		public static int TotalChapter => totalChapter;

		public static int RewardVideoLimit => rewardVideoLimit;

		public static string[] ChapterThemes => chapterThemes;

		public static int[] ChapterRewards => chapterRewards;

		public static int[] ChapterStartLevels => chapterStartLevels;

		public static int[] ChapterEndLevels => chapterEndLevels;

		public static int[] ChapterLettersNum => chapterLettersNum;

		public static int[] StartLevels => startLevels;

		public static int[] Prices => prices;

		public static void Init()
		{
			LoadVideoLimit();
			LoadChapterInfos();
			LoadStartLevels();
			LoadPrices();
		}

		public static void LoadVideoLimit()
		{
			string path = "Infos/VideoLimit";
			TextAsset textAsset = Resources.Load(path) as TextAsset;
			Dictionary<string, object> dictionary = JsonConvert.DeserializeObject(textAsset.text) as Dictionary<string, object>;
			List<object> list = (List<object>)dictionary["data"];
			for (int i = 0; i < list.Count; i++)
			{
				Dictionary<string, object> dictionary2 = (Dictionary<string, object>)list[i];
				foreach (KeyValuePair<string, object> item in dictionary2)
				{
					rewardVideoLimit = int.Parse(item.Value.ToString());
				}
			}
		}

		public static void LoadChapterInfos()
		{
			string path = "Infos/ChapterConfig";
			TextAsset textAsset = Resources.Load(path) as TextAsset;
			Dictionary<string, object> dictionary = JsonConvert.DeserializeObject(textAsset.text) as Dictionary<string, object>;
			List<object> list = (List<object>)dictionary["data"];
			totalChapter = list.Count;
			chapterStartLevels = new int[totalChapter];
			chapterEndLevels = new int[totalChapter];
			chapterLettersNum = new int[totalChapter];
			chapterRewards = new int[totalChapter];
			chapterThemes = new string[totalChapter];
			for (int i = 0; i < list.Count; i++)
			{
				Dictionary<string, object> dictionary2 = (Dictionary<string, object>)list[i];
				foreach (KeyValuePair<string, object> item in dictionary2)
				{
					string a = item.Key.ToString();
					string text = item.Value.ToString();
					if (a == "Chapter_Theme")
					{
						chapterThemes[i] = text;
					}
					if (a == "Begin_Level")
					{
						chapterStartLevels[i] = int.Parse(text);
					}
					if (a == "End_Level")
					{
						chapterEndLevels[i] = int.Parse(text);
					}
					if (a == "Letters_Num")
					{
						chapterLettersNum[i] = int.Parse(text);
					}
					if (a == "Reward")
					{
						chapterRewards[i] = int.Parse(text);
					}
				}
			}
			totalLevel = chapterEndLevels[totalChapter - 1];
		}

		public static void LoadStartLevels()
		{
			string path = "Infos/StartLevels";
			TextAsset textAsset = Resources.Load(path) as TextAsset;
			Dictionary<string, object> dictionary = JsonConvert.DeserializeObject(textAsset.text) as Dictionary<string, object>;
			List<object> list = (List<object>)dictionary["data"];
			startLevels = new int[list.Count];
			for (int i = 0; i < list.Count; i++)
			{
				Dictionary<string, object> dictionary2 = (Dictionary<string, object>)list[i];
				foreach (KeyValuePair<string, object> item in dictionary2)
				{
					string key = item.Key;
					int num = int.Parse(item.Value.ToString());
					startLevels[i] = num;
				}
			}
		}

		public static void LoadPrices()
		{
			string path = "Infos/Prices";
			TextAsset textAsset = Resources.Load(path) as TextAsset;
			Dictionary<string, object> dictionary = JsonConvert.DeserializeObject(textAsset.text) as Dictionary<string, object>;
			List<object> list = (List<object>)dictionary["data"];
			prices = new int[list.Count];
			for (int i = 0; i < list.Count; i++)
			{
				Dictionary<string, object> dictionary2 = (Dictionary<string, object>)list[i];
				foreach (KeyValuePair<string, object> item in dictionary2)
				{
					string key = item.Key;
					int num = int.Parse(item.Value.ToString());
					prices[i] = num;
				}
			}
		}

		public static int GetLevelsNum(int chapter)
		{
			UnityEngine.Debug.Log("Chapter: " + chapter);
			return chapterEndLevels[chapter] - chapterStartLevels[chapter] + 1;
		}

		public static bool CheckIfFirstLevelOfChapter(int level)
		{
			return false;
		}
	}
}
