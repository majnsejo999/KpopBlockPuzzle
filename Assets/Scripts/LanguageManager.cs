using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using UnityEngine;

public class LanguageManager
{
	public static Dictionary<string, string> LanguageTable = new Dictionary<string, string>();

	public static void Load()
	{
		UnityEngine.Debug.Log("Processing Language Infos...");
		UnityEngine.Debug.Log("Current Lang " + Application.systemLanguage.ToString());
		TextAsset textAsset = Resources.Load("Configs/LanguageConfig") as TextAsset;
		UnityEngine.Debug.Log("lang text " + textAsset.text);
		JToken jToken = JToken.Parse(textAsset.text);
		if (Application.systemLanguage == SystemLanguage.English)
		{
			foreach (JProperty item in (IEnumerable<JToken>)jToken["en"])
			{
				string name = item.Name;
				string value = item.Value.ToObject<string>();
				LanguageTable.Add(name, value);
			}
		}
		else if (Application.systemLanguage == SystemLanguage.French)
		{
			foreach (JProperty item2 in (IEnumerable<JToken>)jToken["fr"])
			{
				string name2 = item2.Name;
				string value2 = item2.Value.ToObject<string>();
				LanguageTable.Add(name2, value2);
			}
		}
		else if (Application.systemLanguage == SystemLanguage.Spanish)
		{
			foreach (JProperty item3 in (IEnumerable<JToken>)jToken["es"])
			{
				string name3 = item3.Name;
				string value3 = item3.Value.ToObject<string>();
				LanguageTable.Add(name3, value3);
			}
		}
		else if (Application.systemLanguage == SystemLanguage.German)
		{
			foreach (JProperty item4 in (IEnumerable<JToken>)jToken["de"])
			{
				string name4 = item4.Name;
				string value4 = item4.Value.ToObject<string>();
				LanguageTable.Add(name4, value4);
			}
		}
		else if (Application.systemLanguage == SystemLanguage.Portuguese)
		{
			foreach (JProperty item5 in (IEnumerable<JToken>)jToken["pr"])
			{
				string name5 = item5.Name;
				string value5 = item5.Value.ToObject<string>();
				LanguageTable.Add(name5, value5);
			}
		}
		else if (Application.systemLanguage == SystemLanguage.Indonesian)
		{
			foreach (JProperty item6 in (IEnumerable<JToken>)jToken["in"])
			{
				string name6 = item6.Name;
				string value6 = item6.Value.ToObject<string>();
				LanguageTable.Add(name6, value6);
			}
		}
		else
		{
			foreach (JProperty item7 in (IEnumerable<JToken>)jToken["en"])
			{
				string name7 = item7.Name;
				string value7 = item7.Value.ToObject<string>();
				LanguageTable.Add(name7, value7);
			}
		}
	}

	public static string GetString(string key)
	{
		if (LanguageTable.ContainsKey(key))
		{
			return LanguageTable[key];
		}
		return key;
	}
}
