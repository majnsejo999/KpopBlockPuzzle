using BlockGame.GameEngine.Libs.Log;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TopCanvasManager : MonoBehaviour
{
	public GameObject infoBanner;

	public GameObject touchMask;

	public Canvas canvas;

	private static TopCanvasManager instance;

	public static TopCanvasManager Instance => instance;

	private void Awake()
	{
		instance = this;
		base.gameObject.transform.SetParent(GameObject.Find("MyGame").transform);
		ScreenManager.UpdateCanvasCamera(canvas, 500);
		base.gameObject.transform.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
		base.gameObject.transform.GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 1f);
		touchMask.SetActive(value: false);
	}

	private void OnEnable()
	{
	}

	public void ShowMatchedScore()
	{
	}

	public void ShowCheer(int totalRowAndCol)
	{
		GameObject gameObject = Instantiate(Resources.Load("Prefabs/Game/Cheer"), base.transform) as GameObject;
		Image component = gameObject.transform.Find("CheerWord").GetComponent<Image>();
		if (totalRowAndCol <= 5 && totalRowAndCol >= 2)
		{
			Sprite sprite2 = component.sprite = Resources.Load<Sprite>("Textures/Elements2/" + totalRowAndCol);
		}
		else if (totalRowAndCol > 5)
		{
			Sprite sprite4 = component.sprite = Resources.Load<Sprite>("Textures/Elements2/5");
		}
		component.SetNativeSize();
		gameObject.SetActive(value: true);
		Destroy(gameObject, 3f);
	}

	public void ToggleTouchMask(bool isActive)
	{
		touchMask.SetActive(isActive);
	}

	public void ShowTip(string text)
	{
		Debug.Log("show tip " + text);
		GameObject gameObject = UnityEngine.Object.Instantiate(infoBanner);
		gameObject.transform.SetParent(base.gameObject.transform, worldPositionStays: false);
		gameObject.transform.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
		Image component = gameObject.GetComponent<Image>();
		Color color = gameObject.GetComponent<Image>().color;
		float r = color.r;
		Color color2 = gameObject.GetComponent<Image>().color;
		float g = color2.g;
		Color color3 = gameObject.GetComponent<Image>().color;
		component.color = new Color(r, g, color3.b, 1f);
		Text componentInChildren = gameObject.GetComponentInChildren<Text>();
		Color color4 = gameObject.GetComponentInChildren<Text>().color;
		float r2 = color4.r;
		Color color5 = gameObject.GetComponentInChildren<Text>().color;
		float g2 = color5.g;
		Color color6 = gameObject.GetComponentInChildren<Text>().color;
		componentInChildren.color = new Color(r2, g2, color6.b, 1f);
		gameObject.GetComponentInChildren<Text>().text = text;
		StartCoroutine(PlayFoundWordAnime(gameObject));
	}

	private IEnumerator PlayFoundWordAnime(GameObject wordTip)
	{
		bool played = false;
		float t = 0f;
		float a = 0f;
		yield return new WaitForSeconds(0.8f);
		while (!played)
		{
			Color color = wordTip.GetComponent<Image>().color;
			if (color.a <= 0f)
			{
				played = true;
				UnityEngine.Object.Destroy(wordTip);
			}
			wordTip.GetComponent<Image>().color = Color.Lerp(new Color(255f, 255f, 255f, 1f), new Color(255f, 255f, 255f, 0f), t);
			Text componentInChildren = wordTip.GetComponentInChildren<Text>();
			Color color2 = wordTip.GetComponentInChildren<Text>().color;
			float r = color2.r;
			Color color3 = wordTip.GetComponentInChildren<Text>().color;
			float g = color3.g;
			Color color4 = wordTip.GetComponentInChildren<Text>().color;
			Color a2 = new Color(r, g, color4.b, 1f);
			Color color5 = wordTip.GetComponentInChildren<Text>().color;
			float r2 = color5.r;
			Color color6 = wordTip.GetComponentInChildren<Text>().color;
			float g2 = color6.g;
			Color color7 = wordTip.GetComponentInChildren<Text>().color;
			componentInChildren.color = Color.Lerp(a2, new Color(r2, g2, color7.b, 0f), t);
			yield return new WaitForFixedUpdate();
			a += 0.1f;
			t += Time.fixedDeltaTime * a;
		}
	}
}
