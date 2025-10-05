using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CUtils
{
	private static AndroidJavaClass cls_UnityPlayer;

	private static AndroidJavaObject obj_Activity;

	static CUtils()
	{
		AndroidJNI.AttachCurrentThread();
		cls_UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		obj_Activity = cls_UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
	}

	public static bool IsAppInstalled(string packageName)
	{
		return obj_Activity.Call<bool>("isAppInstalled", new object[1]
		{
			packageName
		});
	}

	public static void PushNotification(int id)
	{
		obj_Activity.Call("pushNotificaiton", id);
	}

	public static void LikeFacebookPage(string faceID)
	{
	}

	public static string ReadFileContent(string path)
	{
		TextAsset textAsset = Resources.Load(path) as TextAsset;
		return (!(textAsset == null)) ? textAsset.text : null;
	}

	public static Vector3 CopyVector3(Vector3 ori)
	{
		return new Vector3(ori.x, ori.y, ori.z);
	}

	public static bool EqualVector3(Vector3 v1, Vector3 v2)
	{
		return Vector3.SqrMagnitude(v1 - v2) <= 1E-07f;
	}

	public static float GetSign(Vector3 A, Vector3 B, Vector3 M)
	{
		return Mathf.Sign((B.x - A.x) * (M.y - A.y) - (B.y - A.y) * (M.x - A.x));
	}

	public static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
	{
		Vector3 point2 = point - pivot;
		point2 = Quaternion.Euler(angles) * point2;
		point = point2 + pivot;
		return point;
	}

	public static void Shuffle<T>(params T[] array)
	{
		for (int i = 0; i < array.Length; i++)
		{
			T val = array[i];
			int num = UnityEngine.Random.Range(0, array.Length);
			array[i] = array[num];
			array[num] = val;
		}
	}

	public static string[] SeparateLines(string lines)
	{
		return lines.Replace("\r\n", "\n").Replace("\r", "\n").Split("\n"[0]);
	}

	public static void ChangeSortingLayerRecursively(Transform root, string sortingLayerName, int offsetOrder = 0)
	{
		SpriteRenderer component = root.GetComponent<SpriteRenderer>();
		if (component != null)
		{
			component.sortingLayerName = sortingLayerName;
			component.sortingOrder += offsetOrder;
		}
		IEnumerator enumerator = root.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				Transform root2 = (Transform)enumerator.Current;
				ChangeSortingLayerRecursively(root2, sortingLayerName, offsetOrder);
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
	}

	public static void ChangeRendererColorRecursively(Transform root, Color color)
	{
		SpriteRenderer component = root.GetComponent<SpriteRenderer>();
		if (component != null)
		{
			component.color = color;
		}
		IEnumerator enumerator = root.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				Transform root2 = (Transform)enumerator.Current;
				ChangeRendererColorRecursively(root2, color);
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
	}

	public static void ChangeImageColorRecursively(Transform root, Color color)
	{
		Image component = root.GetComponent<Image>();
		if (component != null)
		{
			component.color = color;
		}
		IEnumerator enumerator = root.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				Transform root2 = (Transform)enumerator.Current;
				ChangeImageColorRecursively(root2, color);
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
	}

	public static string GetFacePictureURL(string facebookID, int? width = default(int?), int? height = default(int?), string type = null)
	{
		string text = $"/{facebookID}/picture";
		string str = (!width.HasValue) ? string.Empty : ("&width=" + width.ToString());
		str += ((!height.HasValue) ? string.Empty : ("&height=" + height.ToString()));
		str += ((type == null) ? string.Empty : ("&type=" + type));
		str += "&redirect=false";
		if (str != string.Empty)
		{
			text = text + "?g" + str;
		}
		return text;
	}

	public static double GetCurrentTime()
	{
		return DateTime.Now.Subtract(new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
	}

	public static double GetCurrentTimeInDays()
	{
		return DateTime.Now.Subtract(new DateTime(1970, 1, 1, 0, 0, 0)).TotalDays;
	}

	public static double GetCurrentTimeInMills()
	{
		return DateTime.Now.Subtract(new DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds;
	}

	public static T GetRandom<T>(params T[] arr)
	{
		return arr[UnityEngine.Random.Range(0, arr.Length)];
	}

	public static string BuildStringFromCollection(ICollection values, char split = '|')
	{
		string text = string.Empty;
		int num = 0;
		IEnumerator enumerator = values.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object current = enumerator.Current;
				text += current;
				if (num != values.Count - 1)
				{
					text += split;
				}
				num++;
			}
			return text;
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
	}

	public static List<T> BuildListFromString<T>(string values, char split = '|')
	{
		List<T> list = new List<T>();
		if (string.IsNullOrEmpty(values))
		{
			return list;
		}
		string[] array = values.Split(split);
		string[] array2 = array;
		foreach (string value in array2)
		{
			if (!string.IsNullOrEmpty(value))
			{
				T item = (T)Convert.ChangeType(value, typeof(T));
				list.Add(item);
			}
		}
		return list;
	}

	public static List<T> GetObjectInRange<T>(Vector3 position, float radius, int layerMask = -5) where T : class
	{
		List<T> list = new List<T>();
		Collider2D[] array = Physics2D.OverlapCircleAll(position, radius, layerMask);
		Collider2D[] array2 = array;
		foreach (Collider2D collider2D in array2)
		{
			list.Add(collider2D.gameObject.GetComponent(typeof(T)) as T);
		}
		return list;
	}

	public static Sprite GetSprite(string textureName, string spriteName)
	{
		Sprite[] array = Resources.LoadAll<Sprite>(textureName);
		Sprite[] array2 = array;
		foreach (Sprite sprite in array2)
		{
			if (sprite.name == spriteName)
			{
				return sprite;
			}
		}
		return null;
	}

	public static List<Transform> GetActiveChildren(Transform parent)
	{
		List<Transform> list = new List<Transform>();
		IEnumerator enumerator = parent.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				Transform transform = (Transform)enumerator.Current;
				if (transform.gameObject.activeSelf)
				{
					list.Add(transform);
				}
			}
			return list;
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
	}

	public static List<Transform> GetChildren(Transform parent)
	{
		List<Transform> list = new List<Transform>();
		IEnumerator enumerator = parent.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				Transform item = (Transform)enumerator.Current;
				list.Add(item);
			}
			return list;
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
	}

	public static void CheckConnection(MonoBehaviour behaviour, Action<int> connectionListener)
	{
		behaviour.StartCoroutine(ConnectUrl("http://www.google.com", connectionListener));
	}

	private static IEnumerator ConnectUrl(string url, Action<int> connectionListener)
	{
		WWW www = new WWW(url);
		yield return www;
		if (www.error != null)
		{
			connectionListener(1);
		}
		else if (string.IsNullOrEmpty(www.text))
		{
			connectionListener(2);
		}
		else
		{
			connectionListener(0);
		}
	}

	public static void CheckDisconnection(MonoBehaviour behaviour, Action onDisconnected)
	{
		behaviour.StartCoroutine(ConnectUrl("http://www.google.com", onDisconnected));
	}

	private static IEnumerator ConnectUrl(string url, Action onDisconnected)
	{
		WWW www2 = new WWW(url);
		yield return www2;
		if (www2.error != null)
		{
			yield return new WaitForSeconds(2f);
			www2 = new WWW(url);
			yield return www2;
			if (www2.error != null)
			{
				onDisconnected();
			}
		}
	}

	public static void ShowInterstitialAd()
	{
	}

	public static Sprite CreateSprite(Texture2D texture, int width, int height)
	{
		return Sprite.Create(texture, new Rect(0f, 0f, width, height), new Vector2(0.5f, 0.5f), 100f);
	}

	public static List<List<T>> Split<T>(List<T> source, Predicate<T> split)
	{
		List<List<T>> list = new List<List<T>>();
		bool flag = false;
		for (int i = 0; i < source.Count; i++)
		{
			T val = source[i];
			if (split(val))
			{
				flag = false;
				continue;
			}
			if (!flag)
			{
				flag = true;
				list.Add(new List<T>());
			}
			list[list.Count - 1].Add(val);
		}
		return list;
	}

	public static List<List<T>> GetArrList<T>(List<T> source, Predicate<T> take)
	{
		List<List<T>> list = new List<List<T>>();
		bool flag = false;
		foreach (T item in source)
		{
			if (take(item))
			{
				if (!flag)
				{
					flag = true;
					list.Add(new List<T>());
				}
				list[list.Count - 1].Add(item);
			}
			else
			{
				flag = false;
			}
		}
		return list;
	}

	public static List<T> ToList<T>(T obj)
	{
		List<T> list = new List<T>();
		list.Add(obj);
		return list;
	}

	public static int ChooseRandomWithProbs(float[] probs)
	{
		float num = 0f;
		foreach (float num2 in probs)
		{
			num += num2;
		}
		float num3 = UnityEngine.Random.value * num;
		for (int j = 0; j < probs.Length; j++)
		{
			if (num3 < probs[j])
			{
				return j;
			}
			num3 -= probs[j];
		}
		return probs.Length - 1;
	}

	public static bool IsObjectSeenByCamera(Camera camera, GameObject gameObj, float delta = 0f)
	{
		Vector3 vector = camera.WorldToViewportPoint(gameObj.transform.position);
		return vector.z > 0f && vector.x > 0f - delta && vector.x < 1f + delta && vector.y > 0f - delta && vector.y < 1f + delta;
	}

	public static Vector3 GetMiddlePoint(Vector3 begin, Vector3 end, float delta = 0f)
	{
		Vector3 a = Vector3.Lerp(begin, end, 0.5f);
		Vector3 vector = end - begin;
		Vector3 normalized = new Vector3(0f - vector.y, vector.x, 0f).normalized;
		return a + normalized * delta;
	}

	public static AnimationClip GetAnimationClip(Animator anim, string name)
	{
		RuntimeAnimatorController runtimeAnimatorController = anim.runtimeAnimatorController;
		for (int i = 0; i < runtimeAnimatorController.animationClips.Length; i++)
		{
			if (runtimeAnimatorController.animationClips[i].name == name)
			{
				return runtimeAnimatorController.animationClips[i];
			}
		}
		return null;
	}

	public static void Swap<T>(ref T lhs, ref T rhs)
	{
		T val = lhs;
		lhs = rhs;
		rhs = val;
	}

	public static void Pause()
	{
	}
}
