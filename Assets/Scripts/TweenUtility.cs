using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TweenUtility
{
	public static void AnimateNum(int currentNum, int increment, Text text, float time)
	{
		DOTween.To(() => currentNum, delegate(int x)
		{
			currentNum = x;
		}, currentNum + increment, time).SetEase(Ease.Linear).OnUpdate(delegate
		{
			text.text = currentNum.ToString();
		});
	}
}
