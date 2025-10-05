using UnityEngine;

namespace QuickEngine.Extensions
{
	public static class TransformExtensions
	{
		public static void SetX(this Transform transform, float x)
		{
			Vector3 position = transform.position;
			float y = position.y;
			Vector3 position2 = transform.position;
			Vector3 vector2 = transform.position = new Vector3(x, y, position2.z);
		}

		public static void SetY(this Transform transform, float y)
		{
			Vector3 position = transform.position;
			float x = position.x;
			Vector3 position2 = transform.position;
			Vector3 vector2 = transform.position = new Vector3(x, y, position2.z);
		}

		public static void SetZ(this Transform transform, float z)
		{
			Vector3 position = transform.position;
			float x = position.x;
			Vector3 position2 = transform.position;
			Vector3 vector2 = transform.position = new Vector3(x, position2.y, z);
		}

		public static void SetXY(this Transform transform, float x, float y)
		{
			Vector3 position = transform.position;
			Vector3 vector2 = transform.position = new Vector3(x, y, position.z);
		}

		public static void SetXZ(this Transform transform, float x, float z)
		{
			Vector3 position = transform.position;
			Vector3 vector2 = transform.position = new Vector3(x, position.y, z);
		}

		public static void SetYZ(this Transform transform, float y, float z)
		{
			Vector3 position = transform.position;
			Vector3 vector2 = transform.position = new Vector3(position.x, y, z);
		}

		public static void SetXYZ(this Transform transform, float x, float y, float z)
		{
			Vector3 vector2 = transform.position = new Vector3(x, y, z);
		}

		public static void TranslateX(this Transform transform, float x)
		{
			Vector3 vector = new Vector3(x, 0f, 0f);
			transform.position += vector;
		}

		public static void TranslateY(this Transform transform, float y)
		{
			Vector3 vector = new Vector3(0f, y, 0f);
			transform.position += vector;
		}

		public static void TranslateZ(this Transform transform, float z)
		{
			Vector3 vector = new Vector3(0f, 0f, z);
			transform.position += vector;
		}

		public static void TranslateXYZ(this Transform transform, float x, float y, float z)
		{
			Vector3 vector = new Vector3(x, y, z);
			transform.position += vector;
		}

		public static void SetLocalX(this Transform transform, float x)
		{
			Vector3 localPosition = transform.localPosition;
			float y = localPosition.y;
			Vector3 localPosition2 = transform.localPosition;
			Vector3 vector2 = transform.localPosition = new Vector3(x, y, localPosition2.z);
		}

		public static void SetLocalY(this Transform transform, float y)
		{
			Vector3 localPosition = transform.localPosition;
			float x = localPosition.x;
			Vector3 localPosition2 = transform.localPosition;
			Vector3 vector2 = transform.localPosition = new Vector3(x, y, localPosition2.z);
		}

		public static void SetLocalZ(this Transform transform, float z)
		{
			Vector3 localPosition = transform.localPosition;
			float x = localPosition.x;
			Vector3 localPosition2 = transform.localPosition;
			Vector3 vector2 = transform.localPosition = new Vector3(x, localPosition2.y, z);
		}

		public static void SetLocalXY(this Transform transform, float x, float y)
		{
			Vector3 localPosition = transform.localPosition;
			Vector3 vector2 = transform.localPosition = new Vector3(x, y, localPosition.z);
		}

		public static void SetLocalXZ(this Transform transform, float x, float z)
		{
			Vector3 localPosition = transform.localPosition;
			Vector3 vector2 = transform.localPosition = new Vector3(x, localPosition.z, z);
		}

		public static void SetLocalYZ(this Transform transform, float y, float z)
		{
			Vector3 localPosition = transform.localPosition;
			Vector3 vector2 = transform.localPosition = new Vector3(localPosition.x, y, z);
		}

		public static void SetLocalXYZ(this Transform transform, float x, float y, float z)
		{
			Vector3 vector2 = transform.localPosition = new Vector3(x, y, z);
		}

		public static void ResetPosition(this Transform transform)
		{
			transform.position = Vector3.zero;
		}

		public static void ResetLocalPosition(this Transform transform)
		{
			transform.localPosition = Vector3.zero;
		}

		public static void RotateAroundX(this Transform transform, float angle)
		{
			Vector3 eulerAngles = new Vector3(angle, 0f, 0f);
			transform.Rotate(eulerAngles);
		}

		public static void RotateAroundY(this Transform transform, float angle)
		{
			Vector3 eulerAngles = new Vector3(0f, angle, 0f);
			transform.Rotate(eulerAngles);
		}

		public static void RotateAroundZ(this Transform transform, float angle)
		{
			Vector3 eulerAngles = new Vector3(0f, 0f, angle);
			transform.Rotate(eulerAngles);
		}

		public static void SetRotationX(this Transform transform, float angle)
		{
			transform.eulerAngles = new Vector3(angle, 0f, 0f);
		}

		public static void SetRotationY(this Transform transform, float angle)
		{
			transform.eulerAngles = new Vector3(0f, angle, 0f);
		}

		public static void SetRotationZ(this Transform transform, float angle)
		{
			transform.eulerAngles = new Vector3(0f, 0f, angle);
		}

		public static void SetLocalRotationX(this Transform transform, float angle)
		{
			transform.localRotation = Quaternion.Euler(new Vector3(angle, 0f, 0f));
		}

		public static void SetLocalRotationY(this Transform transform, float angle)
		{
			transform.localRotation = Quaternion.Euler(new Vector3(0f, angle, 0f));
		}

		public static void SetLocalRotationZ(this Transform transform, float angle)
		{
			transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
		}

		public static void ResetRotation(this Transform transform)
		{
			transform.rotation = Quaternion.identity;
		}

		public static void ResetLocalRotation(this Transform transform)
		{
			transform.localRotation = Quaternion.identity;
		}

		public static void SetScaleX(this Transform transform, float x)
		{
			Vector3 localScale = transform.localScale;
			float y = localScale.y;
			Vector3 localScale2 = transform.localScale;
			Vector3 vector2 = transform.localScale = new Vector3(x, y, localScale2.z);
		}

		public static void SetScaleY(this Transform transform, float y)
		{
			Vector3 localScale = transform.localScale;
			float x = localScale.x;
			Vector3 localScale2 = transform.localScale;
			Vector3 vector2 = transform.localScale = new Vector3(x, y, localScale2.z);
		}

		public static void SetScaleZ(this Transform transform, float z)
		{
			Vector3 localScale = transform.localScale;
			float x = localScale.x;
			Vector3 localScale2 = transform.localScale;
			Vector3 vector2 = transform.localScale = new Vector3(x, localScale2.y, z);
		}

		public static void SetScaleXY(this Transform transform, float x, float y)
		{
			Vector3 localScale = transform.localScale;
			Vector3 vector2 = transform.localScale = new Vector3(x, y, localScale.z);
		}

		public static void SetScaleXZ(this Transform transform, float x, float z)
		{
			Vector3 localScale = transform.localScale;
			Vector3 vector2 = transform.localScale = new Vector3(x, localScale.y, z);
		}

		public static void SetScaleYZ(this Transform transform, float y, float z)
		{
			Vector3 localScale = transform.localScale;
			Vector3 vector2 = transform.localScale = new Vector3(localScale.x, y, z);
		}

		public static void SetScaleXYZ(this Transform transform, float x, float y, float z)
		{
			Vector3 vector2 = transform.localScale = new Vector3(x, y, z);
		}

		public static void ScaleByX(this Transform transform, float x)
		{
			Vector3 localScale = transform.localScale;
			float x2 = localScale.x * x;
			Vector3 localScale2 = transform.localScale;
			float y = localScale2.y;
			Vector3 localScale3 = transform.localScale;
			transform.localScale = new Vector3(x2, y, localScale3.z);
		}

		public static void ScaleByY(this Transform transform, float y)
		{
			Vector3 localScale = transform.localScale;
			float x = localScale.x;
			Vector3 localScale2 = transform.localScale;
			float y2 = localScale2.y * y;
			Vector3 localScale3 = transform.localScale;
			transform.localScale = new Vector3(x, y2, localScale3.z);
		}

		public static void ScaleByZ(this Transform transform, float z)
		{
			Vector3 localScale = transform.localScale;
			float x = localScale.x;
			Vector3 localScale2 = transform.localScale;
			float y = localScale2.y;
			Vector3 localScale3 = transform.localScale;
			transform.localScale = new Vector3(x, y, localScale3.z * z);
		}

		public static void ScaleByXY(this Transform transform, float x, float y)
		{
			Vector3 localScale = transform.localScale;
			float x2 = localScale.x * x;
			Vector3 localScale2 = transform.localScale;
			float y2 = localScale2.y * y;
			Vector3 localScale3 = transform.localScale;
			transform.localScale = new Vector3(x2, y2, localScale3.z);
		}

		public static void ScaleByXZ(this Transform transform, float x, float z)
		{
			Vector3 localScale = transform.localScale;
			float x2 = localScale.x * x;
			Vector3 localScale2 = transform.localScale;
			float y = localScale2.y;
			Vector3 localScale3 = transform.localScale;
			transform.localScale = new Vector3(x2, y, localScale3.z * z);
		}

		public static void ScaleByYZ(this Transform transform, float y, float z)
		{
			Vector3 localScale = transform.localScale;
			float x = localScale.x;
			Vector3 localScale2 = transform.localScale;
			float y2 = localScale2.y * y;
			Vector3 localScale3 = transform.localScale;
			transform.localScale = new Vector3(x, y2, localScale3.z * z);
		}

		public static void ScaleByXY(this Transform transform, float r)
		{
			transform.ScaleByXY(r, r);
		}

		public static void ScaleByXZ(this Transform transform, float r)
		{
			transform.ScaleByXZ(r, r);
		}

		public static void ScaleByYZ(this Transform transform, float r)
		{
			transform.ScaleByYZ(r, r);
		}

		public static void ScaleByXYZ(this Transform transform, float x, float y, float z)
		{
			transform.localScale = new Vector3(x, y, z);
		}

		public static void ScaleByXYZ(this Transform transform, float r)
		{
			transform.ScaleByXYZ(r, r, r);
		}

		public static void ResetScale(this Transform transform)
		{
			transform.localScale = Vector3.one;
		}

		public static void FlipX(this Transform transform)
		{
			Vector3 localScale = transform.localScale;
			transform.SetScaleX(0f - localScale.x);
		}

		public static void FlipY(this Transform transform)
		{
			Vector3 localScale = transform.localScale;
			transform.SetScaleY(0f - localScale.y);
		}

		public static void FlipZ(this Transform transform)
		{
			Vector3 localScale = transform.localScale;
			transform.SetScaleZ(0f - localScale.z);
		}

		public static void FlipXY(this Transform transform)
		{
			Vector3 localScale = transform.localScale;
			float x = 0f - localScale.x;
			Vector3 localScale2 = transform.localScale;
			transform.SetScaleXY(x, 0f - localScale2.y);
		}

		public static void FlipXZ(this Transform transform)
		{
			Vector3 localScale = transform.localScale;
			float x = 0f - localScale.x;
			Vector3 localScale2 = transform.localScale;
			transform.SetScaleXZ(x, 0f - localScale2.z);
		}

		public static void FlipYZ(this Transform transform)
		{
			Vector3 localScale = transform.localScale;
			float y = 0f - localScale.y;
			Vector3 localScale2 = transform.localScale;
			transform.SetScaleYZ(y, 0f - localScale2.z);
		}

		public static void FlipXYZ(this Transform transform)
		{
			Vector3 localScale = transform.localScale;
			float x = 0f - localScale.z;
			Vector3 localScale2 = transform.localScale;
			float y = 0f - localScale2.y;
			Vector3 localScale3 = transform.localScale;
			transform.SetScaleXYZ(x, y, 0f - localScale3.z);
		}

		public static void FlipPostive(this Transform transform)
		{
			Vector3 localScale = transform.localScale;
			float x = Mathf.Abs(localScale.x);
			Vector3 localScale2 = transform.localScale;
			float y = Mathf.Abs(localScale2.y);
			Vector3 localScale3 = transform.localScale;
			transform.localScale = new Vector3(x, y, Mathf.Abs(localScale3.z));
		}

		public static void Reset(this Transform transform)
		{
			transform.ResetRotation();
			transform.ResetPosition();
			transform.ResetScale();
		}

		public static void ResetLocal(this Transform transform)
		{
			transform.ResetLocalRotation();
			transform.ResetLocalPosition();
			transform.ResetScale();
		}
	}
}
