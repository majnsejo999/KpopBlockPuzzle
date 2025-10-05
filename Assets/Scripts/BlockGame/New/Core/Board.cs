using DG.Tweening;
using GooglePlayGames;
using BlockGame.GameEngine.Libs.Log;
using BlockGame.New.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Board : MonoBehaviour
{
    public Sprite smallBoard;

    public Sprite largeBoard;

    public SpriteRenderer spriteRenderer;

    private Vector3 previewScale = new Vector3(0.9f, 0.9f, 1f);

    private const float BOARD_SIZE = 6.84f;

    public int rowSize;

    public int colSize;

    public int obstacleFrenquency;

    public float boardWidth;

    public float boardHeight;

    public float cellSize;

    private float boardXOffset = 0.3f;

    private float cellGap;

    public int grayCell;

    private Cell[,] board;

    public List<KeyValuePair<int, int>> lastObstacle = new List<KeyValuePair<int, int>>();

    public List<GameObject> lastShapes = new List<GameObject>();

    public List<Enums.CellState[,]> lastMoves = new List<Enums.CellState[,]>();

    public List<int> lastScores = new List<int>();

    public bool DisplayingPreview;

    public List<Cell> previewBlocks = new List<Cell>();

    public List<Cell> filledBlocks = new List<Cell>();

    public List<int> matchedRows = new List<int>();

    public List<int> matchedCols = new List<int>();

    public List<Cell> removingBlocks = new List<Cell>();

    private int RemovedLock;

    private int RemovedBomb;

    private static Board instance;

    public Cell[,] BoardInfo => board;

    public static Board Instance => instance;

    public void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Instance.gameObject.transform.position = Instance.gameObject.transform.position * ScreenManager.ResolutionAdaptionRatio;
    }

    public void CreateBoard(StageConfig conf)
    {
        spriteRenderer.sprite = largeBoard;
        rowSize = conf.rowSize;
        colSize = conf.colSize;
        obstacleFrenquency = conf.obstacleFrenquency;
        Vector2 a = new Vector2(6.84f, 6.84f);
        GetComponent<SpriteRenderer>().size = a * ScreenManager.ResolutionAdaptionRatio;
        Vector2 size = GetComponent<SpriteRenderer>().size;
        boardWidth = size.x;
        Vector2 size2 = GetComponent<SpriteRenderer>().size;
        boardHeight = size2.y;
        cellSize = (boardWidth - boardXOffset) / (float)rowSize * 0.99f;
        board = new Cell[rowSize, colSize];
        for (int i = 0; i < rowSize; i++)
        {
            for (int j = 0; j < colSize; j++)
            {
                board[i, j] = new Cell(i, j, cellSize, base.gameObject);
                float x = (float)j * (cellSize + 0.01f) - (boardWidth - boardXOffset - cellSize - 0.01f) / 2f + cellGap - 0.01f;
                float y = (float)i * (cellSize + 0.01f) - (boardWidth - boardXOffset - cellSize - 0.01f) / 2f - 0.01f;
                board[i, j].Bg.transform.localPosition = new Vector3(x, y);
            }
        }
        Vector2 b = new Vector2(7f, 7f);
        GetComponent<SpriteRenderer>().size = b * ScreenManager.ResolutionAdaptionRatio;
    }

    public int GetRow(Vector3 pos)
    {
        float num = (boardHeight - boardXOffset) / 2f + pos.y;
        Vector3 position = base.transform.position;
        return Mathf.FloorToInt((num - position.y) / cellSize);
    }

    public int GetCol(Vector3 pos)
    {
        return Mathf.FloorToInt(((boardWidth - boardXOffset) / 2f + pos.x) / cellSize);
    }

    public float GetX(int col)
    {
        return ScreenManager.UnitXOffset + cellSize * ((float)col + 0.5f) - ScreenManager.UnitScreenWidth / 2f;
    }

    public float GetY(int row)
    {
        return cellSize * ((float)row + 0.5f) - 3.6f + boardXOffset;
    }

    public Vector3 GetCellPos(int row, int col)
    {
        return board[row, col].Bg.transform.position;
    }

    public bool CanFillCell(int putRow, int putCol)
    {
        if (putRow >= 0 && putRow < rowSize && putCol >= 0 && putCol < colSize && (board[putRow, putCol].State == Enums.CellState.Default || board[putRow, putCol].State == Enums.CellState.Preview))
        {
            return true;
        }
        return false;
    }

    internal void CreateTutorial(int index)
    {     
        Dictionary<string, int>[] array = TutorialManager.TutorialInfo[index];
        foreach (Dictionary<string, int> dictionary in array)
        {
            int num = dictionary["value"];
            if (num != -1)
            {
                int num2 = dictionary["row"];
                int num3 = dictionary["col"];
                board[num2, num3].SetCellFilled();
            }
        }
    }

    public void ShowBlockPreview(int row, int col, int color)
    {
        DisplayingPreview = true;
        board[row, col].Block.transform.localScale = previewScale;
        board[row, col].SetCellBlockColor(color);
        board[row, col].SetCellPreview(display: true);
        previewBlocks.Add(board[row, col]);
        filledBlocks.Add(board[row, col]);
    }

    public bool IfRowMatch(int row)
    {
        bool result = true;
        for (int i = 0; i < colSize; i++)
        {
            Cell cell = board[row, i];
            if (cell.State == Enums.CellState.Default || cell.State == Enums.CellState.Rock)
            {
                result = false;
                break;
            }
        }
        return result;
    }

    public bool IfColMatch(int col)
    {
        bool result = true;
        for (int i = 0; i < rowSize; i++)
        {
            Cell cell = board[i, col];
            if (cell.State == Enums.CellState.Default || cell.State == Enums.CellState.Rock)
            {
                result = false;
                break;
            }
        }
        return result;
    }

    public void HideBlockPreview()
    {
        for (int i = 0; i < previewBlocks.Count; i++)
        {
            previewBlocks[i].SetCellPreview(display: false);
            previewBlocks[i].SetCellBlockColor(previewBlocks[i].Color);
        }
        previewBlocks.Clear();
        DisplayingPreview = false;
    }

    public void FillCell(int row, int col)
    {
        Cell cell = board[row, col];
        cell.SetCellFilled();
    }

    public void RemoveMatchedBlocks()
    {
        if (GamePlayTutorial.Instance != null)
        {
            if (UserDataManager.Instance.GetService().TutorialProgress < 3)
            {
                GamePlayTutorial.Instance.Hide();
            }
            else
            {
                Destroy(GamePlayTutorial.Instance.gameObject);
            }
        }
        int num = 0;
        for (int i = 0; i < rowSize; i++)
        {
            bool flag = false;
            for (int j = 0; j < colSize; j++)
            {
                Cell cell = board[i, j];
                if (cell.State == Enums.CellState.Default || cell.State == Enums.CellState.Rock)
                {
                    flag = true;
                    break;
                }
            }
            if (flag)
            {
                continue;
            }
            num++;
            for (int k = 0; k < colSize; k++)
            {
                Cell item = board[i, k];
                if (!removingBlocks.Contains(item))
                {
                    removingBlocks.Add(item);
                }
            }
            matchedRows.Add(i);
        }
        for (int l = 0; l < colSize; l++)
        {
            bool flag2 = false;
            for (int m = 0; m < rowSize; m++)
            {
                Cell cell2 = board[m, l];
                if (cell2.State == Enums.CellState.Default || cell2.State == Enums.CellState.Rock)
                {
                    flag2 = true;
                    break;
                }
            }
            if (flag2)
            {
                continue;
            }
            num++;
            for (int n = 0; n < rowSize; n++)
            {
                Cell item2 = board[n, l];
                if (!removingBlocks.Contains(item2))
                {
                    removingBlocks.Add(item2);
                }
            }
            matchedCols.Add(l);
        }
        if (matchedCols.Count == 0 && matchedRows.Count == 0)
        {
            GameLogic.Instance.AddScore(GameLogic.Instance.IncreasedScore);
            GameLogic.Instance.IncreasedScore = 0;
            GameLogic.Instance.UpdateMoves();
            return;
        }
        if (matchedCols.Count + matchedRows.Count >= 2)
        {
            TopCanvasManager.Instance.ShowCheer(matchedCols.Count + matchedRows.Count);
            Camera.main.transform.position = new Vector3(0f, 0f, -10f);
            Camera.main.transform.DOPunchPosition(new Vector3(0f, 0.12f, 0f), 0.1f, 80).SetDelay(0.06f).SetEase(Ease.InSine);
        }
        List<int> list = new List<int>();
        List<int> list2 = new List<int>();
        if (matchedRows.Count > 0 || matchedCols.Count > 0)
        {
            foreach (Cell filledBlock in filledBlocks)
            {
                if (matchedCols.Contains(filledBlock.col) || matchedRows.Contains(filledBlock.row))
                {
                    removingBlocks.Add(filledBlock);
                    if (!list.Contains(filledBlock.row))
                    {
                        list.Add(filledBlock.row);
                    }
                    if (!list2.Contains(filledBlock.col))
                    {
                        list2.Add(filledBlock.col);
                    }
                }
            }
            list.Sort();
            list2.Sort();
        }
        GameObject gameObject = null;
        if (matchedRows.Count > 0 && gameObject == null)
        {
            gameObject = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Prefabs/Game/MatchScore"));
            gameObject.transform.SetParent(TopCanvasManager.Instance.canvas.transform, worldPositionStays: false);
            gameObject.transform.position = board[matchedRows[0], list2[list2.Count - 1]].Bg.transform.position;
            GameLogic.Instance.currentMatchColor = board[matchedRows[0], list2[list2.Count - 1]].PreviewColor;
        }
        if (matchedCols.Count > 0 && gameObject == null)
        {
            gameObject = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Prefabs/Game/MatchScore"));
            gameObject.transform.SetParent(TopCanvasManager.Instance.canvas.transform, worldPositionStays: false);
            gameObject.transform.position = board[list[list.Count - 1], matchedCols[0]].Bg.transform.position;
            GameLogic.Instance.currentMatchColor = board[list[list.Count - 1], matchedCols[0]].PreviewColor;
        }
        Cell[] array = removingBlocks.ToArray();
        foreach (Cell cell3 in array)
        {
            RemoveMatchBlock(cell3);
        }
        if (matchedRows.Count > 0 || matchedCols.Count > 0)
        {
            AudioManager.Instance.PlayAudioEffect("match_explosion_1");
        }
        UserDataManager.Instance.GetService().TotoalMatchedBlocks += matchedRows.Count + matchedCols.Count;
       // List<KeyValuePair<string, int>> list3 = SocialPlatformAchievementConfig.MatchRows.ToList();
        //if (Social.localUser.authenticated)
        //{
        //    for (int num3 = 0; num3 < list3.Count; num3++)
        //    {
        //        PlayGamesPlatform.Instance.IncrementAchievement(list3[num3].Key, 1, delegate
        //        {
        //        });
        //        if (UserDataManager.Instance.GetService().TotoalMatchedBlocks == list3[num3].Value && num3 < list3.Count - 1)
        //        {
        //            Social.ReportProgress(list3[num3 + 1].Key, 0.0, delegate
        //            {
        //            });
        //        }
        //    }
        //}
        matchedRows.Clear();
        matchedCols.Clear();
        filledBlocks.Clear();
        int num4 = 10 * num / 2 * (1 + num);
        GameLogic.Instance.IncreasedScore += num4;
        if (gameObject != null)
        {
            gameObject.GetComponent<MatchScore>().SetScore(num4);
        }
        GameLogic.Instance.AddScore(GameLogic.Instance.IncreasedScore);
        GameLogic.Instance.IncreasedScore = 0;
    }

    internal void PlayRestartAnime()
    {
        for (int i = 0; i < rowSize; i++)
        {
            for (int j = 0; j < colSize; j++)
            {
                PlayBlockMatchAnime(ref board[i, j]);
            }
        }
    }

    public void SwitchColColor(int col, int color)
    {
        for (int i = 0; i < rowSize; i++)
        {
            board[i, col].SetCellPreviewColor(color);
        }
    }

    public void SwitchRowColor(int row, int color)
    {
        for (int i = 0; i < colSize; i++)
        {
            board[row, i].SetCellPreviewColor(color);
        }
    }

    public void SetBlocksOriColor()
    {
        for (int i = 0; i < rowSize; i++)
        {
            for (int j = 0; j < colSize; j++)
            {
                board[i, j].SetOriColor();
            }
        }
    }

    public void PlayGameOverAnime()
    {
        AudioManager.Instance.StopAllBackgroundMusic();
        List<Cell> occupiedCells = GetOccupiedCells();
        System.Random random = new System.Random();
        int num = occupiedCells.Count;
        float time = 1f / (float)num;
        while (num > 1)
        {
            num--;
            int index = random.Next(num + 1);
            Cell value = occupiedCells[index];
            occupiedCells[index] = occupiedCells[num];
            occupiedCells[num] = value;
        }
        for (int i = 0; i < occupiedCells.Count; i++)
        {
            grayCell++;
            occupiedCells[i].SetCellGray(i, time);
        }
        AudioManager.Instance.PlayAudioEffect("game_over");
    }

    public void CheckGrayCellCount()
    {
        grayCell--;
        if (grayCell == 0)
        {
            TopCanvasManager.Instance.ToggleTouchMask(false);
          //  if ((UserDataManager.Instance.GetService().RemoveVideoPurchased && !GlobalVariables.GameOverRewardVideoDisplayed) || (!GlobalVariables.GameOverRewardVideoDisplayed && AdsControl.instance.CheckIsReadlyAds()))
            if ((UserDataManager.Instance.GetService().RemoveVideoPurchased && !GlobalVariables.GameOverRewardVideoDisplayed) || (!GlobalVariables.GameOverRewardVideoDisplayed))
            {
                DialogManager.Instance.ShowDialog("GameOverRewardVideoDlg");
            }
            else
            {
               // AdsControl.instance.ShowAdsInter(1,"game_over");
                GameLogic.Instance.ProcessGameOver();
            }
        }
    }

    public void RemoveMatchBlock(Cell cell)
    {
        PlayBlockMatchAnime(ref cell);
        if (!removingBlocks.Contains(cell))
        {
            return;
        }
        removingBlocks.Remove(cell);
        if (removingBlocks.Count == 0)
        {
            if (UserDataManager.Instance.GetService().TutorialProgress < 3)
            {
                Timer.Schedule(this, 0.4f, delegate
                {
                    UserDataManager.Instance.GetService().HighScore = 0;
                    bool showTutorial = (UserDataManager.Instance.GetService().TutorialProgress <= 2) ? true : false;
                    GameLogic.Instance.IncreasedScore = 0;
                    GameLogic.Instance.RetryLevel(showTutorial);
                    UserDataManager.Instance.GetService().TutorialProgress++;
                });
            }
            else
            {
                GameLogic.Instance.UpdateMoves();
            }
        }
    }

    private List<Cell> GetAdjactiveCells(int row, int col)
    {
        List<Cell> list = new List<Cell>();
        if (isRowColValid(row - 1, col))
        {
            list.Add(board[row - 1, col]);
        }
        if (isRowColValid(row, col - 1))
        {
            list.Add(board[row, col - 1]);
        }
        if (isRowColValid(row + 1, col))
        {
            list.Add(board[row + 1, col]);
        }
        if (isRowColValid(row, col + 1))
        {
            list.Add(board[row, col + 1]);
        }
        return list;
    }

    private bool isRowColValid(int row, int col)
    {
        if (row >= 0 && row < rowSize && col >= 0 && col < colSize)
        {
            return true;
        }
        return false;
    }

    public bool CanRemainShapesFillBoard()
    {
        bool result = false;
        for (int i = 0; i < 3; i++)
        {
            Shape shape = ShapeController.Instance.slots[i];
            if (!(ShapeController.Instance.slots[i] == null))
            {
                if (CheckShapeCanFill(shape))
                {
                    result = true;
                    shape.UpdateShapeFit(ifCanFill: true);
                }
                else
                {
                    shape.UpdateShapeFit(ifCanFill: false);
                }
            }
        }
        return result;
    }

    private bool CheckShapeCanFill(Shape shape)
    {
        int num = shape.rowSize;
        int num2 = shape.colSize;
        for (int i = 0; i < rowSize; i++)
        {
            for (int j = 0; j < colSize; j++)
            {
                bool flag = false;
                for (int k = 0; k < num; k++)
                {
                    if (flag)
                    {
                        break;
                    }
                    for (int l = 0; l < num2; l++)
                    {
                        if (shape.shapeGrid[k, l] != null && (!isRowColValid(i + k, j + l) || board[i + k, j + l].State != 0))
                        {
                            flag = true;
                            break;
                        }
                    }
                }
                if (!flag)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void CreateObstacle(int obstacleType)
    {
        AudioManager.Instance.PlayAudioEffect("special_element");
        switch (obstacleType)
        {
            case 0:
                CreateBlock();
                break;
            case 1:
                CreateLock();
                break;
            case 2:
                CreateBomb();
                break;
        }
    }

    private void CreateBlock()
    {
        List<Cell> unfilledCellsForObstacleBlock = GetUnfilledCellsForObstacleBlock();
        if (unfilledCellsForObstacleBlock.Count != 0)
        {
            int index = UnityEngine.Random.Range(0, unfilledCellsForObstacleBlock.Count);
            unfilledCellsForObstacleBlock[index].SetCellObstacleFilled();
            if (!Instance.CanRemainShapesFillBoard())
            {
                GameLogic.Instance.ProcessLevelFinish();
            }
        }
    }

    private void CreateLock()
    {
        List<Cell> filledCells = GetFilledCells();
        if (filledCells.Count != 0)
        {
            int index = UnityEngine.Random.Range(0, filledCells.Count);
            filledCells[index].SetCellLock();
        }
    }

    private void CreateRock()
    {
        List<Cell> unfilledCells = GetUnfilledCells();
        int index = UnityEngine.Random.Range(0, unfilledCells.Count);
        unfilledCells[index].SetCellRock();
        if (!Instance.CanRemainShapesFillBoard())
        {
            GameLogic.Instance.ProcessLevelFinish();
        }
    }

    private void CreateBomb()
    {
        List<Cell> filledCells = GetFilledCells();
        int index = UnityEngine.Random.Range(0, filledCells.Count);
        filledCells[index].SetCellBomb();
        Bomb component = filledCells[index].Bomb.GetComponent<Bomb>();
        GameLogic.Instance.AddBomb(component);
    }

    private List<Cell> GetUnfilledCellsForObstacleBlock()
    {
        List<Cell> list = new List<Cell>();
        List<Cell> list2 = new List<Cell>();
        for (int i = 0; i < rowSize; i++)
        {
            Cell cell = OneSlotToMatchCol(i);
            if (cell != null && !list2.Contains(cell))
            {
                list2.Add(cell);
            }
        }
        for (int j = 0; j < colSize; j++)
        {
            Cell cell2 = OneSlotToMatchRow(j);
            if (cell2 != null && !list2.Contains(cell2))
            {
                list2.Add(cell2);
            }
        }
        for (int k = 0; k < rowSize; k++)
        {
            for (int l = 0; l < colSize; l++)
            {
                if (board[k, l].State == Enums.CellState.Default && !list2.Contains(board[k, l]))
                {
                    list.Add(board[k, l]);
                }
            }
        }
        return list;
    }

    private int CheckRowEmptySlotNum(int row)
    {
        int num = 0;
        for (int i = 0; i < colSize; i++)
        {
            if (board[row, i].State == Enums.CellState.Default)
            {
                num++;
            }
        }
        return num;
    }

    private Cell OneSlotToMatchRow(int row)
    {
        int num = 0;
        Cell result = null;
        for (int i = 0; i < colSize; i++)
        {
            if (board[row, i].State == Enums.CellState.Default)
            {
                num++;
                result = board[row, i];
            }
        }
        if (num > 1)
        {
            result = null;
        }
        return result;
    }

    private Cell OneSlotToMatchCol(int col)
    {
        int num = 0;
        Cell result = null;
        for (int i = 0; i < rowSize; i++)
        {
            if (board[i, col].State == Enums.CellState.Default)
            {
                num++;
                result = board[i, col];
            }
        }
        if (num > 1)
        {
            result = null;
        }
        return result;
    }

    private List<Cell> GetUnfilledCells()
    {
        List<Cell> list = new List<Cell>();
        for (int i = 0; i < rowSize; i++)
        {
            for (int j = 0; j < colSize; j++)
            {
                if (board[i, j].State == Enums.CellState.Default)
                {
                    list.Add(board[i, j]);
                }
            }
        }
        return list;
    }

    private List<Cell> GetFilledCells()
    {
        List<Cell> list = new List<Cell>();
        for (int i = 0; i < rowSize; i++)
        {
            for (int j = 0; j < colSize; j++)
            {
                if (board[i, j].State == Enums.CellState.Filled)
                {
                    list.Add(board[i, j]);
                }
            }
        }
        return list;
    }

    private List<Cell> GetOccupiedCells()
    {
        List<Cell> list = new List<Cell>();
        for (int i = 0; i < rowSize; i++)
        {
            for (int j = 0; j < colSize; j++)
            {
                if (board[i, j].State != 0)
                {
                    list.Add(board[i, j]);
                }
            }
        }
        return list;
    }

    public void SetCellColor(GameObject block, int color)
    {
        Sprite sprite = Resources.Load<Sprite>(TextureRes.Diamond + color);
        block.GetComponent<SpriteRenderer>().sprite = sprite;
    }

    public void Clear()
    {
        RemovedLock = 0;
        RemovedBomb = 0;
        for (int i = 0; i < rowSize; i++)
        {
            for (int j = 0; j < colSize; j++)
            {
                board[i, j].SetCellDefault();
                board[i, j].SetCellColorNormal();
            }
        }
    }

    public void ClearRandomGridsAndContinue()
    {
        if (GlobalVariables.RowOrCol)
        {
            ClearRandomRows();
        }
        else
        {
            ClearRandomCols();
        }
        Cell[,] array = board;
        int length = array.GetLength(0);
        int length2 = array.GetLength(1);
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < length2; j++)
            {
                Cell cell = array[i, j];
                cell.SetCellColorNormal();
                if (cell.State == Enums.CellState.Bomb && cell.Bomb != null && !cell.Bomb.gameObject.activeSelf)
                {
                    cell.SetCellDefault();
                }
            }
        }
        ShapeController.Instance.Refresh(ensureFill: true);
        GameLogic.Instance.ShapeRemain = 3;
        AudioManager.Instance.PlayAudioMusic("bgm_01");
        AudioManager.Instance.PlayAudioEffect("match_explosion_1");
        CanRemainShapesFillBoard();
    }

    public void ClearRandomRows()
    {
        int num = 4;
        List<int> list = new List<int>();
        List<int> list2 = new List<int>();
        for (int i = 0; i < rowSize; i++)
        {
            for (int j = 0; j < colSize; j++)
            {
                if (board[i, j].State != 0)
                {
                    list.Add(i);
                    break;
                }
            }
        }
        for (int k = 0; k < num; k++)
        {
            int index = UnityEngine.Random.Range(0, list.Count);
            list2.Add(list[index]);
            list.RemoveAt(index);
        }
        foreach (int item in list2)
        {
            for (int l = 0; l < colSize; l++)
            {
                PlayBlockMatchAnime(ref board[item, l]);
            }
        }
    }

    public void ClearRandomCols()
    {
        int num = 4;
        List<int> list = new List<int>();
        List<int> list2 = new List<int>();
        for (int i = 0; i < colSize; i++)
        {
            for (int j = 0; j < rowSize; j++)
            {
                if (board[j, i].State != 0)
                {
                    list.Add(i);
                    break;
                }
            }
        }
        for (int k = 0; k < num; k++)
        {
            int index = UnityEngine.Random.Range(0, list.Count);
            list2.Add(list[index]);
            list.RemoveAt(index);
        }
        foreach (int item in list2)
        {
            for (int l = 0; l < rowSize; l++)
            {
                PlayBlockMatchAnime(ref board[l, item]);
            }
        }
    }

    private void PlayBlockMatchAnime(ref Cell cell)
    {
        if (cell.State == Enums.CellState.Filled)
        {
            cell.SetCellDefault();
            BlockMatch blockMatch = PoolMananger.Instance.SpawnBlockMatch(0);
            blockMatch.gameObject.transform.position = cell.Bg.transform.position;
            blockMatch.Play();
        }
        else if (cell.State == Enums.CellState.Locked)
        {
            cell.SetCellFilled();
            cell.SetOriColor();
            GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Prefabs/Particles/ObstacleDestroy"));
            gameObject.GetComponent<ParticleSystemRenderer>().material.mainTexture = Resources.Load<Texture>("Textures/Effects/suolian");
            gameObject.transform.position = cell.Bg.transform.position;
            UnityEngine.Object.Destroy(gameObject, 1f);
            RemovedLock++;
            //if (Social.localUser.authenticated && RemovedLock >= 5)
            //{
            //    Social.ReportProgress("CgkIo_yO-PwdEAIQHA", 100.0, delegate (bool success)
            //    {
            //        if (success)
            //        {
            //            UnityEngine.Debug.Log("Achievement Chain Breaker Finished!");
            //        }
            //    });
            //}
            UserDataManager.Instance.GetService().TotalRemovedobstacles++;
            //List<KeyValuePair<string, int>> list = SocialPlatformAchievementConfig.RemoveObstacles.ToList();
            //if (!Social.localUser.authenticated)
            //{
            //    return;
            //}
            //for (int i = 0; i < list.Count; i++)
            //{
            //    PlayGamesPlatform.Instance.IncrementAchievement(list[i].Key, 1, delegate
            //    {
            //    });
            //    if (UserDataManager.Instance.GetService().TotalRemovedobstacles == list[i].Value && i < list.Count - 1)
            //    {
            //        Social.ReportProgress(list[i + 1].Key, 0.0, delegate
            //        {
            //        });
            //    }
            //}
        }
        else
        {
            if (cell.State == Enums.CellState.Rock || cell.State != Enums.CellState.Bomb)
            {
                return;
            }
            cell.SetCellDefault();
            BlockMatch blockMatch2 = PoolMananger.Instance.SpawnBlockMatch(1);
            blockMatch2.gameObject.transform.position = cell.Bg.transform.position;
            blockMatch2.Play();
            //if (Social.localUser.authenticated && RemovedBomb >= 3)
            //{
            //    Social.ReportProgress("CgkIo_yO-PwdEAIQHQ", 100.0, delegate (bool success)
            //    {
            //        if (success)
            //        {
            //            UnityEngine.Debug.Log("Achievement Bomb Expert Finished!");
            //        }
            //    });
            //}
            UserDataManager.Instance.GetService().TotalRemovedobstacles++;
            //List<KeyValuePair<string, int>> list2 = SocialPlatformAchievementConfig.RemoveObstacles.ToList();
            //if (!Social.localUser.authenticated)
            //{
            //    return;
            //}
            //for (int j = 0; j < list2.Count; j++)
            //{
            //    PlayGamesPlatform.Instance.IncrementAchievement(list2[j].Key, 1, delegate
            //    {
            //    });
            //    if (UserDataManager.Instance.GetService().TotalRemovedobstacles == list2[j].Value && j < list2.Count - 1)
            //    {
            //        Social.ReportProgress(list2[j + 1].Key, 0.0, delegate
            //        {
            //        });
            //    }
            //}
        }
    }
}

