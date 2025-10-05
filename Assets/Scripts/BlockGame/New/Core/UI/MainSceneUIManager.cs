using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace BlockGame.New.Core.UI
{
    public class MainSceneUIManager : BaseUIManager
    {
        public Button btnPlayNormal;

        public Button btnPlayAdvanced;

        public Text BtnGpText;

        public Button BtnNoAds;

        public Text textAdvanced;

        public GameObject SmallButtons;

        public Button BtnSettings;

        public Button BtnLeaderboard;

        public Button BtnAchievement;

        public Button BtnMoreGames;
        public Sprite[] sprAva;
        public GameObject objDauChamThan;
        private static MainSceneUIManager instance;

        public static MainSceneUIManager Instance => instance;

        protected override void Awake()
        {
            base.Awake();
            instance = this;
            btnPlayAdvanced.interactable = true;
            UserDataManager.Instance.GetService().PlayedGamesInAWeek += 1;
        }

        protected override void Start()
        {
            base.Start();
        }

        public override void ShowUI()
        {
            base.ShowUI();
            PlayBtnEmergeAnime();
            PlayLogoAnime();
            AdjustButtonPosForBanner();
            objDauChamThan.SetActive(true);
        }

        public void BtnNoAdsClicked()
        {
            Purchaser.Instance.BuyProduct(0);
        }

        public void BtnGooglePlayClicked()
        {
            DialogManager.Instance.ShowDialog("GooglePlayDlg");
        }

        public void PlayBtnEmergeAnime()
        {
            btnPlayNormal.transform.localScale = Vector3.zero;
            btnPlayAdvanced.transform.localScale = Vector3.zero;
            btnPlayNormal.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
            btnPlayAdvanced.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
        }

        public void PlayLogoAnime()
        {
            PlayLogoBlockAnime();
        }

        private void PlayLogoBlockAnime()
        {

        }

        public void BtnPlayNormalClicked()
        {
            GlobalVariables.GameType = 0;
            SceneTransManager.Instance.SwitchTo("GameScene");
            AudioManager.Instance.PlayAudioEffect("game_start");
            if (UserDataManager.Instance.GetService().AdvancedGameModeUnlocked)
            {
                return;
            }
            UserDataManager.Instance.GetService().PlayNormalCount++;
        }

        public void BtnPlayAdvancedClicked()
        {
            GlobalVariables.GameType = 1;
            SceneTransManager.Instance.SwitchTo("GameScene");
            AudioManager.Instance.PlayAudioEffect("game_start");
        }

        public void BtnRateClicked()
        {
            DialogManager.Instance.ShowDialog("RateDlg");
        }

        public void PlayUnlockAdvancedModeAnime()
        {
            btnPlayAdvanced.transform.DOPunchScale(new Vector3(0.2f, 0.2f), 1.5f, 4, 0.5f).OnStart(delegate
            {
            }).SetDelay(1.4f)
                .OnComplete(delegate
                {
                    UserDataManager.Instance.GetService().AdvancedGameModeUnlocked = true;
                    TopCanvasManager.Instance.ToggleTouchMask(isActive: false);
                });
        }

        public void BtnSettingsClicked()
        {
            DialogManager.Instance.ShowDialog("MainSettingDlg");
        }

        public void BtnAchievementClicked()
        {
            GlobalVariables.ResumeFromDesktop = false;
        }

        public void BtnLeadervoardClicked()
        {
            objDauChamThan.SetActive(false);
            if (string.IsNullOrEmpty(UserDataManager.Instance.GetService().playerName))
            {
                UserDataManager.Instance.GetService().idDevice = SystemInfo.deviceUniqueIdentifier;
                DialogManager.Instance.ShowDialog("SignInDlg");
            }
            else
            {
                DialogManager.Instance.ShowDialog("LeaderBoardDlg");
            }
        }

        public void BtnMoreGamesClicked()
        {
            Application.OpenURL("https://play.google.com/store/apps/dev?id=7806098187455465808");
        }

        public void AdjustButtonPosForBanner()
        {
            float num = (float)Screen.width * 1f / (float)Screen.height;
            float num2 = (!(num >= 0.5625f)) ? (0.5625f / num) : 1f;
            //if (AdsControl.instance.CheckLoadedBanner)
            //{
            //    SmallButtons.transform.localPosition = new Vector3(0f, -640f * num2 + 100f);
            //}
            //else
            //{
            //    SmallButtons.transform.localPosition = new Vector3(0f, -640f * num2 + 19.5f);
            //}
        }
    }
}
