using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseUI : MonoBehaviour
{
	public LanguageType currLanguage;

	public Dictionary<Text, string> multiLangTexts;

	public virtual void ProcessTexts()
	{
		currLanguage = (LanguageType)0;
		multiLangTexts = new Dictionary<Text, string>();
		Text[] componentsInChildren = GetComponentsInChildren<Text>();
		Text[] array = componentsInChildren;
		foreach (Text text in array)
		{
			string text2 = text.text;
			if (text2.StartsWith("#", StringComparison.CurrentCulture) && !multiLangTexts.ContainsKey(text))
			{
				multiLangTexts.Add(text, text2.ToLower());
			}
		}
	}

	public virtual void UpdateLanguage()
	{
		foreach (KeyValuePair<Text, string> multiLangText in multiLangTexts)
		{
			string @string = LanguageManager.GetString(multiLangText.Value);
			multiLangText.Key.text = @string;
		}
	}
}
