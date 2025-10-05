using BlockGame.New.Core;
using UnityEngine;

public class GamePlayTutorial : MonoBehaviour
{
	public GameObject MaskDestination;

	public GameObject MaskStartPoint;

	public GameObject MaskFrame;

	public GameObject Hand;

	private GameObject hand;

	private static GamePlayTutorial instance;

	public static GamePlayTutorial Instance => instance;

	private void Awake()
	{
		instance = this;
		Canvas component = GetComponent<Canvas>();
		ScreenManager.UpdateCanvasCamera(component, 2);
	}

	private void Start()
	{
	}

	public void Show()
	{
		base.gameObject.SetActive(value: true);
		GetMaskPosition();
	}

	public void Hide()
	{
		iTween.Stop(hand);
		hand.SetActive(value: false);
		base.gameObject.SetActive(value: false);
	}

	public void GetMaskPosition()
	{
		int tutorialProgress = UserDataManager.Instance.GetService().TutorialProgress;
		int num = 4;
		int num2 = 4;
		GameObject bg = Board.Instance.BoardInfo[num, num2].Bg;
		float num3 = Board.Instance.cellSize * 100f;
		Shape shape = ShapeController.Instance.slots[1];
		Vector3 shapeStartPos = shape.ShapeStartPos;
		MaskStartPoint.transform.localScale *= 0.95f;
		MaskStartPoint.transform.position = new Vector3(shapeStartPos.x, shapeStartPos.y - 0.17f, shapeStartPos.z);
		switch (tutorialProgress)
		{
		case 0:
		{
			float num5 = 1f;
			if (ScreenManager.AspectRatio > ScreenManager.DesignAspectRatio)
			{
				num5 = ScreenManager.AspectRatio / ScreenManager.DesignAspectRatio;
			}
			Vector2 sizeDelta = new Vector2((num3 * 3f + 10f) * num5, (num3 + 10f) * num5);
			MaskDestination.GetComponent<RectTransform>().sizeDelta = sizeDelta;
			MaskFrame.GetComponent<RectTransform>().sizeDelta = sizeDelta;
			MaskDestination.transform.position = bg.transform.position;
			break;
		}
		case 1:
		{
			float num6 = 1f;
			if (ScreenManager.AspectRatio > ScreenManager.DesignAspectRatio)
			{
				num6 = ScreenManager.AspectRatio / ScreenManager.DesignAspectRatio;
			}
			MaskDestination.GetComponent<RectTransform>().sizeDelta = new Vector2((num3 * 1f + 10f) * num6, (num3 * 3f + 10f) * num6);
			MaskFrame.GetComponent<RectTransform>().sizeDelta = new Vector2((num3 * 1f + 10f) * num6, (num3 * 3f + 10f) * num6);
			MaskDestination.transform.position = bg.transform.position;
			break;
		}
		default:
		{
			float num4 = 1f;
			if (ScreenManager.AspectRatio > ScreenManager.DesignAspectRatio)
			{
				num4 = ScreenManager.AspectRatio / ScreenManager.DesignAspectRatio;
			}
			MaskDestination.GetComponent<RectTransform>().sizeDelta = new Vector2((num3 * 3f + 10f) * num4, (num3 * 3f + 10f) * num4);
			MaskFrame.GetComponent<RectTransform>().sizeDelta = new Vector2((num3 * 3f + 10f) * num4, (num3 * 3f + 10f) * num4);
			MaskDestination.transform.position = bg.transform.position;
			break;
		}
		}
		if (hand == null)
		{
			hand = UnityEngine.Object.Instantiate(Hand);
		}
		float num7 = 45f;
		float num8 = -45f;
		Vector3 position = MaskStartPoint.transform.position;
		position = new Vector3(position.x + num7 / 100f, position.y + num8 / 100f);
		Vector3 position2 = MaskDestination.transform.position;
		position2 = new Vector3(position2.x + num7 / 100f, position2.y + num8 / 100f);
		hand.transform.SetParent(TopCanvasManager.Instance.transform, worldPositionStays: false);
		hand.GetComponent<TutorialHand>().SetPositions(position, position2);
		hand.transform.position = position;
		hand.SetActive(value: true);
		iTween.Init(hand);
	}

	public void ShowHand(bool isShow)
	{
		hand.SetActive(isShow);
	}

	public void OnDestroy()
	{
		hand.SetActive(value: false);
		UnityEngine.Object.Destroy(hand);
	}
}
