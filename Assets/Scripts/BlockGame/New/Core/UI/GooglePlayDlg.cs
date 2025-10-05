using UnityEngine;
using UnityEngine.UI;

namespace BlockGame.New.Core.UI
{
	public class GooglePlayDlg : BaseDialog
	{
		public Text BtnGpText;

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
			UpdateBtnLogin();
		}

		public override void Close()
		{
			base.Close();
		}

		public void UpdateBtnLogin()
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

		public void BtnLoginClicked()
		{
			//if (Social.localUser.authenticated)
			//{
			//	PlayGamesPlatform.Instance.SignOut();
			//	return;
			//}
			GlobalVariables.ResumeFromDesktop = false;
			//Social.localUser.Authenticate(delegate(bool success)
			//{
			//	UnityEngine.Debug.Log("Authenticate callback retrieved.");
			//	UpdateBtnLogin();
			//	if (success)
			//	{
			//		UnityEngine.Debug.Log("Authentication success!");
			//		((PlayGamesPlatform)Social.Active).SetGravityForPopups(Gravity.TOP);
			//	}
			//});
		}

		public void BtnAchievementClicked()
		{
			Social.ShowAchievementsUI();
		}

		public void BtnLeaderboardClicked()
		{
			Social.ShowLeaderboardUI();
		}
	}
}
