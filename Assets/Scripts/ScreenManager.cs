using UnityEngine;

public class ScreenManager
{
	public const int PixelDesignWidth = 720;

	public const int PixelDesignHeight = 1280;

	public const float UnitDesignWidth = 7.2f;

	public const float UnitDesignHeight = 12.8f;

	public const int PixelPerUnit = 100;

	public static float AspectRatio => (float)Screen.width * 1f / (float)Screen.height;

	public static float DesignAspectRatio => 0.5625f;

	public static float ResolutionAdaptionRatio => (!((float)Screen.width * 1f / (float)Screen.height >= 0.5625f)) ? 1f : (0.5625f / ((float)Screen.width * 1f / (float)Screen.height));

	public static float UnitXOffset => (UnitScreenWidth - 7.2f) / 2f;

	public static float UnitScreenWidth => UnitScreenHeight * AspectRatio;

	public static float UnitScreenHeight => Camera.main.orthographicSize * 2f;

	public static void AdjustScreen()
	{
		float num = Screen.height;
		float num2 = Screen.width;
		float orthographicSize = Camera.main.orthographicSize;
		float orthographicSize2 = 3.6f * ((float)Screen.height * 1f / (float)Screen.width);
		Camera.main.orthographicSize = orthographicSize2;
	}

	public static void UpdateCanvasCamera(Canvas canvas, int order = 100, string layer = "UI")
	{
		canvas.worldCamera = Camera.main;
		canvas.sortingLayerName = layer;
		canvas.sortingOrder = order;
	}
}
