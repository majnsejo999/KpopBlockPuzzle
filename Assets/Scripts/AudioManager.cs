using BlockGame.New.Core;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	private Dictionary<string, AudioSource> soundDictionary = new Dictionary<string, AudioSource>();

	private Dictionary<string, AudioSource> musicDictionary = new Dictionary<string, AudioSource>();

	public AudioSource musicAudioSource;

	public AudioSource effectAudioSource;

	private static AudioManager instance;

	public static AudioManager Instance => instance;

	private void Awake()
	{
		UnityEngine.Debug.Log("load audio listener");
		instance = this;
		AudioClip[] array = Resources.LoadAll<AudioClip>("Sound");
		AudioClip[] array2 = array;
		foreach (AudioClip audioClip in array2)
		{
			AudioSource audioSource = base.gameObject.AddComponent<AudioSource>();
			audioSource.playOnAwake = false;
			audioSource.clip = audioClip;
			soundDictionary.Add(audioClip.name, audioSource);
		}
		AudioClip[] array3 = Resources.LoadAll<AudioClip>("Music");
		AudioClip[] array4 = array3;
		foreach (AudioClip audioClip2 in array4)
		{
			UnityEngine.Debug.Log("load music " + audioClip2.name);
			AudioSource audioSource2 = base.gameObject.AddComponent<AudioSource>();
			audioSource2.clip = audioClip2;
			audioSource2.loop = true;
			audioSource2.playOnAwake = false;
			musicDictionary.Add(audioClip2.name, audioSource2);
		}
	}

	private void Start()
	{
		SetAudioEffectMute(!UserDataManager.Instance.GetService().SoundEnabled);
		SetAudioMusicMute(!UserDataManager.Instance.GetService().MusicEnabled);
	}

	public void PlayAudioMusic(string musicName)
	{
		StopAllBackgroundMusic();
		if (musicDictionary.ContainsKey(musicName))
		{
			UnityEngine.Debug.Log("PlayAudioMusic " + musicName);
			musicDictionary[musicName].Play();
		}
	}

	public void StopAllBackgroundMusic()
	{
		foreach (KeyValuePair<string, AudioSource> item in musicDictionary)
		{
			item.Value.Stop();
		}
	}

	public void PlayAudioEffect(string audioEffectName, bool loop = false)
	{
		if (soundDictionary.ContainsKey(audioEffectName))
		{
			if (loop)
			{
				soundDictionary[audioEffectName].loop = true;
			}
			soundDictionary[audioEffectName].Play();
		}
	}

	public void StopAudioEffect(string audioEffectName)
	{
		soundDictionary[audioEffectName].Stop();
	}

	public void SetAudioEffectMute(bool isMute)
	{
		foreach (KeyValuePair<string, AudioSource> item in soundDictionary)
		{
			item.Value.mute = isMute;
		}
		UserDataManager.Instance.GetService().SoundEnabled = !isMute;
	}

	public void SetAudioMusicMute(bool isMute)
	{
		foreach (KeyValuePair<string, AudioSource> item in musicDictionary)
		{
			item.Value.mute = isMute;
		}
		UserDataManager.Instance.GetService().MusicEnabled = !isMute;
	}
}
