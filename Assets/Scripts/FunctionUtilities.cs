using BlockGame.GameEngine.Libs.Log;
using System;
using UnityEngine;

public class FunctionUtilities
{
	public static int IntParse(object obj)
	{
		int num = 0;
		try
		{
			UnityEngine.Debug.Log("Casting obj: " + obj.ToString());
			return int.Parse(obj.ToString());
		}
		catch (Exception arg)
		{
			return 0;
		}
		finally
		{
		}
	}

	public static float FloatParse(object obj)
	{
		float result = 0f;
		try
		{
			result = int.Parse(obj.ToString());
			return result;
		}
		catch (Exception arg)
		{
			return result;
		}
	}

	public static void SetPositionX(GameObject go, float value, bool isLocal = false)
	{
		if (isLocal)
		{
			Vector3 localPosition = go.transform.localPosition;
			go.transform.localPosition = new Vector3(value, localPosition.y, localPosition.z);
		}
		else
		{
			Vector3 position = go.transform.position;
			go.transform.position = new Vector3(value, position.y, position.z);
		}
	}

	public static void SetPositionY(GameObject go, float value, bool isLocal = false)
	{
		if (isLocal)
		{
			Vector3 localPosition = go.transform.localPosition;
			go.transform.localPosition = new Vector3(localPosition.x, value, localPosition.z);
		}
		else
		{
			Vector3 position = go.transform.position;
			go.transform.position = new Vector3(position.x, value, position.z);
		}
	}

	public static void SetPositionZ(GameObject go, float value, bool isLocal = false)
	{
		if (isLocal)
		{
			Vector3 localPosition = go.transform.localPosition;
			go.transform.localPosition = new Vector3(localPosition.x, localPosition.y, value);
		}
		else
		{
			Vector3 position = go.transform.position;
			go.transform.position = new Vector3(position.x, position.y, value);
		}
	}
}
