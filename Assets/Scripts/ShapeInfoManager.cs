using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ShapeInfoManager
{
	public static readonly int SHAPE_SELECTOR_SLOT = 3;

	private const string SHAPE_CONFIG_PATH = "Configs/Shapes";

	public static ShapeInfo[][] shapeInfo;

	public static void Init()
	{
		LoadShapeInfo();
	}

	private static void LoadShapeInfo()
	{
		TextAsset textAsset = Resources.Load("Configs/Shapes") as TextAsset;
		JObject jObject = JObject.Parse(textAsset.text);
		int count = jObject.Count;
		ShapeInfoManager.shapeInfo = new ShapeInfo[count][];
		for (int i = 0; i < count; i++)
		{
			string propertyName = "shapes" + (i + 1);
			string json = jObject[propertyName].ToString();
			JArray jArray = JArray.Parse(json);
			ShapeInfoManager.shapeInfo[i] = new ShapeInfo[jArray.Count];
			for (int j = 0; j < jArray.Count; j++)
			{
				JToken jToken = jArray[j];
				ShapeInfo shapeInfo = new ShapeInfo();
				ShapeInfoManager.shapeInfo[i][j] = shapeInfo;
				foreach (JProperty item in (IEnumerable<JToken>)jToken)
				{
					string name = item.Name;
					if (name == "id")
					{
						shapeInfo.Id = item.Value.ToObject<int>();
					}
					else if (name == "rows")
					{
						shapeInfo.Rows = item.Value.ToObject<int>();
					}
					else if (name == "cols")
					{
						shapeInfo.Cols = item.Value.ToObject<int>();
					}
					else if (name == "grid")
					{
						shapeInfo.Grids = JsonConvert.DeserializeObject<int[]>(item.Value.ToString());
					}
				}
			}
		}
	}

	public static ShapeInfo GetShapeInfoById(int id)
	{
		for (int i = 0; i < shapeInfo.Length; i++)
		{
			for (int j = 0; j < shapeInfo[i].Length; j++)
			{
				if (id == shapeInfo[i][j].Id)
				{
					return shapeInfo[i][j];
				}
			}
		}
		return shapeInfo[0][0];
	}
}
