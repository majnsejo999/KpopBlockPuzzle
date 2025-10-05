using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MatchScore : MonoBehaviour
{
	public Text score;

	private void Awake()
	{
	}

	private void Start()
	{
		Transform transform = base.transform;
		Vector3 position = base.transform.position;
		transform.DOMoveY(position.y + 0.55f, 1f).SetEase(Ease.OutBack);
		FadeOut();
	}

	public void SetScore(int score)
	{
		this.score.text = score.ToString();
	}

	public void FadeOut()
	{
		score.DOFade(0f, 0.7f).SetDelay(0.3f).OnComplete(delegate
		{
			UnityEngine.Object.Destroy(base.gameObject);
		});
	}
}
