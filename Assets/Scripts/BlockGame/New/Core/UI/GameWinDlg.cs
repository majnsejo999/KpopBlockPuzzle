using DG.Tweening;
using GooglePlayGames;
using BlockGame.Nova.Conf;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace BlockGame.New.Core.UI
{
	public class GameWinDlg : BaseDialog
	{
		public GameObject BtnRetry;

		public Button BtnMoreGame;

		public Button BtnNoAds;

		public Button BtnRateUs;

		public Text score;

		public Text highScore;

		private GameObject GameOverParticle;

		public GameObject starEffect;

		public GameObject newRecordEffect;

		public Animator newRecordAnim;

		public GameObject NewRecordEffect;

		public Image medal;

		public Image banner;

		public Image bgLight;

		public ParticleSystem newRecordStarEffect;

		public Image iconNewRecord;

		public Image panelNewRecord;

		private Vector3 medalInitPos;

		private static GameWinDlg instance;

		public static GameWinDlg Instance => instance;

		protected override void Awake()
		{
			base.Awake();
			instance = this;
		}

		protected override void Start()
		{
			base.Start();
			newRecordEffect.SetActive(value: false);
			GameOverParticle = UnityEngine.Object.Instantiate(starEffect);
			GameOverParticle.transform.SetParent(base.transform, worldPositionStays: false);
			GameOverParticle.transform.localPosition = new Vector3(0f, 600f);
			GameOverParticle.SetActive(value: false);
			medalInitPos = medal.transform.position;
			UpdateUI();
		}

		private void OnEnable()
		{
		}

		public override void Show()
		{
			iconNewRecord.gameObject.SetActive(value: false);
			TopCanvasManager.Instance.ToggleTouchMask(isActive: false);
			UserDataManager.Instance.Save();
			base.Show();
			if (GameLogic.Instance.Score >= 300)
			{
				GlobalVariables.MoreThan300Score++;
			}
			else
			{
				GlobalVariables.MoreThan300Score = 0;
			}
			if (GameLogic.Instance.Score <= 100)
			{
				GlobalVariables.LessThan100Score++;
			}
			else
			{
				GlobalVariables.LessThan100Score = 0;
			}
			UserDataManager.Instance.GetService().TotalPlayedGames++;
			GlobalVariables.GameOverRewardVideoDisplayed = false;
			AudioManager.Instance.PlayAudioEffect("score");
			TweenUtility.AnimateNum(0, GameLogic.Instance.Score, score, 1.25f);
			if (GlobalVariables.GameType == 0)
			{
				Timer.Schedule(this, 0.4f, delegate
				{
					if (GameOverParticle != null)
					{
						GameOverParticle.SetActive(value: true);
					}
				});
				if (GameLogic.Instance.Score > UserDataManager.Instance.GetService().HighBasicScore)
				{
					if (UserDataManager.Instance.GetService().HighBasicScore > 0)
					{
						TopCanvasManager.Instance.touchMask.SetActive(value: true);
						Timer.Schedule(this, 0.8f, PlayNewRecordEffect);
					}
					UserDataManager.Instance.GetService().HighBasicScore = GameLogic.Instance.Score;
				}
				highScore.text = UserDataManager.Instance.GetService().HighBasicScore.ToString();
				if (GameLogic.Instance.Score > 100 && UserDataManager.Instance.GetService().TotalPlayedGames == 1)
				{
					Timer.Schedule(this, 1f, ShowLeaderBoard);
				}
			}
			else
			{
				if (GlobalVariables.GameType != 1)
				{
					return;
				}
				Timer.Schedule(this, 0.4f, delegate
				{
					if (GameOverParticle != null)
					{
						GameOverParticle.SetActive(value: true);
					}
				});
				if (GameLogic.Instance.Score > UserDataManager.Instance.GetService().HighScore)
				{
					if (UserDataManager.Instance.GetService().HighScore > 0)
					{
						TopCanvasManager.Instance.touchMask.SetActive(true);
						Timer.Schedule(this, 0.8f, PlayNewRecordEffect);
					}
					UserDataManager.Instance.GetService().HighScore = GameLogic.Instance.Score;
					if(UserDataManager.Instance.GetService().HighScore >= 600 && !UserDataManager.Instance.GetService().PushEventHighScore600)
					{
						UserDataManager.Instance.GetService().PushEventHighScore600 = true;
					}
				}
				highScore.text = UserDataManager.Instance.GetService().HighScore.ToString();
			}
		}

		public override void Close()
		{
			base.Close();
			newRecordStarEffect.Stop();
			GameOverParticle.SetActive(value: false);
		}

		public void UpdateUI()
		{
		}

		public void BtnRetryClicked()
		{
			AudioManager.Instance.PlayAudioMusic("bgm_01");
			if (GlobalVariables.GameType == 0 && UserDataManager.Instance.GetService().HighBasicScore >= 1000 && !UserDataManager.Instance.GetService().AdvancedGameModeUnlocked)
			{
				Action cb = delegate
				{
					TopCanvasManager.Instance.ToggleTouchMask(true);
					MainSceneUIManager.Instance.PlayUnlockAdvancedModeAnime();
					SceneTransManager.Instance.SwitchTo("MainScene");
				};
				InfoDlg.Instance.UpdateInfo(LanguageManager.GetString("#unlock_new"), cb);
				DialogManager.Instance.ShowDialog("InfoDlg");
				GameOverParticle.SetActive(value: false);
				Hide();
			}
			else
			{
				GlobalVariables.RestartGame = true;
				GameLogic.Instance.RetryLevel(showTutorial: false, 0.5f);
				Timer.Schedule(this, 0.5f, delegate
				{
					AudioManager.Instance.PlayAudioEffect("game_start");
				});
				GameOverParticle.SetActive(value: false);
				Hide();
			}
		}

		public void BtnMoreGameClicked()
		{
			Application.OpenURL("https://play.google.com/store/apps/dev?id=7806098187455465808");
		}

		public void BtnNoAdsClicked()
		{
			Purchaser.Instance.BuyProduct(0);
		}

		public void BtnBackToMainClicked()
		{
			SceneTransManager.Instance.SwitchTo("MainScene");
			GameOverParticle.SetActive(value: false);
			Hide();
		}

		public void BtnShopClicked()
		{
			DialogManager.Instance.ShowDialog("ShopDlg");
		}

		public void BtnRateClicked()
		{
			GlobalVariables.ResumeFromDesktop = false;
			GlobalVariables.SwitchOutRate = true;
			Application.OpenURL("market://details?id=" + GeneralConfig.PackageName);
		}

		public void BtnContactClicked()
		{
			string text = "support@BlockGame.cn";
			string text2 = WWW.EscapeURL("Block Puzzle Wood Feedback");
			string s = "My suggestion is: \n\n\n\n\n\n\n\n\n\n\n\n\n\nPlease don’t delete the important info below!\nGame Version: " + Application.version + "\nSystem Info: " + SystemInfo.operatingSystem + "\nDevice Info: " + SystemInfo.deviceModel + "\nTimezone: " + TimeZone.CurrentTimeZone.StandardName + "\nScreen Resolution: " + Screen.currentResolution.ToString() + "\nSystem Language: " + Application.systemLanguage;
			s = WWW.EscapeURL(s);
			Application.OpenURL("mailto:" + text + "?subject=" + text2 + "&body=" + s);
		}

		private void Update()
		{
			if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
			{
				UnityEngine.Debug.Log("escape clicked");
				BtnRetryClicked();
			}
		}

		private void ShowRate()
		{
			if (GlobalVariables.IfPopUpRate)
			{
				DialogManager.Instance.ShowDialog("RateDlg");
				GlobalVariables.IfPopUpRate = false;
				UserDataManager.Instance.GetService().TimesOfGameOver = 0;
			}
		}
		private void ShowLeaderBoard()
		{
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
		private void PlayNewRecordEffect()
		{
			AudioManager.Instance.PlayAudioEffect("new_record_trophy");
			medal.gameObject.SetActive(value: true);
			medal.transform.position = medalInitPos;
			iconNewRecord.gameObject.SetActive(value: false);
			banner.gameObject.SetActive(value: true);
			panelNewRecord.gameObject.SetActive(value: true);
			newRecordAnim.GetComponent<Animator>().enabled = true;
			newRecordEffect.gameObject.SetActive(value: true);
			medal.transform.DOMove(iconNewRecord.transform.position, 0.4f).SetDelay(2f).OnStart(delegate
			{
				newRecordAnim.GetComponent<Animator>().enabled = false;
				AudioManager.Instance.PlayAudioEffect("trophy_fly");
				panelNewRecord.gameObject.SetActive(value: false);
				banner.gameObject.SetActive(value: false);
			})
				.OnComplete(delegate
				{
					medal.gameObject.SetActive(value: false);
					newRecordEffect.gameObject.SetActive(value: false);
					iconNewRecord.gameObject.SetActive(value: true);
					iconNewRecord.transform.DOPunchScale(new Vector3(0.02f, 0.02f), 0.8f);
					TopCanvasManager.Instance.touchMask.SetActive(value: false);
					Timer.Schedule(this, 0.2f, delegate
					{
						ShowLeaderBoard();
					});
				});
			medal.transform.DOScale(0.35f, 0.4f).SetDelay(2f);
		}
	}
}
