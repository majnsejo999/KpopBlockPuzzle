using UnityEngine;

namespace BlockGame.New.Core.UI
{
	public class BaseUIManager : BaseUI
	{
		protected virtual void Awake()
		{
			base.gameObject.transform.SetParent(GameObject.Find("UI").transform);
			base.gameObject.transform.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
			base.gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 1f);
			base.gameObject.SetActive(value: false);
			ProcessTexts();
		}

		protected virtual void Start()
		{
		}

		protected virtual void OnEnable()
		{
			UpdateCamera();
		}

		public virtual void RefreshUI()
		{
			UpdateCamera();
			HideUI();
		}

		public virtual void HideUI()
		{
			base.gameObject.SetActive(value: false);
		}

		public virtual void ShowUI()
		{
			UpdateLanguage();
			base.gameObject.SetActive(value: true);
		}

		public void UpdateCamera()
		{
			GetComponent<Canvas>().worldCamera = Camera.main;
			GetComponent<Canvas>().sortingLayerName = "UI";
			GetComponent<Canvas>().sortingOrder = UIConfig.BaseUISortingOrder;
		}
	}
}
