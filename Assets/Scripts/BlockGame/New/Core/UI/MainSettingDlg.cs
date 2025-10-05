using System;
using UnityEngine;
using UnityEngine.UI;

namespace BlockGame.New.Core.UI
{
	public class MainSettingDlg : BaseDialog
	{
		public GameObject MusicButton;

		public GameObject SoundButton;

		public Button BtnGooglePlay;

		public Button BtnRestorePurchase;

		public Text BtnGpText;

		public Button BtnQuit;

		protected override void Awake()
		{
			base.Awake();
		}

		protected override void Start()
		{
			base.Start();
		}

		public override void Show()
		{
			base.Show();
			UpdateToggles();
			BtnGooglePlay.gameObject.SetActive(value: false);
			BtnRestorePurchase.gameObject.SetActive(value: false);
			UpdateBtnGooglePlay();
		}

		public override void Close()
		{
			base.Close();
		}

		public void UpdateToggles()
		{
			if (UserDataManager.Instance.GetService().MusicEnabled)
			{
				SetToggleMusicStatus(MusicButton, true);
			}
			else
			{
				SetToggleMusicStatus(MusicButton, false);
			}
			if (UserDataManager.Instance.GetService().SoundEnabled)
			{
				SetToggleSoundStatus(SoundButton, true);
			}
			else
			{
				SetToggleSoundStatus(SoundButton, false);
			}
		}

		private void SetToggleMusicStatus(GameObject button, bool enabled)
		{
			if (enabled)
			{
				button.GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/Buttonicons/icon_music");
			}
			else
			{
				button.GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/Buttonicons/icon_music_turnoff");
			}
		}
		private void SetToggleSoundStatus(GameObject button, bool enabled)
		{
			if (enabled)
			{
				button.GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/Buttonicons/icon_sound");
			}
			else
			{
				button.GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/Buttonicons/icon_sound_disable");
			}
		}
		public void ToggleMusicButton()
		{
			bool flag = !UserDataManager.Instance.GetService().MusicEnabled;
			UserDataManager.Instance.GetService().MusicEnabled = flag;
			SetToggleMusicStatus(MusicButton, flag);
			AudioManager.Instance.SetAudioMusicMute(!flag);
		}

		public void ToggleSoundButton()
		{
			bool flag = !UserDataManager.Instance.GetService().SoundEnabled;
			UserDataManager.Instance.GetService().SoundEnabled = flag;
			SetToggleSoundStatus(SoundButton, flag);
			AudioManager.Instance.SetAudioEffectMute(!flag);
		}

		public void UpdateBtnGooglePlay()
		{
			//if (Social.localUser.authenticated)
			//{
			//	BtnGpText.text = "Log out";
			//}
			//else
			//{
				BtnGpText.text = "Log in";
			//}
		}

		public void BtnGooglePlayClicked()
		{
			//if (Social.localUser.authenticated)
			//{
			//	PlayGamesPlatform.Instance.SignOut();
			//	UpdateBtnGooglePlay();
			//}
			//else
			//{
			//	GlobalVariables.ResumeFromDesktop = false;
			//	Social.localUser.Authenticate(delegate(bool success)
			//	{
			//		UnityEngine.Debug.Log("Authenticate callback retrieved.");
			//		UpdateBtnGooglePlay();
			//		if (success)
			//		{
			//			UnityEngine.Debug.Log("Authentication success!");
			//			((PlayGamesPlatform)Social.Active).SetGravityForPopups(Gravity.TOP);
			//		}
			//	});
			//}
		}

		public void BtnRestorePurchaseClicked()
		{
			if (Application.internetReachability != 0)
			{
				GlobalVariables.ResumeFromDesktop = false;
				MaskDlg.Instance.Enable();
				Purchaser.Instance.RestorePurchases();
			}
			else
			{
				InfoDlg.Instance.UpdateInfo("Your network is disconnected, please try again later.");
				InfoDlg.Instance.Show();
			}
		}

		public void BtnQuitClicked()
		{
			DialogManager.Instance.ShowDialog("QuitDlg");
		}
	}
}
