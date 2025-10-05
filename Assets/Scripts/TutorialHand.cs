using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TutorialHand : MonoBehaviour
{
	private Vector3 startPos = Vector3.zero;

	private Vector3 endPos = Vector3.zero;

	private Image image;

	private Color transparent = new Color(1f, 1f, 1f, 0f);

	private float handEmergeDelay;

	public void SetPositions(Vector3 startPos, Vector3 endPos, float delay = 0.5f)
	{
		this.startPos = startPos;
		this.endPos = endPos;
		handEmergeDelay = delay;
		HandEmerge();
	}

	private void Start()
	{
		image = GetComponent<Image>();
	}

	public void HandEmerge()
	{
		base.transform.position = startPos;
		if (image == null)
		{
			image = (image = GetComponent<Image>());
		}
		image.color = transparent;
		image.DOFade(1f, 0.4f).SetDelay(handEmergeDelay).OnComplete(delegate
		{
			HandMove();
		});
	}

	private void HandMove()
	{
		base.transform.DOMove(endPos, 1f).OnComplete(delegate
		{
			HandVanish();
		});
	}

	private void HandVanish()
	{
		image.DOFade(0f, 0.4f).OnComplete(delegate
		{
			HandEmerge();
		});
	}

	private void UpdateHandOpacity(float alpha)
	{
		base.gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, alpha);
	}

	private void OnDisable()
	{
		DOTween.Kill(base.transform);
		DOTween.Kill(image);
		base.transform.position = startPos;
		image.color = transparent;
	}

	private void OnEnable()
	{
		if (startPos != Vector3.zero)
		{
			HandEmerge();
		}
	}
}
