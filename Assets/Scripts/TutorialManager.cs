using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager
{
	private const string TUTORIAL_CONFIG_PATH = "Configs/TutorialConfig";

	private static Dictionary<string, int>[][] tutorialInfo;

	public static Dictionary<string, int>[][] TutorialInfo => tutorialInfo;

	public static void LoadTutorialInfo()
	{
		TextAsset textAsset = Resources.Load("Configs/TutorialConfig") as TextAsset;
		JObject jObject = JObject.Parse(textAsset.text);
		int count = jObject.Count;
		tutorialInfo = new Dictionary<string, int>[count][];
		for (int i = 0; i < count; i++)
		{
			string propertyName = "Tutorial" + i;
			string json = jObject[propertyName].ToString();
			JArray jArray = JArray.Parse(json);
			Dictionary<string, int>[] array = new Dictionary<string, int>[jArray.Count];
			tutorialInfo[i] = array;
			for (int j = 0; j < jArray.Count; j++)
			{
				JToken jToken = jArray[j];
				array[j] = new Dictionary<string, int>();
				foreach (JProperty item in (IEnumerable<JToken>)jToken)
				{
					string name = item.Name;
					int value = item.Value.ToObject<int>();
					array[j].Add(name, value);
				}
			}
		}
	}
}
