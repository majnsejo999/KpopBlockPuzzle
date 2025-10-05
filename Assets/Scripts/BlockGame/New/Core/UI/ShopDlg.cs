using BlockGame.GameEngine.Libs.Log;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BlockGame.New.Core.UI
{
    public class ShopDlg : BaseDialog
    {
        private const int BIN = 1;

        private const int UNDO = 2;

        private const int NO_ADS = 3;

        public GameObject UndoItems;

        public GameObject BinItems;

        public GameObject NoAds;

        protected override void Awake()
        {
            base.Awake();
        }

        private void OnEnable()
        {
        }

        protected override void Start()
        {
            base.Start();
        }
        public void GetItemRotateAds()
        {
            //AdsControl.instance.ShowAds(
            //() =>
            //{
            //    UserDataManager.Instance.GetService().UsedRewardVideo++;
            //    if (UserDataManager.Instance.GetService().UsedRewardVideo == 5)
            //    {
            //         FirebaseControl.instance.LogEventCPA("Feature_selected_ad_reward_5");
            //    }
            //    else if (UserDataManager.Instance.GetService().UsedRewardVideo == 8)
            //    {
            //        FirebaseControl.instance.LogEventCPA("Feature_selected_ad_reward_8");
            //    }
            //    UserDataManager.Instance.GetService().countRota +=1;
            //    DialogManager.Instance.ShowDialog("InfoDlg");
            //    InfoDlg.Instance.UpdateInfo(LanguageManager.GetString("You got 1 extra free rotate"));
            //    if (SceneManager.GetActiveScene().name == "GameScene")
            //    {
            //        GameSceneUIManager.Instance.txt_countRota.text = UserDataManager.Instance.GetService().countRota.ToString();
            //    }
            //    FirebaseControl.instance.LogEventAds("reward_ads", "buy_item_rotate", "succeed");
            //},
            //() =>
            //{
                GameLogic.Instance.ProcessRewardVideoNotFinished();
            //    FirebaseControl.instance.LogEventAds("reward_ads", "buy_item_rotate", "fail");
            //});
        }
        public void PurchaseItem(int idx)
        {
            if (!GlobalVariables.Purchasing)
            {
                Purchaser.Instance.BuyProduct(idx);
            }
        }
    }
}
