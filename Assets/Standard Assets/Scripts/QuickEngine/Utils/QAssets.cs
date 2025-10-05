using System.Collections.Generic;
using UnityEngine;

namespace QuickEngine.Utils
{
	public static class QAssets
	{
		public static UnityEngine.Object GetScriptableObjectFromResources<T>(string path)
		{
			return Resources.Load(path, typeof(T));
		}

		public static Object[] GetScriptableObjectsFromResources(string path)
		{
			return Resources.LoadAll(path);
		}

		public static T[] GetScriptableObjectArray<T>(Object[] objects) where T : ScriptableObject
		{
			if (objects == null || objects.Length == 0)
			{
				return null;
			}
			List<T> list = new List<T>();
			for (int i = 0; i < objects.Length; i++)
			{
				list.Add((T)objects[i]);
			}
			return list.ToArray();
		}
	}
}
