using DG.Tweening;
using GooglePlayGames;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.UI;

namespace BlockGame.New.Core.UI
{
    public class GameOverRewardVideoDlg : BaseDialog
    {
        public Text txtProcess;

        public Button btnContinue;

        public Button btnCancel;

        public Text text;
        private bool showForceAds;

        protected override void Start()
        {
            base.Start();
        }

        public override void Show()
        {
            base.Show();
            showForceAds = false;
            TopCanvasManager.Instance.ToggleTouchMask(isActive: false);
            btnCancel.gameObject.SetActive(value: false);
            btnCancel.transform.localScale = Vector3.zero;
            Timer.Schedule(this, 3f, delegate
            {
                btnCancel.gameObject.SetActive(value: true);
                btnCancel.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack);
            });
            GlobalVariables.RowOrCol = ((Random.Range(0, 2) == 0) ? true : false);
            if (GlobalVariables.RowOrCol)
            {
                text.text = LanguageManager.GetString("#rv_desc_2");
            }
            else
            {
                text.text = LanguageManager.GetString("#rv_desc_1");
            }
            GlobalVariables.GameOverRewardVideoDisplayed = true;
            float fillValue = 1f;
            txtProcess.text = ((int)(fillValue * 8)).ToString();
            DOTween.To(() => fillValue, delegate (float x)
            {
                fillValue = x;
            }, 0f, 8f).SetDelay(0.2f).SetEase(Ease.Linear)
                .OnUpdate(delegate
                {
                    txtProcess.text = ((int)(fillValue * 8)).ToString();
                })
                .OnComplete(delegate
                {
                    BtnCancelClicked();
                });
            btnContinue.transform.localScale = Vector3.one;
        }

        public void BtnContinueClicked()
        {
            DOTween.KillAll();
            //if (Social.localUser.authenticated)
            //{
            //    List<KeyValuePair<string, int>> list = SocialPlatformAchievementConfig.UseContinue.ToList();
            //    for (int i = 0; i < list.Count; i++)
            //    {
            //        PlayGamesPlatform.Instance.IncrementAchievement(list[i].Key, 1, delegate
            //        {
            //        });
            //        if (UserDataManager.Instance.GetService().UsedRewardVideo == list[i].Value && i < list.Count - 1)
            //        {
            //            Social.ReportProgress(list[i + 1].Key, 0.0, delegate
            //            {
            //            });
            //        }
            //    }
            //}
            GameLogic.Instance.State = GameLogic.GameState.Over;
            Hide();
            //if (AdsControl.instance.CheckIsReadlyAds())
            //{
            //    AdsControl.instance.ShowAds(
            //        () =>
            //        {
            //            UserDataManager.Instance.GetService().UsedRewardVideo++;
            //            if(UserDataManager.Instance.GetService().UsedRewardVideo == 5)
            //            {
            //                FirebaseControl.instance.LogEventCPA("Feature_selected_ad_reward_5");
            //            }
            //            else if (UserDataManager.Instance.GetService().UsedRewardVideo == 8)
            //            {
            //                FirebaseControl.instance.LogEventCPA("Feature_selected_ad_reward_8");
            //            }
            //            GameLogic.Instance.ProcessRewardVideoFinished();
            //            FirebaseControl.instance.LogEventAds("reward_ads", "revive", "succeed");
            //            showForceAds = true;
            //        },
            //        () =>
            //        {
            //            GameLogic.Instance.ProcessRewardVideoNotFinished();
            //            FirebaseControl.instance.LogEventAds("reward_ads", "revive", "fail");
            //        });
            //}
            //else
            //{
                GameLogic.Instance.ContinueGame();
            //}
        }
        public void BtnCancelClicked()
        {
            DOTween.KillAll();
            GameLogic.Instance.ProcessGameOver();
            Hide();
            //if (!showForceAds)
            //{
            //    AdsControl.instance.ShowAdsInter(1, "game_over");
            //}
        }
    }
}
