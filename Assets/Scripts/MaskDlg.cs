using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MaskDlg : MonoBehaviour
{
	public Image loadingImg;

	private static MaskDlg instance;

	public static MaskDlg Instance => instance;

	private void Awake()
	{
		instance = this;
	}

	public void Enable()
	{
		loadingImg.transform.localEulerAngles = Vector3.zero;
		loadingImg.transform.DORotate(new Vector3(0f, 0f, -360f), 2.5f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1);
		base.gameObject.SetActive(value: true);
	}

	public void Disable()
	{
		DOTween.Kill(loadingImg.transform);
		base.gameObject.SetActive(value: false);
	}
}
