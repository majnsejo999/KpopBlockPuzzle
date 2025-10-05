using DG.Tweening;
using BlockGame.New.Core;
using BlockGame.New.Core.UI;
using UnityEngine;

public class Cell
{
	public int row;

	public int col;

	public Enums.CellState State;

	public float Size;

	public int Color;

	public int PreviewColor;

	public GameObject Bg;

	public GameObject Block;

	public GameObject Lock;

	public GameObject Rock;

	public GameObject Bomb;

	private GameObject _board;

	private Block blockScript;

	private SpriteRenderer blockRenderer;

	public Cell(int r, int c, float size, GameObject board)
	{
		row = r;
		col = c;
		_board = board;
		Bg = Object.Instantiate(Resources.Load<GameObject>("Prefabs/Game/CellBg"));
		Block = Object.Instantiate(Resources.Load<GameObject>("Prefabs/Game/Block"));
		State = Enums.CellState.Default;
		Bg.name = r + "," + c;
		Size = size;
		Bg.GetComponent<SpriteRenderer>().size = new Vector2(size, size);
		Block.GetComponent<SpriteRenderer>().size = new Vector2(size, size);
		Bg.transform.SetParent(_board.transform, worldPositionStays: false);
		Block.transform.SetParent(Bg.transform, worldPositionStays: false);
		Block.SetActive(value: false);
		blockScript = Block.GetComponent<Block>();
		blockRenderer = Block.GetComponent<SpriteRenderer>();
	}

	public void SetCellBlockColor(int color)
	{
		Color = color;
	}

	public void SetCellPreviewColor(int color)
	{
		PreviewColor = color;
		Block.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/GameScene/d" + color);
	}

	public void SetOriColor()
	{
		Block.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/GameScene/d" + Color);
	}

	public void SetCellLock(bool playAnime = true)
	{
		State = Enums.CellState.Locked;
		if (Lock == null)
		{
			Lock = Object.Instantiate(Resources.Load<GameObject>("Prefabs/Game/Lock"));
			Lock.SetActive(value: false);
			Lock.GetComponent<SpriteRenderer>().size = new Vector2(Size, Size);
			Lock.transform.SetParent(Bg.transform, worldPositionStays: false);
		}
		Lock.SetActive(value: true);
		Block.SetActive(value: true);
		if (playAnime)
		{
			PlayEmergeAnime(Lock);
		}
		else
		{
			Lock.GetComponent<SpriteRenderer>().color = UnityEngine.Color.white;
		}
	}

	public void SetCellRock(bool playAnime = true)
	{
		State = Enums.CellState.Rock;
		if (Rock == null)
		{
			Rock = Object.Instantiate(Resources.Load<GameObject>("Prefabs/Game/Rock"));
			Rock.SetActive(value: false);
			Rock.GetComponent<SpriteRenderer>().size = new Vector2(Size, Size);
			Rock.transform.SetParent(Bg.transform, worldPositionStays: false);
		}
		Rock.SetActive(value: true);
		if (playAnime)
		{
			PlayEmergeAnime(Rock);
		}
		else
		{
			Rock.GetComponent<SpriteRenderer>().color = UnityEngine.Color.white;
		}
	}

	public void SetCellBomb(bool playAnime = true)
	{
		State = Enums.CellState.Bomb;
		if (Bomb == null)
		{
			Bomb = Object.Instantiate(Resources.Load<GameObject>("Prefabs/Game/Bomb"));
			Bomb.SetActive(value: false);
			Bomb.GetComponent<SpriteRenderer>().size = new Vector2(Size, Size);
			Bomb.transform.SetParent(Bg.transform, worldPositionStays: false);
		}
		Bomb.SetActive(value: true);
		Block.SetActive(value: false);
		if (playAnime)
		{
			PlayEmergeAnime(Bomb);
		}
		else
		{
			Bomb.GetComponent<SpriteRenderer>().color = UnityEngine.Color.white;
		}
	}

	public void SetCellObstacleFilled()
	{
		State = Enums.CellState.Filled;
		Block.transform.localScale = Vector3.one;
		PlayEmergeAnime(Block);
	}

	public void SetCellFilled()
	{
		State = Enums.CellState.Filled;
		Block.transform.localScale = Vector3.one;
		Block.SetActive(value: true);
		if(UserDataManager.Instance.GetService().TutorialProgress == 0)
		{
			Block.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/GameScene/d" + 3);
		}
		else if (UserDataManager.Instance.GetService().TutorialProgress == 1)
		{
			Block.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/GameScene/d" + 3);
		}
		else if(UserDataManager.Instance.GetService().TutorialProgress == 2)
		{
			Block.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/GameScene/d" + 2);
		}
		if (Lock != null)
		{
			Lock.SetActive(value: false);
		}
		if (Rock != null)
		{
			Rock.SetActive(value: false);
		}
	}

	public void SetCellPreview(bool display)
	{
		float a = (!display) ? 1f : 0.4f;
		SetOriColor();
		blockRenderer.color = new Color(1f, 1f, 1f, a);
		Block.SetActive(display);
		State = (display ? Enums.CellState.Preview : Enums.CellState.Default);
	}

	public void SetCellDefault()
	{
		State = Enums.CellState.Default;
		Block.SetActive(value: false);
		if (Bomb != null)
		{
			GameLogic.Instance.Bombs.Remove(Bomb.GetComponent<Bomb>());
			Bomb.SetActive(value: false);
		}
		if (Lock != null)
		{
			Lock.SetActive(value: false);
		}
		if (Rock != null)
		{
			Rock.SetActive(value: false);
		}
	}

	public void SetCellState(Enums.CellState state)
	{
		switch (state)
		{
		case Enums.CellState.Default:
			SetCellDefault();
			break;
		case Enums.CellState.Filled:
			SetCellFilled();
			break;
		case Enums.CellState.Locked:
			SetCellLock();
			break;
		case Enums.CellState.Rock:
			SetCellRock();
			break;
		case Enums.CellState.Bomb:
			SetCellBomb();
			break;
		}
	}

	public void SetCellGray(int index, float time)
	{
		blockScript.SetBlockGray(isGray: true);
		float delay = (float)index * time;
		float greyValue = 0f;
		DOTween.To(() => greyValue, delegate(float x)
		{
			greyValue = x;
		}, 1f, 0.3f).SetDelay(delay).OnUpdate(delegate
		{
			blockScript.UpdateGrayScale(greyValue);
		})
			.OnComplete(delegate
			{
				Board.Instance.CheckGrayCellCount();
			});
	}

	public void SetCellColorNormal()
	{
		blockScript.SetBlockGray(isGray: false);
	}

	private void PlayEmergeAnime(GameObject go)
	{
		GameSceneUIManager.Instance.UpdateObstacleIcon(Block.transform.position, go);
		go.SetActive(value: true);
		go.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.6f);
	}
}
