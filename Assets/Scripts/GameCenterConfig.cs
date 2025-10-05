using System.Collections.Generic;

public static class GameCenterConfig
{
	public const string achievement_good_luck = "achievement_good_luck";

	public const string achievement_chain_breaker = "achievement_chain_breaker";

	public const string achievement_destroyer_iv = "achievement_destroyer_iv";

	public const string achievement_challenger = "achievement_challenger";

	public const string achievement_hero = "achievement_hero";

	public const string achievement_novice = "achievement_novice";

	public const string achievement_collector_ii = "achievement_collector_ii";

	public const string achievement_collector_iii = "achievement_collector_iii";

	public const string achievement_rookie = "achievement_rookie";

	public const string achievement_collector_iv = "achievement_collector_iv";

	public const string achievement_never_say_never_ii = "achievement_never_say_never_ii";

	public const string achievement_get_some_sleep = "achievement_get_some_sleep";

	public const string achievement_specialist = "achievement_specialist";

	public const string achievement_wood_blocker = "achievement_wood_blocker";

	public const string achievement_expert = "achievement_expert";

	public const string achievement_collector = "achievement_collector";

	public const string leaderboard_classic = "classic_mode";

	public const string achievement_never_say_never_iii = "achievement_never_say_never_iii";

	public const string achievement_destroyer_i = "achievement_destroyer";

	public const string achievement_regular = "achievement_regular";

	public const string achievement_never_say_never = "achievement_never_say_never";

	public const string achievement_master = "achievement_master";

	public const string achievement_advanced = "achievement_advanced";

	public const string leaderboard_advanced = "advanced_mode";

	public const string achievement_destroyer_iii = "achievement_destroyer_iii";

	public const string achievement_semipro = "achievement_semipro";

	public const string achievement_bomb_expert = "achievement_bomb_expert";

	public const string achievement_wood_blockaholic = "achievement_wood_blockaholic";

	public const string achievement_destroyer_ii = "achievement_destroyer_ii";

	public static Dictionary<string, int> ClassicnNormalAchievements = new Dictionary<string, int>
	{
		{
			"achievement_novice",
			200
		},
		{
			"achievement_regular",
			500
		},
		{
			"achievement_advanced",
			1000
		},
		{
			"achievement_expert",
			2000
		},
		{
			"achievement_master",
			5000
		}
	};

	public static Dictionary<string, int> AdvancedNormalAchievements = new Dictionary<string, int>
	{
		{
			"achievement_rookie",
			400
		},
		{
			"achievement_challenger",
			800
		},
		{
			"achievement_semipro",
			1200
		},
		{
			"achievement_specialist",
			2000
		},
		{
			"achievement_hero",
			3000
		}
	};

	public static Dictionary<string, int> UseContinue = new Dictionary<string, int>
	{
		{
			"achievement_never_say_never",
			5
		},
		{
			"achievement_never_say_never_ii",
			25
		},
		{
			"achievement_never_say_never_iii",
			50
		}
	};

	public static Dictionary<string, int> RemoveObstacles = new Dictionary<string, int>
	{
		{
			"achievement_destroyer",
			5
		},
		{
			"achievement_destroyer_ii",
			25
		},
		{
			"achievement_destroyer_iii",
			50
		},
		{
			"achievement_destroyer_iv",
			100
		}
	};

	public static Dictionary<string, int> MatchRows = new Dictionary<string, int>
	{
		{
			"achievement_collector",
			100
		},
		{
			"achievement_collector_ii",
			300
		},
		{
			"achievement_collector_iii",
			1000
		},
		{
			"achievement_collector_iv",
			3000
		}
	};
}
