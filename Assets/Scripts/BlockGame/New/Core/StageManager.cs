using Newtonsoft.Json.Linq;
using BlockGame.GameEngine.Libs.Log;
using System.Collections.Generic;
using UnityEngine;

namespace BlockGame.New.Core
{
	public class StageManager
	{
		public int stageNum;

		private Dictionary<int, StageConfig> stageList = new Dictionary<int, StageConfig>();

		private static StageManager instance;

		public static StageManager Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new StageManager();
				}
				return instance;
			}
		}

		public Dictionary<int, StageConfig> StageList
		{
			get
			{
				return stageList;
			}
			set
			{
				stageList = value;
			}
		}

		public void LoadStageData()
		{
			LoadGeneralConfig();
			LoadStageScoreConfig();
			LoadStageShapeConfig();
		}

		public void LoadGeneralConfig()
		{
			TextAsset textAsset = Resources.Load("Configs/StageConfig") as TextAsset;
			JObject jObject = JObject.Parse(textAsset.text);
			JArray jArray = JArray.Parse(jObject["data0"].ToString());
			stageNum = jArray.Count;
			foreach (JToken item in jArray)
			{
				StageConfig stageConfig = new StageConfig();
				stageConfig.rowSize = item["Row"].Value<int>();
				stageConfig.colSize = item["Col"].Value<int>();
				stageConfig.obstacleFrenquency = item["ObstacleFrequench"].Value<int>();
				StageList[item["StageID"].Value<int>()] = stageConfig;
			}
		}

		public void LoadStageScoreConfig()
		{
			TextAsset textAsset = Resources.Load("Configs/StageScore") as TextAsset;
			JObject jObject = JObject.Parse(textAsset.text);
			JArray jArray = JArray.Parse(jObject["data0"].ToString());
			foreach (JToken item in jArray)
			{
				int num = item["StageID"].Value<int>();
				StageList[num].scoreBase = new Dictionary<int, int>();
				for (int i = 1; i <= 4; i++)
				{
					StageList[num].scoreBase[i] = item["Score" + i].Value<int>();
				}
			}
		}

		public void LoadStageShapeConfig()
		{
			for (int i = 1; i <= stageNum; i++)
			{
				TextAsset textAsset = Resources.Load("Configs/StageShape_" + i) as TextAsset;
				JObject jObject = JObject.Parse(textAsset.text);
				JArray jArray = JArray.Parse(jObject["data" + (i - 1)].ToString());
				StageList[i].difficultyLevel = new Dictionary<int, Dictionary<int, int>>();
				foreach (JToken item in jArray)
				{
					int num = item["DifficultyLevel"].Value<int>();
					StageList[i].difficultyLevel[num] = new Dictionary<int, int>();
					for (int j = 1; j <= 3; j++)
					{
						StageList[i].difficultyLevel[num][j] = item["Group" + j].Value<int>();
					}
				}
			}
		}
	}
}
