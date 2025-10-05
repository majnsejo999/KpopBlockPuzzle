using GooglePlayGames;
using BlockGame.GameEngine.Libs.Log;
using BlockGame.New.Core;
using BlockGame.New.Core.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public enum GameState
    {
        Initialize,
        Run,
        ShowBuildings,
        Pause,
        Over,
        Tutorial
    }

    public const int BOMB_MOVE = 9;

    public Dictionary<int, int> difficultyObstacle = new Dictionary<int, int>
        {
            {
                1,
                0
            },
            {
                2,
                300
            },
            {
                3,
                500
            }
        };

    public Dictionary<int, Dictionary<int, int>> ObstacleConfig = new Dictionary<int, Dictionary<int, int>>
        {
            {
                1,
                new Dictionary<int, int>
                {
                    {
                        1,
                        100
                    },
                    {
                        2,
                        100
                    },
                    {
                        3,
                        100
                    }
                }
            },
            {
                2,
                new Dictionary<int, int>
                {
                    {
                        1,
                        0
                    },
                    {
                        2,
                        100
                    },
                    {
                        3,
                        100
                    }
                }
            },
            {
                3,
                new Dictionary<int, int>
                {
                    {
                        1,
                        0
                    },
                    {
                        2,
                        0
                    },
                    {
                        3,
                        100
                    }
                }
            }
        };

    private int frequency;

    private GameState state;

    private int difficultyLevel = 1;

    private int group = 1;

    private int score;

    private int move;

    private int turn;

    private int obstacleMove;

    private int shapeRemain;

    private int undoNum = 3;

    private int nextObstacleIndex;

    public int IncreasedScore;

    public GameObject lastPutDownShape;

    public Vector3 ShapePos;

    public int currentMatchColor;

    public Dictionary<Bomb, int> Bombs;

    public Dictionary<int, int> FilledBlocks = new Dictionary<int, int>();

    private static GameLogic instance;

    public GameState State
    {
        get
        {
            return state;
        }
        set
        {
            state = value;
        }
    }

    public int DifficultyLevel => difficultyLevel;

    public int Group => group;

    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
        }
    }

    public int Move => move;

    public int Turn => turn;

    public int ShapeRemain
    {
        get
        {
            return shapeRemain;
        }
        set
        {
            shapeRemain = value;
        }
    }

    public int UndoNum
    {
        get
        {
            return undoNum;
        }
        set
        {
            undoNum = value;
        }
    }

    public int ObstacleMove => obstacleMove;

    public int NextObstacleIndex => nextObstacleIndex;

    public static GameLogic Instance => instance;

    private void Awake()
    {
        instance = this;
        frequency = StageManager.Instance.StageList[UserDataManager.Instance.GetService().Stage].obstacleFrenquency;
    }

    public void StartGame(bool showTutorial = true, float delayTime = 0f)
    {
        GlobalVariables.BrokenNewRecordInGame = false;
        GameSceneUIManager.Instance.ResetNewRecordEffect();
        if (GlobalVariables.GameType == 0)
        {
            GameSceneUIManager.Instance.ObstacleLock.SetActive(value: true);
            GameSceneUIManager.Instance.IconObstacle.gameObject.SetActive(value: false);
            GameSceneUIManager.Instance.NextObstacle.gameObject.SetActive(value: false);
            Board.Instance.CreateBoard(StageManager.Instance.StageList[1]);
        }
        else if (GlobalVariables.GameType == 1)
        {
            GameSceneUIManager.Instance.ObstacleLock.SetActive(value: false);
            GameSceneUIManager.Instance.IconObstacle.gameObject.SetActive(value: true);
            GameSceneUIManager.Instance.NextObstacle.gameObject.SetActive(value: true);
            Board.Instance.CreateBoard(StageManager.Instance.StageList[2]);
        }
        if (TestConfig.Instance.enabled)
        {
            UserDataManager.Instance.GetService().DiscardNum = TestConfig.Instance.TestDiscardNum;
            UserDataManager.Instance.GetService().UnDoNum = TestConfig.Instance.TestUndoNum;
        }
        score = 0;
        shapeRemain = ShapeInfoManager.SHAPE_SELECTOR_SLOT;
        move = 0;
        difficultyLevel = 1;
        undoNum = 3;
        if (delayTime > 0f)
        {
            Timer.Schedule(this, delayTime, delegate
            {
                ShapeController.Instance.CreateShapeSelector();
            });
        }
        else
        {
            ShapeController.Instance.CreateShapeSelector();
        }
        Bombs = new Dictionary<Bomb, int>();
        UpdateGameUI();
        state = GameState.Run;
        if (GlobalVariables.GameType == 0)
        {
            if (showTutorial)
            {
                CheckTutorial();
            }
        }
        else
        {
            obstacleMove = frequency;
            GameSceneUIManager.Instance.NextObstacle.text = obstacleMove.ToString();
            nextObstacleIndex = GetNextObstacleIndex();
            GameSceneUIManager.Instance.UpdatePreviewItem(nextObstacleIndex);
            Instance.State = GameState.Pause;
            if (UserDataManager.Instance.GetService().TutorialProgress < 5)
            {
                UserDataManager.Instance.GetService().TutorialProgress = 5;
                Timer.Schedule(this, 1.1f, delegate
                {
                    GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Prefabs/UI/ObstacleTutorial"));
                });
            }
            else
            {
                Instance.State = GameState.Run;
            }
        }
        GlobalVariables.RestartGame = false;
    }

    private void CreateFromSavedProgress()
    {
        score = UserDataManager.Instance.GetService().SavedScore;
    }

    private void CreateNew()
    {
    }

    private void CheckTutorial()
    {
        int tutorialProgress = UserDataManager.Instance.GetService().TutorialProgress;
        if (tutorialProgress < 3)
        {
            LoadGamePlayTutorial(tutorialProgress);
        }
    }

    private void LoadGamePlayTutorial(int progress)
    {
        Board.Instance.CreateTutorial(progress);
        if (GamePlayTutorial.Instance == null)
        {
            Timer.Schedule(this, 0.02f, delegate
            {
                GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Prefabs/UI/GamePlayTutorial"));
                GamePlayTutorial.Instance.Show();
            });
        }
        else
        {
            GamePlayTutorial.Instance.Show();
        }
    }

    private void UpdateNextObstacle()
    {
        obstacleMove = frequency;
        nextObstacleIndex = GetNextObstacleIndex();
        GameSceneUIManager.Instance.UpdatePreviewItem(nextObstacleIndex);
    }

    public void CheckObstacleTutorial(int obstacleIndex)
    {
        if (state != GameState.Run)
        {
            return;
        }
        int tutorialProgress = UserDataManager.Instance.GetService().TutorialProgress;
        if ((obstacleIndex == 0 && tutorialProgress == 5) || (obstacleIndex == 1 && tutorialProgress == 6) || (obstacleIndex == 2 && tutorialProgress == 7))
        {
            if (ObstacleTutorial.Instance == null)
            {
                GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Prefabs/UI/ObstacleTutorial"));
                ObstacleTutorial.Instance.GeneralInfo.SetActive(value: false);
            }
            else
            {
                ObstacleTutorial.Instance.gameObject.SetActive(value: true);
            }
            ObstacleTutorial.Instance.Show(tutorialProgress);
        }
    }

    private int GetNextObstacleIndex()
    {
        int obstacleDifficultyLevel = GetObstacleDifficultyLevel();
        if (obstacleDifficultyLevel <= 0)
        {
            return 0;
        }
        int num = 0;
        List<int> list = new List<int>();
        int num2 = 1;
        foreach (KeyValuePair<int, Dictionary<int, int>> item in ObstacleConfig)
        {
            int num3 = item.Value[obstacleDifficultyLevel];
            if (num3 == 0)
            {
                break;
            }
            num += num3;
            list.Add(num);
            num2++;
        }
        int result = 0;
        int num4 = UnityEngine.Random.Range(0, num + 1);
        for (int i = 0; i < list.Count; i++)
        {
            int num5 = (i > 0) ? list[i - 1] : 0;
            int num6 = list[i];
            if (num4 >= num5 && num4 < num6)
            {
                result = i;
                break;
            }
        }
        return result;
    }

    public void UpdateMoves()
    {
        move++;
        shapeRemain--;
        if (shapeRemain == 0)
        {
            shapeRemain = ShapeInfoManager.SHAPE_SELECTOR_SLOT;
            ShapeController.Instance.AddShapes();
        }
        if (GamePlayTutorial.Instance != null)
        {
            GamePlayTutorial.Instance.gameObject.SetActive(value: false);
        }
        if (!Board.Instance.CanRemainShapesFillBoard())
        {
            ProcessLevelFinish();
        }
        else if ((Bombs.Count <= 0 || UpdateBombs()) && GlobalVariables.GameType == 1)
        {
            Instance.UpdateObstaclePreview();
        }
    }

    public void GenerateObstacle()
    {
        if (obstacleMove == 0 && state != GameState.Over)
        {
            Board.Instance.CreateObstacle(nextObstacleIndex);
            UpdateNextObstacle();
        }
    }

    public void UpdateObstaclePreview()
    {
        obstacleMove--;
        GameObject obstaclePreview = GameSceneUIManager.Instance.ObstaclePreview;
        GameSceneUIManager.Instance.NextObstacle.text = obstacleMove.ToString();
        GenerateObstacle();
    }

    private int GetObstacleDifficultyLevel()
    {
        List<int> list = difficultyObstacle.Values.ToList();
        for (int num = list.Count - 1; num >= 0; num--)
        {
            if (score >= list[num])
            {
                return num + 1;
            }
        }
        return 0;
    }

    public void RetryTutorialLevel()
    {
    }

    public void RetryLevel(bool showTutorial = false, float restartTime = 0.6f)
    {
        Instance.State = GameState.Over;
        bool ifPlayMatchSound = false;
        Cell[,] boardInfo = Board.Instance.BoardInfo;
        int length = boardInfo.GetLength(0);
        int length2 = boardInfo.GetLength(1);
        for (int i = 0; i < length; i++)
        {
            int num = 0;
            while (num < length2)
            {
                Cell cell = boardInfo[i, num];
                if (cell.State == Enums.CellState.Default)
                {
                    num++;
                    continue;
                }
                goto IL_0068;
            }
            continue;
        IL_0068:
            ifPlayMatchSound = true;
            restartTime = 1.2f;
            break;
        }
        // AdsControl.instance.ShowAdsInter(1, "RetryGame");
        Timer.Schedule(this, 0.5f, delegate
        {
            Board.Instance.PlayRestartAnime();
            Board.Instance.Clear();
            if (ifPlayMatchSound)
            {
                AudioManager.Instance.PlayAudioEffect("match_explosion_1");
            }
        });
        Timer.Schedule(this, restartTime, delegate
        {
            ShapeController.Instance.Clear();
            if (!showTutorial)
            {
                AudioManager.Instance.PlayAudioEffect("game_start");
            }
            StartGame(showTutorial);
        });
    }

    public void ProcessLevelFinish()
    {
        if (state != GameState.Over)
        {
            TopCanvasManager.Instance.ToggleTouchMask(isActive: true);
            Timer.Schedule(this, 0.55f, delegate
            {
                Board.Instance.PlayGameOverAnime();
            });
            UserDataManager.Instance.GetService().LastScore = score;
            UserDataManager.Instance.GetService().TimesOfGameOver++;
            if (UserDataManager.Instance.GetService().TimesOfGameOver >= 5 && UserDataManager.Instance.GetService().LastScore >= 50)
            {
                GlobalVariables.IfPopUpRate = true;
            }
        }
        state = GameState.Over;
    }

    public void AddScore(int num)
    {
        difficultyLevel = CalculateDifficultyLevel();
        TweenUtility.AnimateNum(score, num, GameSceneUIManager.Instance.scoreText, 0.4f);
        score += num;
        if (!(GamePlayTutorial.Instance != null))
        {
            if (!GlobalVariables.BrokenNewRecordInGame && GlobalVariables.GameType == 0 && score > UserDataManager.Instance.GetService().HighBasicScore && UserDataManager.Instance.GetService().HighBasicScore > 0)
            {
                GlobalVariables.BrokenNewRecordInGame = true;
                GameSceneUIManager.Instance.ShowInGameHighScoreAnime();
            }
            else if (!GlobalVariables.BrokenNewRecordInGame && GlobalVariables.GameType == 1 && score > UserDataManager.Instance.GetService().HighScore && UserDataManager.Instance.GetService().HighScore > 0)
            {
                GlobalVariables.BrokenNewRecordInGame = true;
                GameSceneUIManager.Instance.ShowInGameHighScoreAnime();
            }
        }
    }

    private int CalculateDifficultyLevel()
    {
        int stage = UserDataManager.Instance.GetService().Stage;
        StageConfig stageConfig = StageManager.Instance.StageList[stage];
        for (int num = stageConfig.scoreBase.Count; num > 0; num--)
        {
            if (score >= stageConfig.scoreBase[num])
            {
                return num + 1;
            }
        }
        return 1;
    }

    public int RandomGroup()
    {
        StageConfig stageConfig = StageManager.Instance.StageList[UserDataManager.Instance.GetService().Stage];
        int num = 0;
        for (int i = 0; i < stageConfig.difficultyLevel[difficultyLevel].Count; i++)
        {
            num += stageConfig.difficultyLevel[difficultyLevel][i + 1];
        }
        int num2 = UnityEngine.Random.Range(0, num);
        int num3 = stageConfig.difficultyLevel[difficultyLevel][1];
        int num4 = num3 + stageConfig.difficultyLevel[difficultyLevel][2];
        int num5 = num4 + stageConfig.difficultyLevel[difficultyLevel][3];
        if (num2 >= 0 && num2 < num3)
        {
            group = 0;
        }
        else if (num2 >= num3 && num2 < num4)
        {
            group = 1;
        }
        else
        {
            group = 2;
        }
        return group;
    }

    public void AddBomb(Bomb bomb)
    {
        Bombs.Add(bomb, 9);
        bomb.SetMoveLeft(9);
    }

    public bool UpdateBombs()
    {
        List<Bomb> list = Bombs.Keys.ToList();
        foreach (Bomb item in list)
        {
            Dictionary<Bomb, int> bombs;
            Bomb key;
            (bombs = Bombs)[key = item] = bombs[key] - 1;
            item.SetMoveLeft(Bombs[item]);
            if (Bombs[item] == 0)
            {
                GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Prefabs/Effects/Explosion"));
                float length = gameObject.GetComponent<Animator>().runtimeAnimatorController.animationClips[0].length;
                AudioManager.Instance.PlayAudioEffect("bomb");
                item.gameObject.SetActive(value: false);
                gameObject.transform.position = item.transform.position;
                state = GameState.Pause;
                UnityEngine.Object.Destroy(gameObject, length);
                Invoke("ProcessLevelFinish", length);
                return false;
            }
        }
        return true;
    }

    public void ProcessBlockSettle(GameObject shape)
    {
        UserDataManager.Instance.GetService().TotalPutDownBlocks++;
        //if (Social.localUser.authenticated)
        //{
        //    PlayGamesPlatform.Instance.IncrementAchievement("CgkIo_yO-PwdEAIQGw", 1, delegate
        //    {
        //    });
        //}\
        if (shape.GetComponent<Shape>().rota.z != 0)
        {
            if (System.Array.Exists(shape.GetComponent<Shape>().grids, element => element == 0))
            {
                UserDataManager.Instance.GetService().countRota -= 1;
                GameSceneUIManager.Instance.CheckCountRotate();
            }
            else
            {
                if (shape.GetComponent<Shape>().rota.z % 180 != 0)
                {
                    UserDataManager.Instance.GetService().countRota -= 1;
                    GameSceneUIManager.Instance.CheckCountRotate();
                }
            }
        }
        UnityEngine.Object.Destroy(shape);
        Board.Instance.RemoveMatchedBlocks();
    }

    public void ProcessOneShape(int index)
    {
        shapeRemain--;
        ShapeController.Instance.slots[index] = null;
    }

    public void UpdateGameUI()
    {
        GameSceneUIManager.Instance.SetScore(score);
        if (GlobalVariables.GameType == 0)
        {
            GameSceneUIManager.Instance.SetHighScore(UserDataManager.Instance.GetService().HighBasicScore);
        }
        else if (GlobalVariables.GameType == 1)
        {
            GameSceneUIManager.Instance.SetHighScore(UserDataManager.Instance.GetService().HighScore);
        }
    }

    public void UpdateShapeController()
    {
        shapeRemain = 3;
        ShapeController.Instance.AddShapes();
    }

    public void ProcessRewardVideoFinished()
    {
        UnityEngine.Debug.Log("Process Reward Video Finished");
        Timer.Schedule(this, 0.8f, delegate
        {
            GlobalVariables.RewardVideoFinished = false;
            GlobalVariables.RestartGame = false;
            state = GameState.Run;
            Board.Instance.ClearRandomGridsAndContinue();
        });
    }

    public void ProcessRewardVideoNotFinished()
    {
        UnityEngine.Debug.Log("Process Reward Video NotFinished");
        Timer.Schedule(this, 0.8f, delegate
        {
            ProcessGameOver();
        });
    }

    public void ContinueGame()
    {
        Timer.Schedule(this, 0.8f, delegate
        {
            GlobalVariables.RestartGame = false;
            state = GameState.Run;
            Board.Instance.ClearRandomGridsAndContinue();
        });
    }

    public void ProcessGameOver()
    {
        UserDataManager.Instance.GetService().NumberOfGameOver += 1;
        DialogManager.Instance.ShowDialog("GameWinDlg");
    }
}
