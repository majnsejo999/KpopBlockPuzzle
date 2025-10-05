using BlockGame.New.Core;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class SocialPlatformAchievementConfig
{
	public const string achievement_good_luck = "CgkIo_yO-PwdEAIQHg";

	public const string achievement_chain_breaker = "CgkIo_yO-PwdEAIQHA";

	public const string achievement_destroyer_iv = "CgkIo_yO-PwdEAIQFg";

	public const string achievement_challenger = "CgkIo_yO-PwdEAIQCw";

	public const string achievement_hero = "CgkIo_yO-PwdEAIQDg";

	public const string achievement_novice = "CgkIo_yO-PwdEAIQAg";

	public const string achievement_collector_ii = "CgkIo_yO-PwdEAIQGA";

	public const string achievement_collector_iii = "CgkIo_yO-PwdEAIQGQ";

	public const string achievement_rookie = "CgkIo_yO-PwdEAIQCg";

	public const string achievement_collector_iv = "CgkIo_yO-PwdEAIQGg";

	public const string achievement_never_say_never_ii = "CgkIo_yO-PwdEAIQEA";

	public const string achievement_get_some_sleep = "CgkIo_yO-PwdEAIQHw";

	public const string achievement_specialist = "CgkIo_yO-PwdEAIQDQ";

	public const string achievement_wood_blocker = "CgkIo_yO-PwdEAIQEg";

	public const string achievement_expert = "CgkIo_yO-PwdEAIQCA";

	public const string achievement_collector = "CgkIo_yO-PwdEAIQFw";

	public const string leaderboard_classic = "CgkIo_yO-PwdEAIQBA";

	public const string achievement_never_say_never_iii = "CgkIo_yO-PwdEAIQEQ";

	public const string achievement_destroyer_i = "CgkIo_yO-PwdEAIQEw";

	public const string achievement_regular = "CgkIo_yO-PwdEAIQBg";

	public const string achievement_never_say_never = "CgkIo_yO-PwdEAIQDw";

	public const string achievement_master = "CgkIo_yO-PwdEAIQCQ";

	public const string achievement_advanced = "CgkIo_yO-PwdEAIQBw";

	public const string leaderboard_advanced = "CgkIo_yO-PwdEAIQAw";

	public const string achievement_destroyer_iii = "CgkIo_yO-PwdEAIQFQ";

	public const string achievement_semipro = "CgkIo_yO-PwdEAIQDA";

	public const string achievement_bomb_expert = "CgkIo_yO-PwdEAIQHQ";

	public const string achievement_wood_blockaholic = "CgkIo_yO-PwdEAIQGw";

	public const string achievement_destroyer_ii = "CgkIo_yO-PwdEAIQFA";

	public static Dictionary<string, int> ClassicnNormalAchievements = new Dictionary<string, int>
	{
		{
			"CgkIo_yO-PwdEAIQAg",
			200
		},
		{
			"CgkIo_yO-PwdEAIQBg",
			500
		},
		{
			"CgkIo_yO-PwdEAIQBw",
			1000
		},
		{
			"CgkIo_yO-PwdEAIQCA",
			2000
		},
		{
			"CgkIo_yO-PwdEAIQCQ",
			5000
		}
	};

	public static Dictionary<string, int> AdvancedNormalAchievements = new Dictionary<string, int>
	{
		{
			"CgkIo_yO-PwdEAIQCg",
			400
		},
		{
			"CgkIo_yO-PwdEAIQCw",
			800
		},
		{
			"CgkIo_yO-PwdEAIQDA",
			1200
		},
		{
			"CgkIo_yO-PwdEAIQDQ",
			2000
		},
		{
			"CgkIo_yO-PwdEAIQDg",
			3000
		}
	};

	public static Dictionary<string, int> UseContinue = new Dictionary<string, int>
	{
		{
			"CgkIo_yO-PwdEAIQDw",
			5
		},
		{
			"CgkIo_yO-PwdEAIQEA",
			25
		},
		{
			"CgkIo_yO-PwdEAIQEQ",
			50
		}
	};

	public static Dictionary<string, int> RemoveObstacles = new Dictionary<string, int>
	{
		{
			"CgkIo_yO-PwdEAIQEw",
			5
		},
		{
			"CgkIo_yO-PwdEAIQFA",
			25
		},
		{
			"CgkIo_yO-PwdEAIQFQ",
			50
		},
		{
			"CgkIo_yO-PwdEAIQFg",
			100
		}
	};

	public static Dictionary<string, int> MatchRows = new Dictionary<string, int>
	{
		{
			"CgkIo_yO-PwdEAIQFw",
			100
		},
		{
			"CgkIo_yO-PwdEAIQGA",
			300
		},
		{
			"CgkIo_yO-PwdEAIQGQ",
			1000
		},
		{
			"CgkIo_yO-PwdEAIQGg",
			3000
		}
	};

	public static void ReportNewHighScore()
	{
		//if (!Social.localUser.authenticated)
		//{
		//	return;
		//}
		//List<KeyValuePair<string, int>> list = AdvancedNormalAchievements.ToList();
		//for (int i = 0; i < list.Count; i++)
		//{
		//	if (GameLogic.Instance.Score == list[i].Value && i < list.Count - 1)
		//	{
		//		Social.ReportProgress(list[i + 1].Key, 0.0, delegate
		//		{
		//		});
		//	}
		//	if (GameLogic.Instance.Score >= list[i].Value)
		//	{
		//		Social.ReportProgress(list[i].Key, 100.0, delegate
		//		{
		//		});
		//	}
		//}
	}

	public static void ReportNewClassicHighScore()
	{
		//if (!Social.localUser.authenticated)
		//{
		//	return;
		//}
		//List<KeyValuePair<string, int>> list = ClassicnNormalAchievements.ToList();
		//for (int i = 0; i < list.Count; i++)
		//{
		//	if (GameLogic.Instance.Score == list[i].Value && i < list.Count - 1)
		//	{
		//		Social.ReportProgress(list[i + 1].Key, 0.0, delegate
		//		{
		//		});
		//	}
		//	if (GameLogic.Instance.Score >= list[i].Value)
		//	{
		//		Social.ReportProgress(list[i].Key, 100.0, delegate
		//		{
		//		});
		//	}
		//}
	}
}
