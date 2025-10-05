using System.Collections;
using UnityEngine;

namespace QuickEngine.Extensions
{
	public static class AudioExtensions
	{
		public static IEnumerator PlayOneShotDelayed(this AudioSource anAudioSource, AudioClip anAudioClip, float aDelay)
		{
			while (aDelay > 0f)
			{
				yield return null;
				aDelay -= Time.deltaTime;
			}
			anAudioSource.PlayOneShot(anAudioClip);
		}

		public static AudioType PlatformAudioType()
		{
			return AudioType.MPEG;
		}

		public static string PlatformAudioExtension()
		{
			return ".mp3";
		}

		public static string PlatformFileProtocol()
		{
			return "file://";
		}

		public static float ToDecibel(this float linear)
		{
			if (linear != 0f)
			{
				return 20f * Mathf.Log10(linear);
			}
			return -144f;
		}

		public static float ToLinear(this float dB)
		{
			return Mathf.Pow(10f, dB / 20f);
		}
	}
}
