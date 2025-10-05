using UnityEngine;

namespace QuickEngine.Extensions
{
	public static class ColorExtensions
	{
		private const float LightOffset = 0.0625f;

		private const float DarkerFactor = 0.9f;

		public static Color ColorFrom256(this Color color, float r, float g, float b, float a = 256f)
		{
			return new Color(r / 255f, g / 255f, b / 255f, a / 255f);
		}

		public static Color ColorFrom256(float r, float g, float b, float a = 256f)
		{
			return new Color(r / 255f, g / 255f, b / 255f, a / 255f);
		}

		public static Color Lighter(this Color color)
		{
			return new Color(color.r + 0.0625f, color.g + 0.0625f, color.b + 0.0625f, color.a);
		}

		public static Color Darker(this Color color)
		{
			return new Color(color.r - 0.0625f, color.g - 0.0625f, color.b - 0.0625f, color.a);
		}

		public static float Brightness(this Color color)
		{
			return (color.r + color.g + color.b) / 3f;
		}

		public static Color WithBrightness(this Color color, float brightness)
		{
			if (color.IsApproximatelyBlack())
			{
				return new Color(brightness, brightness, brightness, color.a);
			}
			float num = brightness / color.Brightness();
			float r = color.r * num;
			float g = color.g * num;
			float b = color.b * num;
			float a = color.a;
			return new Color(r, g, b, a);
		}

		public static bool IsApproximatelyBlack(this Color color)
		{
			return color.r + color.g + color.b <= Mathf.Epsilon;
		}

		public static bool IsApproximatelyWhite(this Color color)
		{
			return color.r + color.g + color.b >= 1f - Mathf.Epsilon;
		}

		public static Color Opaque(this Color color)
		{
			return new Color(color.r, color.g, color.b);
		}

		public static Color Invert(this Color color)
		{
			return new Color(1f - color.r, 1f - color.g, 1f - color.b, color.a);
		}

		public static Color WithAlpha(this Color color, float alpha)
		{
			return new Color(color.r, color.g, color.b, alpha);
		}
	}
}
