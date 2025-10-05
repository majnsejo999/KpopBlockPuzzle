using BlockGame.New.Core.UI;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
	public string[] GameDialogs = new string[13]
	{
		"GameWinDlg",
		"BreakRecordDlg",
		"GameOverRewardVideoDlg",
		"ShopDlg",
		"MainSettingDlg",
		"SettingDlg",
		"InfoDlg",
		"RateDlg",
		"QuitDlg",
		"GooglePlayDlg",
		"RemoveAdsDlg",
		"SignInDlg",
		"LeaderBoardDlg"
	};

	public Dictionary<string, GameObject> dialogInstances = new Dictionary<string, GameObject>();

	private static DialogManager instance;

	public Canvas canvas;

	public static DialogManager Instance => instance;

	public void Awake()
	{
		UnityEngine.Debug.Log("Dialog manager awake");
		instance = this;
		canvas = base.gameObject.GetComponent<Canvas>();
		ScreenManager.UpdateCanvasCamera(canvas, UIConfig.DialogSortingOrder);
	}

	public void Start()
	{
		//Init();
	}

	public void Init()
	{
		base.gameObject.transform.SetParent(GameObject.Find("UI").transform);
		base.gameObject.transform.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
		base.gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 1f);
	}

	public void CreateDialog(GameObject root, string name)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Prefabs/UI/Dialogs/" + name));
		gameObject.name = gameObject.name.Replace("(Clone)", string.Empty);
		gameObject.transform.SetParent(root.transform);
		gameObject.transform.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
		gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 0f);
		dialogInstances.Add(name, gameObject);
		gameObject.SetActive(value: false);
	}

	public void ShowDialog(string name)
	{
		(dialogInstances[name].GetComponent(name) as BaseDialog).Show();
	}

	public void HideDialog(string name)
	{
		(dialogInstances[name].GetComponent(name) as BaseDialog).Hide();
	}

	public void CloseAllDialogs()
	{
		string[] array = GameDialogs;
		foreach (string text in array)
		{
			if (dialogInstances[text].activeSelf)
			{
				UnityEngine.Debug.Log("hide dialog " + text);
				dialogInstances[text].SendMessage("Hide");
			}
		}
	}

	public bool IsDialogShowing()
	{
		string[] array = GameDialogs;
		foreach (string key in array)
		{
			if (dialogInstances[key].activeSelf)
			{
				return true;
			}
		}
		return false;
	}
}
