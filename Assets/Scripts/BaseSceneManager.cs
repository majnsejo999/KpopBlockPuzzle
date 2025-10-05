using BlockGame.New.Core.UI;
using UnityEngine;

public class BaseSceneManager : MonoBehaviour
{
	protected BaseUIManager sceneUI;

	protected virtual void Awake()
	{
		ScreenManager.UpdateCanvasCamera(TopCanvasManager.Instance.canvas, 500);
		ScreenManager.UpdateCanvasCamera(DialogManager.Instance.canvas, UIConfig.DialogSortingOrder);
	}

	protected virtual void Start()
	{
		AdjustScreen();
		sceneUI.ShowUI();
	}

	protected virtual void OnEnable()
	{
	}

	protected virtual void OnDisable()
	{
		if (sceneUI != null)
		{
			sceneUI.HideUI();
		}
	}

	private void AdjustScreen()
	{
		float num = Screen.height;
		float num2 = Screen.width;
		float orthographicSize = Camera.main.orthographicSize;
		float orthographicSize2 = 3.6f * ((float)Screen.height * 1f / (float)Screen.width);
		Camera.main.orthographicSize = orthographicSize2;
	}

	private void OnApplicationPause(bool isPause)
	{
		UnityEngine.Debug.Log("OnApplicationPause");
		ApplicationController.ProcessApplicationPause(isPause);
	}

	private void OnApplicationQuit()
	{
		UnityEngine.Debug.Log("OnApplicationQuit");
		ApplicationController.ProcessApplicationQuit();
	}
}
