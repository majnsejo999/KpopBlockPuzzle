using UnityEngine;

namespace BlockGame.New.Core.UI
{
	public class RemoveAdsDlg : BaseDialog
	{
		public GameObject MusicButton;

		public GameObject SoundButton;

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

		public void PurchaseClicked(int type)
		{
			Purchaser.Instance.BuyProduct(type);
		}
	}
}
