using QuickEngine.Extensions;
using System;
using UnityEngine;

namespace QuickEngine
{
	[Serializable]
	public class QColor
	{
		private Color color;

		private Color colorDark;

		private Color colorLight;

		public Color Color => color;

		public Color ColorDark => colorDark;

		public Color ColorLight => colorLight;

		public float ColorBrightness => Color.Brightness();

		public float ColorDarkBrightness => ColorDark.Brightness();

		public float ColorLightBrightness => ColorLight.Brightness();

		public Color ColorOpaque => Color.Opaque();

		public Color ColorDarkOpaque => ColorDark.Opaque();

		public Color ColorLightOpaque => ColorLight.Opaque();

		public Color ColorInvert => Color.Invert();

		public Color ColorDarkInvert => ColorDark.Invert();

		public Color ColorLightInvert => ColorLight.Invert();

		public QColor(Color color)
		{
			SetColor(color);
		}

		public QColor(Color color, float alpha)
		{
			SetColor(new Color(color.r, color.g, color.b, alpha));
		}

		public QColor(float r, float g, float b, bool from256 = true)
		{
			SetColor((!from256) ? new Color(r, g, b) : ColorExtensions.ColorFrom256(r, g, b));
		}

		public QColor(float r, float g, float b, float a, bool from256 = true)
		{
			SetColor((!from256) ? new Color(r, g, b, a) : ColorExtensions.ColorFrom256(r, g, b, a));
		}

		public void SetColor(Color color)
		{
			this.color = color;
			colorLight = color.Lighter();
			colorDark = color.Darker();
		}

		public Color ColorWithBrightness(float brightness)
		{
			return Color.WithBrightness(brightness);
		}

		public Color ColorDarkWithBrightness(float brightness)
		{
			return ColorDark.WithBrightness(brightness);
		}

		public Color ColorLightWithBrightness(float brightness)
		{
			return ColorLight.WithBrightness(brightness);
		}

		public Color ColorWithAlpha(float alpha)
		{
			return Color.WithAlpha(alpha);
		}

		public Color ColorDarkWithAlpha(float alpha)
		{
			return ColorDark.WithAlpha(alpha);
		}

		public Color ColorLightWithAlpha(float alpha)
		{
			return ColorLight.WithAlpha(alpha);
		}
	}
}
