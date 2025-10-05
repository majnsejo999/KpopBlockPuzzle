using BlockGame.GameEngine.Libs.Log;
using BlockGame.New.Core;
using BlockGame.New.Core.UI;
using UnityEngine;

public class ObstacleTutorial : BaseUI
{
	public GameObject GeneralInfo;

	public GameObject BlockInfo;

	public GameObject LockInfo;

	public GameObject RockInfo;

	public GameObject BombInfo;

	public GameObject IconMask;

	public GameObject InfoMask;

	private static ObstacleTutorial instance;

	public static ObstacleTutorial Instance => instance;

	private void Awake()
	{
		instance = this;
		Canvas component = GetComponent<Canvas>();
		ScreenManager.UpdateCanvasCamera(component, UIConfig.DialogSortingOrder);
		InfoMask.transform.position = GameSceneUIManager.Instance.ObstacleLock.transform.position;
		ProcessTexts();
		GeneralInfo.SetActive(true);
		BlockInfo.SetActive(false);
		LockInfo.SetActive(false);
		RockInfo.SetActive(false);
		BombInfo.SetActive(false);
	}

	private void Start()
	{
		UpdateLanguage();
	}

	public void Show(int process)
	{
		GameLogic.Instance.State = GameLogic.GameState.Pause;
		ShapeController.Instance.MoveAllShapeBack();
		switch (process)
		{
		case 5:
			BlockInfo.SetActive(true);
			break;
		case 6:
			LockInfo.SetActive(true);
			break;
		case 7:
			BombInfo.SetActive(true);
			break;
		default:
			break;
		}
	}

	public void GeneralInfoViewed()
	{
		GameLogic.Instance.State = GameLogic.GameState.Run;
		GeneralInfo.SetActive(false);
		base.gameObject.SetActive(false);
	}

	public void SetIconMaskPosition(Vector3 pos)
	{
		IconMask.transform.position = pos;
	}

	public void ProcessToNextStep()
	{
		UserDataManager.Instance.GetService().TutorialProgress++;
		GameLogic.Instance.State = GameLogic.GameState.Run;
		GameSceneUIManager.Instance.MoveObstacleIcon();
		UnityEngine.Object.Destroy(base.gameObject);
	}
}
