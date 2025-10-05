using UnityEngine;
using UnityEngine.UI;

namespace BlockGame.New.Core.UI
{
    public class SettingDlg : BaseDialog
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
            UpdateToggles();
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

        public void RestartLevel()
        {
            GlobalVariables.GameOverRewardVideoDisplayed = false;
            GlobalVariables.RestartGame = true;
            GameLogic.Instance.RetryLevel();
           // AdsControl.instance.ShowAdsInter(1, "RetryLevel");
            base.Close();
        }

        public void ShowShopDlg()
        {
            DialogManager.Instance.ShowDialog("ShopDlg");
        }

        public void BackMainMenuClicked()
        {
            Close();
           // AdsControl.instance.ShowAdsInter(1, "BackMainMenu");
            SceneTransManager.Instance.SwitchTo("MainScene");
        }

        public void ShowHowToPlayDlg()
        {
        }
    }
}
