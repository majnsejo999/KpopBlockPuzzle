using System;

namespace BlockGame.New.Core
{
	[Serializable]
	public class UserData
	{
		public int Stage;

		public int Coin;

		public int MoneySpend;

		public bool SoundEnabled;

		public bool MusicEnabled;

		public int HighScore;

		public int HighBasicScore;

		public long LastQuitTime;

		public bool RemoveAdPurchased;

		public bool RemoveVideoPurchased;

		public int UnDoNum;

		public int DiscardNum;

		public int TutorialProgress;

		public int LastScore;

		public int TimesOfGameOver;

		public bool IfHasSavedProgress;

		public int SavedScore;

		public Cell[,] BoardInfo;

		public int ObstacleId;

		public int ObstacleMoveLeft;

		public int[] ShapeIds;

		public string CurrentVersion;

		public bool IOS_CurrentVersionRated;

		public bool AdvancedGameModeUnlocked;

		public bool ShowingAdvancedGameTutorial;

		public bool TutorialObstacleInfoFinished;

		public bool TutorialGamePlayFinished;

		public bool TutorialBlockFinished;

		public bool TutorialLockFinished;

		public bool TutorialBombFinished;

		public int PlayNormalCount;

		public int PlayAdvancedCount;

		public int UsedRewardVideo;

		public int TotalRemovedobstacles;

		public int TotoalMatchedBlocks;

		public int TotalPutDownBlocks;

		public int TotalPlayedGames;

		public DateTime LastLoginTime;

		public int PlayedGamesInAWeek;
		public int NumberOfGameOver;
		public bool PushEventHighScore600;
		public int countRota;
		public string playerName;
		public string idDevice;
		public int avatar;
	}
}
