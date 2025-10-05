using System;
using UnityEngine.UI;

namespace BlockGame.New.Core.UI
{
	public class InfoDlg : BaseDialog
	{
		public Text info;

		public Action callback;

		private static InfoDlg instance;

		public static InfoDlg Instance => instance;

		protected override void Awake()
		{
			base.Awake();
			instance = this;
		}

		protected override void Start()
		{
			base.Start();
		}

		public override void Show()
		{
			base.Show();
		}

		public void UpdateInfo(string info, Action cb = null)
		{
			this.info.text = info;
			callback = cb;
		}

		public void BtnOkClicked()
		{
			if (callback != null)
			{
				callback();
				Hide();
			}
			else
			{
				base.Close();
			}
		}
	}
}
