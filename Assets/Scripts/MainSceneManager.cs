using BlockGame.New.Core;
using BlockGame.New.Core.UI;
using UnityEngine;

public class MainSceneManager : BaseSceneManager
{
	protected override void Awake()
	{
		base.Awake();
		sceneUI = MainSceneUIManager.Instance;
		AudioManager.Instance.SetAudioMusicMute(!UserDataManager.Instance.GetService().MusicEnabled);
	}

	protected override void Start()
	{
		base.Start();
		if (SceneTransManager.Instance.GetPreviousScene() == "LoadingScene")
		{
			AudioManager.Instance.PlayAudioMusic("bgm_01");
		}
		//if (SceneTransManager.Instance.GetPreviousScene() != "GameScene" )
		//{
		//	AdsControl.instance.ShowBanner();
		//}
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (!DialogManager.Instance.IsDialogShowing())
			{
				AudioManager.Instance.PlayAudioEffect("button_click");
				DialogManager.Instance.ShowDialog("QuitDlg");
			}
		}
	}
}
