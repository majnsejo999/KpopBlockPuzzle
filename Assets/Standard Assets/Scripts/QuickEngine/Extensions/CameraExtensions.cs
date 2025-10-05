using UnityEngine;

namespace QuickEngine.Extensions
{
	public static class CameraExtensions
	{
		public static Vector2 Pixel2Units2D(this Camera c)
		{
			if (!c.orthographic)
			{
				UnityEngine.Debug.LogError("Pixel2Units2D works only with orthographics camera");
				return Vector2.zero;
			}
			Vector3 vector = c.WorldToScreenPoint(c.transform.position + c.transform.forward);
			Vector3 vector2 = c.ScreenToWorldPoint(vector);
			Vector3 vector3 = c.ScreenToWorldPoint(vector + new Vector3(0f, 1f, 0f));
			Vector3 vector4 = c.ScreenToWorldPoint(vector + new Vector3(1f, 0f, 0f));
			return new Vector2(Mathf.Abs(vector4.x - vector2.x), Mathf.Abs(vector3.y - vector2.y));
		}

		public static Vector2 Unit2Pixels2D(this Camera c)
		{
			if (!c.orthographic)
			{
				UnityEngine.Debug.LogError("Unit2Pixels2D works only with orthographics camera");
				return Vector2.zero;
			}
			Vector3 vector = c.transform.position + c.transform.forward;
			Vector3 vector2 = c.WorldToScreenPoint(vector);
			Vector3 vector3 = c.WorldToScreenPoint(vector + new Vector3(0f, 1f, 0f));
			Vector3 vector4 = c.WorldToScreenPoint(vector + new Vector3(1f, 0f, 0f));
			return new Vector2(Mathf.Abs(vector4.x - vector2.x), Mathf.Abs(vector3.y - vector2.y));
		}

		public static Vector2 ToWorldSize(this Camera camera, Bounds bounds)
		{
			Vector3 min = bounds.min;
			float x = min.x;
			Vector3 max = bounds.max;
			Vector3 vector = new Vector3(x, max.y);
			Vector3 max2 = bounds.max;
			float x2 = max2.x;
			Vector3 min2 = bounds.min;
			Vector3 vector2 = new Vector3(x2, min2.y);
			return new Vector2(vector2.x - vector.x, vector.y - vector2.y);
		}

		public static Vector2 ToScreenSize(this Camera camera, Bounds bounds)
		{
			Vector3 min = bounds.min;
			float x = min.x;
			Vector3 max = bounds.max;
			Vector3 vector = camera.WorldToScreenPoint(new Vector3(x, max.y));
			Vector3 max2 = bounds.max;
			float x2 = max2.x;
			Vector3 min2 = bounds.min;
			Vector3 vector2 = camera.WorldToScreenPoint(new Vector3(x2, min2.y));
			return new Vector2(vector2.x - vector.x, vector.y - vector2.y);
		}

		public static Rect ToScreenRect(this Camera camera, Renderer renderer)
		{
			Bounds bounds = renderer.bounds;
			Vector3 min = bounds.min;
			float x = min.x;
			Vector3 max = bounds.max;
			float y = max.y;
			Vector3 position = renderer.transform.position;
			Vector3 vector = camera.WorldToScreenPoint(new Vector3(x, y, position.z));
			Vector3 max2 = bounds.max;
			float x2 = max2.x;
			Vector3 min2 = bounds.min;
			float y2 = min2.y;
			Vector3 position2 = renderer.transform.position;
			Vector3 vector2 = camera.WorldToScreenPoint(new Vector3(x2, y2, position2.z));
			return new Rect(vector.x, vector.y, vector2.x - vector.x, vector.y - vector2.y);
		}

		public static Rect ToWorldRect(this Camera camera, Renderer renderer)
		{
			Bounds bounds = renderer.bounds;
			Vector3 min = bounds.min;
			float x = min.x;
			Vector3 max = bounds.max;
			float y = max.y;
			Vector3 position = renderer.transform.position;
			Vector3 vector = new Vector3(x, y, position.z);
			Vector3 max2 = bounds.max;
			float x2 = max2.x;
			Vector3 min2 = bounds.min;
			float y2 = min2.y;
			Vector3 position2 = renderer.transform.position;
			Vector3 vector2 = new Vector3(x2, y2, position2.z);
			return new Rect(vector.x, vector.y, vector2.x - vector.x, vector.y - vector2.y);
		}

		public static Vector3 EdgePosition(this Camera camera, TextAnchor point, float distance)
		{
			Vector3 result = Vector3.zero;
			switch (point)
			{
			case TextAnchor.LowerCenter:
				result = camera.ViewportToWorldPoint(new Vector3(0.5f, 0f, distance));
				break;
			case TextAnchor.LowerLeft:
				result = camera.ViewportToWorldPoint(new Vector3(0f, 0f, distance));
				break;
			case TextAnchor.LowerRight:
				result = camera.ViewportToWorldPoint(new Vector3(1f, 0f, distance));
				break;
			case TextAnchor.MiddleCenter:
				result = camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, distance));
				break;
			case TextAnchor.MiddleLeft:
				result = camera.ViewportToWorldPoint(new Vector3(0f, 0.5f, distance));
				break;
			case TextAnchor.MiddleRight:
				result = camera.ViewportToWorldPoint(new Vector3(1f, 0.5f, distance));
				break;
			case TextAnchor.UpperCenter:
				result = camera.ViewportToWorldPoint(new Vector3(0.5f, 1f, distance));
				break;
			case TextAnchor.UpperLeft:
				result = camera.ViewportToWorldPoint(new Vector3(0f, 1f, distance));
				break;
			case TextAnchor.UpperRight:
				result = camera.ViewportToWorldPoint(new Vector3(1f, 1f, distance));
				break;
			}
			return result;
		}
	}
}
