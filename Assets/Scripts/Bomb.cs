using UnityEngine;

public class Bomb : MonoBehaviour
{
	public GameObject MoveLeftText;

	private TextMesh text;

	private void Awake()
	{
		text = MoveLeftText.GetComponent<TextMesh>();
	}

	public void Start()
	{
		text.GetComponent<Renderer>().sortingLayerID = GetComponent<SpriteRenderer>().sortingLayerID;
		text.GetComponent<Renderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder + 1;
	}

	public void SetMoveLeft(int num)
	{
		text.text = num.ToString();
	}
}
