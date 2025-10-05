using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace BlockGame.New.Core.UI
{
	public class BreakRecordDlg : BaseDialog
	{
		public Image medal;

		public Image banner;

		public Image bgLight;

		public ParticleSystem starParticles;

		public override void Close()
		{
			DOTween.KillAll();
			base.Close();
		}

		protected override void Start()
		{
			base.Start();
		}

		public override void Show()
		{
			base.Show();
			Invoke("ProcessToGameOverDlg", 2.5f);
		}

		private void ProcessToGameOverDlg()
		{
			Close();
		}
	}
}
