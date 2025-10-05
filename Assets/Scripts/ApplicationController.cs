using BlockGame.New.Core;
using System;
using UnityEngine;

public static class ApplicationController
{
	public static void ProcessApplicationQuit()
	{
		UserDataManager.Instance.GetService().LastQuitTime = DateTime.Now.Ticks;
		UserDataManager.Instance.Save();
	}

	public static void ProcessApplicationPause(bool isPause)
	{
		if (isPause)
		{
			UserDataManager.Instance.GetService().LastQuitTime = DateTime.Now.Ticks;
			UserDataManager.Instance.Save();
			return;
		}
		UserDataManager.Instance.Save();
		//if (GlobalVariables.ResumeFromDesktop && !GlobalVariables.Purchasing)
		//{
		//	AdsControl.instance.ShowAdsInter(1,"ResumeFromDesktop");			
		//}
		//else
		//{
		//	GlobalVariables.ResumeFromDesktop = true;
		//}
		if (GlobalVariables.SwitchOutRate)
		{
			GlobalVariables.SwitchOutRate = false;
			DialogManager.Instance.HideDialog("RateDlg");
		}
	}
}
