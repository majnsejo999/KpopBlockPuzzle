using System;
using UnityEngine;

namespace QuickEngine.Extensions
{
	public static class FloatExtensions
	{
		public static float Round(this float f, int decimals = 1)
		{
			return (float)Math.Round(f, decimals);
		}

		public static float Round(this float f)
		{
			return Mathf.Round(f);
		}
	}
}
