namespace BlockGame.Nova.Conf
{
	public static class Configs
	{
		public static void Init()
		{
			GameConfig.Init();
			DailySpinConfig.LoadDailySpinConfig();
			ExtraWordConfig.LoadExtraWordConfig();
			RemindRewardConfig.LoadRemindRewardConfig();
			SuperSaleConfig.Init();
			ShopConfig.LoadShopConfig();
		}
	}
}
