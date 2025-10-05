using BlockGame.GameEngine.Libs.Log;
using BlockGame.New.Core.UI;
using UnityEngine;

namespace BlockGame.New.Core
{
	public class GameSceneManager : BaseSceneManager
	{
		protected override void Awake()
		{
			base.Awake();
			sceneUI = GameSceneUIManager.Instance;
			AudioManager.Instance.SetAudioMusicMute(!UserDataManager.Instance.GetService().MusicEnabled);
		}

		protected override void Start()
		{
			base.Start();
			if (UserDataManager.Instance.GetService().TutorialProgress >= 3)
			{
				GameLogic.Instance.StartGame(showTutorial: true, 0.42f);
			}
			else
			{
				GameLogic.Instance.StartGame();
			}
		}

		private void Update()
		{
			if (Input.GetMouseButtonDown(0) && GameLogic.Instance.State == GameLogic.GameState.Run)
			{
				Vector3 pos = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
			}
			else if (!Input.GetMouseButtonUp(0))
			{
			}
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				if (!DialogManager.Instance.IsDialogShowing())
				{
					AudioManager.Instance.PlayAudioEffect("button_click");
					DialogManager.Instance.ShowDialog("SettingDlg");
				}
			}
		}
	}
}
