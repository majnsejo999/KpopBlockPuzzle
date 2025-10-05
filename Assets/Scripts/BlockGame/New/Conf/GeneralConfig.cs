namespace BlockGame.Nova.Conf
{
	public static class GeneralConfig
	{
		private static string gameName = "Jewel Puzzle Block 2021";

		private static int version = 1;

		public static string PackageName = "com.puzzle.block.jewel.classic.gem.brain.free.game";

		public static int RemindPlayNotificationID = 10000;

		public static int RemindDailyBonusNotificationID = 20000;

		public static int RemindHintNotificationID = 30000;

		public static int RemindRewardNotificationID = 40000;

		public static int Version => version;

		public static string GameName => gameName;
	}
}
