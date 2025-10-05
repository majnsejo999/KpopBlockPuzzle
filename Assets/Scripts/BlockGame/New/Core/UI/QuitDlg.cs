using UnityEngine;

namespace BlockGame.New.Core.UI
{
	public class QuitDlg : BaseDialog
	{
		protected override void Start()
		{
			base.Start();
		}

		public void BtnOkClicked()
		{
			UnityEngine.Debug.Log("quit game");
			Application.Quit();
		}
	}
}
