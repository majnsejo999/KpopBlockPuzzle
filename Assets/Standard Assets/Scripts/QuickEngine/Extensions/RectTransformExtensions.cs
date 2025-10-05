using UnityEngine;

namespace QuickEngine.Extensions
{
	public static class RectTransformExtensions
	{
		public static void SetDefaultScale(this RectTransform trans)
		{
			trans.localScale = new Vector3(1f, 1f, 1f);
		}

		public static void SetPivotAndAnchors(this RectTransform trans, Vector2 aVec)
		{
			trans.pivot = aVec;
			trans.anchorMin = aVec;
			trans.anchorMax = aVec;
		}

		public static void SetAnchors(this RectTransform trans, Vector2 aVec)
		{
			trans.anchorMin = aVec;
			trans.anchorMax = aVec;
		}

		public static Vector2 GetSize(this RectTransform trans)
		{
			return trans.rect.size;
		}

		public static float GetWidth(this RectTransform trans)
		{
			return trans.rect.width;
		}

		public static float GetHeight(this RectTransform trans)
		{
			return trans.rect.height;
		}

		public static void SetPositionOfPivot(this RectTransform trans, Vector2 newPos)
		{
			float x = newPos.x;
			float y = newPos.y;
			Vector3 localPosition = trans.localPosition;
			trans.localPosition = new Vector3(x, y, localPosition.z);
		}

		public static void SetLeftBottomPosition(this RectTransform trans, Vector2 newPos)
		{
			float x = newPos.x;
			Vector2 pivot = trans.pivot;
			float x2 = x + pivot.x * trans.rect.width;
			float y = newPos.y;
			Vector2 pivot2 = trans.pivot;
			float y2 = y + pivot2.y * trans.rect.height;
			Vector3 localPosition = trans.localPosition;
			trans.localPosition = new Vector3(x2, y2, localPosition.z);
		}

		public static void SetLeftTopPosition(this RectTransform trans, Vector2 newPos)
		{
			float x = newPos.x;
			Vector2 pivot = trans.pivot;
			float x2 = x + pivot.x * trans.rect.width;
			float y = newPos.y;
			Vector2 pivot2 = trans.pivot;
			float y2 = y - (1f - pivot2.y) * trans.rect.height;
			Vector3 localPosition = trans.localPosition;
			trans.localPosition = new Vector3(x2, y2, localPosition.z);
		}

		public static void SetRightBottomPosition(this RectTransform trans, Vector2 newPos)
		{
			float x = newPos.x;
			Vector2 pivot = trans.pivot;
			float x2 = x - (1f - pivot.x) * trans.rect.width;
			float y = newPos.y;
			Vector2 pivot2 = trans.pivot;
			float y2 = y + pivot2.y * trans.rect.height;
			Vector3 localPosition = trans.localPosition;
			trans.localPosition = new Vector3(x2, y2, localPosition.z);
		}

		public static void SetRightTopPosition(this RectTransform trans, Vector2 newPos)
		{
			float x = newPos.x;
			Vector2 pivot = trans.pivot;
			float x2 = x - (1f - pivot.x) * trans.rect.width;
			float y = newPos.y;
			Vector2 pivot2 = trans.pivot;
			float y2 = y - (1f - pivot2.y) * trans.rect.height;
			Vector3 localPosition = trans.localPosition;
			trans.localPosition = new Vector3(x2, y2, localPosition.z);
		}

		public static void SetSize(this RectTransform trans, Vector2 newSize)
		{
			Vector2 size = trans.rect.size;
			Vector2 vector = newSize - size;
			Vector2 offsetMin = trans.offsetMin;
			float x = vector.x;
			Vector2 pivot = trans.pivot;
			float x2 = x * pivot.x;
			float y = vector.y;
			Vector2 pivot2 = trans.pivot;
			trans.offsetMin = offsetMin - new Vector2(x2, y * pivot2.y);
			Vector2 offsetMax = trans.offsetMax;
			float x3 = vector.x;
			Vector2 pivot3 = trans.pivot;
			float x4 = x3 * (1f - pivot3.x);
			float y2 = vector.y;
			Vector2 pivot4 = trans.pivot;
			trans.offsetMax = offsetMax + new Vector2(x4, y2 * (1f - pivot4.y));
		}

		public static void SetWidth(this RectTransform trans, float newSize)
		{
			Vector2 size = trans.rect.size;
			trans.SetSize(new Vector2(newSize, size.y));
		}

		public static void SetHeight(this RectTransform trans, float newSize)
		{
			Vector2 size = trans.rect.size;
			trans.SetSize(new Vector2(size.x, newSize));
		}
	}
}
