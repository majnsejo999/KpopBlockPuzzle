using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransManager : MonoBehaviour
{
	public Image panel;

	public float fadeTime;

	private bool isSceneLoaded;

	public string currentScene = "LoadingScene";

	public string previousScene = "LoadingScene";

	private static SceneTransManager instance;

	public static SceneTransManager Instance => instance;

	private void Awake()
	{
		instance = this;
	}

	private void Start()
	{
		base.gameObject.SetActive(value: false);
	}

	public void TransTo(string scene)
	{
		previousScene = currentScene;
		currentScene = scene;
		SceneManager.LoadScene(scene);
	}

	public void SwitchTo(string scene)
	{
		previousScene = currentScene;
		currentScene = scene;
		panel.color = new Color(0f, 0f, 0f, 0f);
		base.gameObject.SetActive(value: true);
		panel.DOFade(1f, fadeTime).OnComplete(delegate
		{
			StartCoroutine(_SwitchToScene(scene));
		});
	}

	public string GetCurrentScene()
	{
		return currentScene;
	}

	public string GetPreviousScene()
	{
		return previousScene;
	}

	public void SetPreviousScene(string sceneName)
	{
		previousScene = sceneName;
	}

	public void SetCurrentScene(string sceneName)
	{
		currentScene = sceneName;
	}

	private IEnumerator _SwitchToScene(string sceneName)
	{
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
		while (!asyncLoad.isDone)
		{
			yield return null;
		}
		yield return asyncLoad;
		FadeOut();
	}

	private void FadeOut()
	{
		panel.DOFade(0f, fadeTime).OnComplete(delegate
		{
			base.gameObject.SetActive(value: false);
		});
	}
}
