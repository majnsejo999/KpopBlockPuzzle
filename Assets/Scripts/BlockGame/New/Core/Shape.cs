using DG.Tweening;
using BlockGame.GameEngine.Libs.Log;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BlockGame.New.Core.UI;

namespace BlockGame.New.Core
{
	public class Shape : MonoBehaviour
	{
		public Sprite normalBlock;

		public Sprite highlightBlock;

		public int rowSize;

		public int colSize;

		private bool isSelectionDone;

		public int index;

		public List<int> grid;

		public GameObject[,] shapeGridWithoutShadow;

		public GameObject[,] shapeGrid;

		public int color;

		private BoxCollider2D collider;

		public BoxCollider2D binCollider;

		private float _shapeDisplayOffset = 2.75f;

		private bool _moveDone;

		private bool _scaleDone;

		private Vector3 _touchStartPos;

		private Vector3 _shapeSelectedOriginPos;

		public Vector3 ShapeStartPos;

		public Vector3 ShapeStartScale;

		private float cellSize = 0.4f;
		private float cellSize1 = 0.5f;

		private int fillingBlockCount;
		public int[] grids;
		public RotaShape rota;
		private float timeout;
		private bool runTimeOut;
		public Sprite[] spriteColorBlock;
		public void Create(int r, int c, int[] grid, int index, int color)
		{
			timeout = 0;
			runTimeOut = false;
			int num = 0;
			float num2 = 0f;
			float num3 = 0f;
			rowSize = r;
			colSize = c;
			this.index = index;
			this.color = color;
			grids = grid;
			shapeGrid = new GameObject[rowSize, colSize];
			shapeGridWithoutShadow = new GameObject[rowSize, colSize];
			collider = GetComponent<BoxCollider2D>();
			for (int i = 0; i < rowSize; i++)
			{
				for (int j = 0; j < colSize; j++)
				{
					if (grid[(rowSize - i - 1) * colSize + j] == 1)
					{
						shapeGrid[i, j] = Instantiate(Resources.Load<GameObject>("Prefabs/Game/BlockShadow"));
						shapeGrid[i, j].GetComponent<SpriteRenderer>().sprite = spriteColorBlock[this.color];
						shapeGridWithoutShadow[i, j] = Instantiate(Resources.Load<GameObject>("Prefabs/Game/Block"));
						shapeGridWithoutShadow[i, j].SetActive(value: false);
						float num4 = (float)j * cellSize1 + cellSize1 / 2f + (float)(num * j) - cellSize1 * (float)colSize / 2f + num2;
						float num5 = (float)i * cellSize1 + (float)(num * i) - cellSize1 * (float)(rowSize-1) / 2f - num3;
						shapeGrid[i, j].transform.SetParent(base.gameObject.transform, false);
						shapeGridWithoutShadow[i, j].transform.SetParent(base.gameObject.transform, worldPositionStays: false);
						SpriteRenderer component = shapeGrid[i, j].GetComponent<SpriteRenderer>();
						component.size = new Vector2(cellSize * 9f / 7f, cellSize * 9f / 7f);
						SpriteRenderer component2 = shapeGridWithoutShadow[i, j].GetComponent<SpriteRenderer>();
						component2.size = new Vector2(cellSize, cellSize);
						component.sortingLayerName = "UI";
						component2.sortingLayerName = "UI";
						component.sortingOrder = 3 + (3 - i);
						component2.sortingOrder = 3 + (3 - i);
						shapeGrid[i, j].transform.localPosition = new Vector3(num4, num5);
						shapeGridWithoutShadow[i, j].transform.localPosition = new Vector3(num4, num5);
					}
					else
					{
						shapeGrid[i, j] = null;
					}
				}
			}
			float num6 = (float)colSize * 0.4f;
			float num7 = (float)rowSize * 0.4f;
			base.transform.localScale = new Vector3(0.75f,0.75f, 0.75f);
			collider.isTrigger = true;
			Vector2 offset = collider.offset;
			collider.offset = new Vector2(offset.x, offset.y);
		}
		private void OnMouseDown()
		{
			if (GameLogic.Instance.State != GameLogic.GameState.Run)
			{
				return;
			}
			if (UserDataManager.Instance.GetService().TutorialProgress < 3 && GamePlayTutorial.Instance != null)
			{
				GamePlayTutorial.Instance.ShowHand(isShow: false);
			}
			if (rota.CanRota && rota.gameObject.activeInHierarchy)
			{
				rota.CanRota = false;
				timeout = 0;
				runTimeOut = true;
			}
			else
			{
				_touchStartPos = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
				Hashtable hashtable = new Hashtable();
				float num = Board.Instance.cellSize / 0.5f / ScreenManager.ResolutionAdaptionRatio;
				base.transform.DOScale(new Vector3(num, num), 0.04f).OnComplete(delegate
				{
					SetScaleDone();
				});
				base.transform.DOLocalMoveY(_shapeDisplayOffset, 0.04f).OnComplete(delegate
				{
					SetMoveDone();
				});
				GameObject[,] array = shapeGrid;
				int length = array.GetLength(0);
				int length2 = array.GetLength(1);
				for (int i = 0; i < length; i++)
				{
					for (int j = 0; j < length2; j++)
					{
						GameObject gameObject = array[i, j];
						if (gameObject != null)
						{
							SpriteRenderer component = gameObject.GetComponent<SpriteRenderer>();
							component.size = new Vector2(cellSize * 9f / 7f * 0.9f, cellSize * 9f / 7f * 0.9f);
						}
					}
				}
				GameObject[,] array2 = shapeGridWithoutShadow;
				int length3 = array2.GetLength(0);
				int length4 = array2.GetLength(1);
				for (int k = 0; k < length3; k++)
				{
					for (int l = 0; l < length4; l++)
					{
						GameObject gameObject2 = array2[k, l];
						if (gameObject2 != null)
						{
							SpriteRenderer component2 = gameObject2.GetComponent<SpriteRenderer>();
							component2.size = new Vector2(cellSize * 0.9f, cellSize * 0.9f);
						}
					}
				}
			}
		}
		private void Update()
		{
			if (runTimeOut)
			{
				timeout += Time.deltaTime;
				if(timeout > 0.3f)
				{
					timeout = 0;
					runTimeOut = false;
					LongRunningMethod();
				}
			}
		}
		void LongRunningMethod()
		{
			rota.gameObject.SetActive(false);
			rota.CanRota = true;
			_touchStartPos = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
			Hashtable hashtable = new Hashtable();
			float num = Board.Instance.cellSize / 0.5f / ScreenManager.ResolutionAdaptionRatio;
			base.transform.DOScale(new Vector3(num, num), 0.04f).OnComplete(delegate
			{
				SetScaleDone();
			});
			base.transform.DOLocalMoveY(_shapeDisplayOffset, 0.04f).OnComplete(delegate
			{
				SetMoveDone();
			});
			GameObject[,] array = shapeGrid;
			int length = array.GetLength(0);
			int length2 = array.GetLength(1);
			for (int i = 0; i < length; i++)
			{
				for (int j = 0; j < length2; j++)
				{
					GameObject gameObject = array[i, j];
					if (gameObject != null)
					{
						SpriteRenderer component = gameObject.GetComponent<SpriteRenderer>();
						component.size = new Vector2(cellSize * 9f / 7f * 0.9f, cellSize * 9f / 7f * 0.9f);
					}
				}
			}
			GameObject[,] array2 = shapeGridWithoutShadow;
			int length3 = array2.GetLength(0);
			int length4 = array2.GetLength(1);
			for (int k = 0; k < length3; k++)
			{
				for (int l = 0; l < length4; l++)
				{
					GameObject gameObject2 = array2[k, l];
					if (gameObject2 != null)
					{
						SpriteRenderer component2 = gameObject2.GetComponent<SpriteRenderer>();
						component2.size = new Vector2(cellSize * 0.9f, cellSize * 0.9f);
					}
				}
			}
		}
		private void OnMouseUp()
		{
			if (!rota.CanRota && runTimeOut)
			{
				rota.CanRota = true;
				rota.z += 90;
				if (rota.z >= 360)
				{
					rota.z = 0;
				}
				timeout = 0;
				runTimeOut = false;
				rota.Rotate = new Vector3(0, 0, rota.z);
				base.transform.DORotate(rota.Rotate, 0.04f);
			}
			if (GameLogic.Instance.State != GameLogic.GameState.Run)
			{
				return;
			}
			if (UserDataManager.Instance.GetService().TutorialProgress < 3 && GamePlayTutorial.Instance != null)
			{
				GamePlayTutorial.Instance.ShowHand(isShow: true);
			}
			if (GameLogic.Instance.State != GameLogic.GameState.Run)
			{
				return;
			}
			if (IfShapeCanFill())
			{
				int tutorialProgress = UserDataManager.Instance.GetService().TutorialProgress;
				if (tutorialProgress < 3)
				{
					if (CheckTutorialFill(tutorialProgress))
					{
						PutDownShape();
						ShapeController.Instance.slots[index] = null;
					}
					else
					{
						MoveShapeBack();
					}
				}
				else if (tutorialProgress == 4)
				{
					Board.Instance.HideBlockPreview();
					MoveShapeBack();
				}
				else
				{
					PutDownShape();
					ShapeController.Instance.slots[index] = null;
				}
			}
			else
			{
				MoveShapeBack();
			}
		}

		private bool CheckTutorialFill(int progress)
		{
			switch (progress)
			{
			case 0:
				for (int k = 3; k < 6; k++)
				{
					if (!Board.Instance.IfColMatch(k))
					{
						Board.Instance.SetBlocksOriColor();
						Board.Instance.HideBlockPreview();
						return false;
					}
				}
				break;
			case 1:
				for (int l = 3; l < 6; l++)
				{
					if (!Board.Instance.IfRowMatch(l))
					{
						Board.Instance.SetBlocksOriColor();
						Board.Instance.HideBlockPreview();
						return false;
					}
				}
				break;
			default:
				for (int i = 3; i < 6; i++)
				{
					if (!Board.Instance.IfColMatch(i))
					{
						Board.Instance.SetBlocksOriColor();
						Board.Instance.HideBlockPreview();
						return false;
					}
				}
				for (int j = 3; j < 6; j++)
				{
					if (!Board.Instance.IfRowMatch(j))
					{
						return false;
					}
				}
				break;
			}
			return true;
		}

		public void MoveShapeBack()
		{
			if (GameSceneUIManager.Instance.turnONRota)
			{
				if (rowSize != colSize || System.Array.Exists(grids, element => element == 0))
				{
					rota.gameObject.SetActive(true);
				}
			}
			base.transform.DOScale(ShapeStartScale, 0.04f).OnComplete(delegate
			{
				SetScaleBackDone();
			});
			base.transform.DOMove(ShapeStartPos, 0.04f).OnComplete(delegate
			{
				SetMoveBackDone();
			});
			GameObject[,] array = shapeGrid;
			int length = array.GetLength(0);
			int length2 = array.GetLength(1);
			for (int i = 0; i < length; i++)
			{
				for (int j = 0; j < length2; j++)
				{
					GameObject gameObject = array[i, j];
					if (gameObject != null)
					{
						SpriteRenderer component = gameObject.GetComponent<SpriteRenderer>();
						component.size = new Vector2(cellSize * 9f / 7f, cellSize * 9f / 7f);
					}
				}
			}
			GameObject[,] array2 = shapeGridWithoutShadow;
			int length3 = array2.GetLength(0);
			int length4 = array2.GetLength(1);
			for (int k = 0; k < length3; k++)
			{
				for (int l = 0; l < length4; l++)
				{
					GameObject gameObject2 = array2[k, l];
					if (gameObject2 != null)
					{
						SpriteRenderer component2 = gameObject2.GetComponent<SpriteRenderer>();
						component2.size = new Vector2(cellSize, cellSize);
					}
				}
			}
		}

		private void SetScaleDone()
		{
			_scaleDone = true;
		}

		private void SetMoveDone()
		{
			_moveDone = true;
			SetShapeOriginPos();
		}

		private void SetScaleBackDone()
		{
			_scaleDone = false;
		}

		private void SetMoveBackDone()
		{
			_moveDone = false;
			isSelectionDone = false;
		}

		private void SetShapeOriginPos()
		{
			if (_moveDone)
			{
				_shapeSelectedOriginPos = base.transform.localPosition;
				isSelectionDone = true;
			}
		}

		private bool IsSelectActionDone()
		{
			return _scaleDone && _moveDone;
		}

		private void OnMouseDrag()
		{
			if (GameLogic.Instance.State != GameLogic.GameState.Run)
			{
				return;
			}
			GameObject[,] array = shapeGrid;
			int length = array.GetLength(0);
			int length2 = array.GetLength(1);
			for (int i = 0; i < length; i++)
			{
				for (int j = 0; j < length2; j++)
				{
					GameObject gameObject = array[i, j];
					if (gameObject != null)
					{
						gameObject.GetComponent<SpriteRenderer>().sprite = spriteColorBlock[color];
					}
				}
			}
			Board.Instance.SetBlocksOriColor();
			if (isSelectionDone)
			{
				Vector3 a = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
				Vector3 a2 = a - _touchStartPos;
				base.transform.localPosition = _shapeSelectedOriginPos + a2 / ScreenManager.ResolutionAdaptionRatio;
			}
			if (IfShapeCanFill())
			{
				ShowSettlePreview();
			}
		}

		public void ShowSettlePreview()
		{
			List<int> list = new List<int>();
			List<int> list2 = new List<int>();
			Board.Instance.HideBlockPreview();
			Board.Instance.filledBlocks.Clear();
			for (int i = 0; i < rowSize; i++)
			{
				for (int j = 0; j < colSize; j++)
				{
					if (!(shapeGrid[i, j] != null))
					{
						continue;
					}
					int row = Board.Instance.GetRow(shapeGrid[i, j].transform.position);
					int col = Board.Instance.GetCol(shapeGrid[i, j].transform.position);
					Board.Instance.ShowBlockPreview(row, col, color);
					if (Board.Instance.IfRowMatch(row))
					{
						for (int k = 0; k < colSize; k++)
						{
							if (shapeGrid[i, k] != null)
							{
								shapeGrid[i, k].GetComponent<SpriteRenderer>().sprite = spriteColorBlock[color];
							}
						}
						Board.Instance.SwitchRowColor(row, color);
					}
					if (!Board.Instance.IfColMatch(col))
					{
						continue;
					}
					for (int l = 0; l < rowSize; l++)
					{
						if (shapeGrid[l, j] != null)
						{
							shapeGrid[l, j].GetComponent<SpriteRenderer>().sprite = spriteColorBlock[color];
						}
					}
					Board.Instance.SwitchColColor(col, color);
				}
			}
		}

		public bool IfShapeCanFill()
		{
			bool result = true;
			for (int i = 0; i < rowSize; i++)
			{
				for (int j = 0; j < colSize; j++)
				{
					if (shapeGrid[i, j] != null)
					{
						int row = Board.Instance.GetRow(shapeGrid[i, j].transform.position);
						int col = Board.Instance.GetCol(shapeGrid[i, j].transform.position);
						if (!Board.Instance.CanFillCell(row, col))
						{
							Board.Instance.HideBlockPreview();
							result = false;
							break;
						}
					}
				}
			}
			return result;
		}

		public void PutDownShape()
		{
			collider.enabled = false;
			Board.Instance.HideBlockPreview();
			GameLogic.Instance.ShapePos = base.transform.position;
			for (int i = 0; i < rowSize; i++)
			{
				for (int j = 0; j < colSize; j++)
				{
					if (shapeGrid[i, j] != null)
					{
						shapeGrid[i, j].SetActive(value: false);
						shapeGridWithoutShadow[i, j].SetActive(value: true);
					}
				}
			}
			AudioManager.Instance.PlayAudioEffect("put_down");
			for (int k = 0; k < rowSize; k++)
			{
				for (int l = 0; l < colSize; l++)
				{
					if (shapeGrid[k, l] != null)
					{
						PlayBlockAnime(k, l);
					}
				}
			}
		}

		private void PlayBlockAnime(int row, int col)
		{
			int putRow = Board.Instance.GetRow(shapeGrid[row, col].transform.position);
			int putCol = Board.Instance.GetCol(shapeGrid[row, col].transform.position);
			Vector3 cellPos = Board.Instance.GetCellPos(putRow, putCol);
			GameObject gameObject = shapeGridWithoutShadow[row, col];
			fillingBlockCount++;
			Vector3 position = gameObject.transform.position;
			float x = position.x;
			Vector3 position2 = gameObject.transform.position;
			Vector2 a = new Vector2(x, position2.y);
			float num = Vector2.Distance(b: new Vector2(cellPos.x, cellPos.y), a: a);
			float num2 = 4f;
			float duration = num / num2;
			gameObject.transform.DOMove(cellPos, duration).SetEase(Ease.Linear);
			float num3 = Board.Instance.cellSize;
			Vector2 size = gameObject.GetComponent<SpriteRenderer>().size;
			float x2 = size.x;
			Vector3 localScale = base.transform.localScale;
			float x3 = localScale.x;
			float num4 = num3 / ScreenManager.ResolutionAdaptionRatio / (x3 * x2);
			gameObject.transform.DOScale(new Vector3(num4, num4, 1f), duration).OnComplete(delegate
			{
				BlockSettle(putRow, putCol);
			}).SetEase(Ease.Linear);
			Board.Instance.FillCell(putRow, putCol);
			Board.Instance.BoardInfo[putRow, putCol].Block.SetActive(value: false);
			GameLogic.Instance.IncreasedScore++;
		}

		private void BlockSettle(int row, int col)
		{
			fillingBlockCount--;
			Board.Instance.BoardInfo[row, col].Block.SetActive(value: true);
			if (fillingBlockCount == 0)
			{
				GameLogic.Instance.ProcessBlockSettle(base.gameObject);
			}
		}

		private void BlockSettle(Dictionary<int, int> rowAndCol)
		{
			fillingBlockCount--;
			foreach (KeyValuePair<int, int> item in rowAndCol)
			{
				Board.Instance.BoardInfo[item.Key, item.Value].Block.SetActive(value: true);
			}
			if (fillingBlockCount == 0)
			{
				GameLogic.Instance.ProcessBlockSettle(base.gameObject);
			}
		}

		public void UpdateShapeFit(bool ifCanFill)
		{
			Color color = new Color(1f, 1f, 1f, 1f);
			Color color2 = new Color(0.7f, 0.7f, 0.7f, 0.6f);
			GameObject[,] array = shapeGrid;
			int length = array.GetLength(0);
			int length2 = array.GetLength(1);
			for (int i = 0; i < length; i++)
			{
				for (int j = 0; j < length2; j++)
				{
					GameObject gameObject = array[i, j];
					if (gameObject != null)
					{
						gameObject.GetComponent<SpriteRenderer>().color = ((!ifCanFill) ? color2 : color);
					}
				}
			}
		}
	}
}
