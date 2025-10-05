using BlockGame.GameEngine.Libs.DataStore;
using BlockGame.GameEngine.Libs.Log;
using System.IO;
using UnityEngine;

namespace BlockGame.New.Core
{
	public class UserDataManager
	{
		private static UserDataManager instance;

		private string dataFilePath = Application.persistentDataPath + GlobalConstants.UserDataSaveFile;

		private UserData userData;

		public static UserDataManager Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new UserDataManager();
				}
				return instance;
			}
		}

		public UserData GetService()
		{
			return userData;
		}

		public void InitDesEncrypt(string key)
		{
			FileIOHelper.Instance.InitDesEnc(GlobalConstants.DesKey);
		}

		public void Load()
		{
			if (File.Exists(dataFilePath))
			{
				GlobalVariables.FirstEnter = false;
				userData = (UserData)FileIOHelper.Instance.ReadFile(dataFilePath);
			}
			else
			{
				GlobalVariables.FirstEnter = true;
				userData = new UserData();
				Init();
			}
		}

		public void Init()
		{
			userData.Coin = 0;
			userData.Stage = 1;
			userData.SoundEnabled = true;
			userData.MusicEnabled = true;
			userData.HighScore = 0;
			userData.UnDoNum = 0;
			userData.DiscardNum = 0;
			userData.CurrentVersion = string.Empty;
			userData.RemoveAdPurchased = false;
			userData.RemoveVideoPurchased = false;
			userData.PlayNormalCount = 0;
			userData.PlayedGamesInAWeek = 0;
			userData.NumberOfGameOver = 0;
			userData.UsedRewardVideo = 0;
			userData.countRota = 3;
			Save();
		}

		public void Save()
		{
			FileIOHelper.Instance.SaveFile(dataFilePath, userData);
		}
	}
}
