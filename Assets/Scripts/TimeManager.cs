using BlockGame.New.Core;
using System;

public static class TimeManager
{
	public static int GetLeaveDay()
	{
		if (UserDataManager.Instance.GetService().LastQuitTime > 0 && UserDataManager.Instance.GetService().LastQuitTime < DateTime.Now.Ticks)
		{
			TimeSpan ts = new TimeSpan(UserDataManager.Instance.GetService().LastQuitTime);
			TimeSpan timeSpan = new TimeSpan(DateTime.Now.Ticks);
			return timeSpan.Subtract(ts).Duration().Days;
		}
		return 0;
	}
}
