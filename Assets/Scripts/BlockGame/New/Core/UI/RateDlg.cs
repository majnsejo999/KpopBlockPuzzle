using BlockGame.Nova.Conf;
using UnityEngine;

namespace BlockGame.New.Core.UI
{
	public class RateDlg : BaseDialog
	{
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
		}

		public void BtnRateClicked()
		{
			GlobalVariables.ResumeFromDesktop = false;
			GlobalVariables.SwitchOutRate = true;
			Application.OpenURL("market://details?id=" + GeneralConfig.PackageName);
		}
	}
}
