using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BlockGame.Leah.Core
{
	public class ButtonUtilities : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IEventSystemHandler
	{
		public float disableDuration = 0.4f;

		public bool hasCustomCallback;

		public bool playDefaultSound = true;

		[HideInInspector]
		public Material mat;

		public bool clicking;

		public bool hidingMat;

		private Button btn;

		private void Awake()
		{
			btn = base.gameObject.GetComponent<Button>();
			if (base.gameObject.GetComponent<Image>().material != null && hidingMat)
			{
				mat = base.gameObject.GetComponent<Image>().material;
				ShowEffect(isShow: false);
			}
		}

		private void Start()
		{
			ChangeBtnDisabledColor();
			btn.onClick.AddListener(delegate
			{
				if (playDefaultSound)
				{
					AddClickAudioSource();
				}
				if (!hasCustomCallback)
				{
					PreventMultiTap();
				}
			});
		}

		private void AddClickAudioSource()
		{
			AudioManager.Instance.PlayAudioEffect("button");
		}

		private void ChangeBtnDisabledColor()
		{
			if (btn.transition == Selectable.Transition.ColorTint)
			{
				ColorBlock colors = btn.colors;
				colors.disabledColor = new Color32(150, 150, 150, 240);
				btn.colors = colors;
			}
		}

		private void PreventMultiTap()
		{
			btn.GetComponent<Image>().raycastTarget = false;
			Invoke("ResumeClickable", disableDuration);
		}

		private void ResumeClickable()
		{
			btn.GetComponent<Image>().raycastTarget = true;
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			clicking = true;
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			clicking = false;
		}

		public void ShowEffect(bool isShow)
		{
			if (isShow)
			{
				if (mat != null)
				{
					base.gameObject.GetComponent<Image>().material = mat;
				}
			}
			else if (base.gameObject.GetComponent<Image>().material != null)
			{
				base.gameObject.GetComponent<Image>().material = null;
			}
		}
	}
}
