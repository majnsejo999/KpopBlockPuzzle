using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace BlockGame.New.Core.UI
{
    public class GameSceneUIManager : BaseUIManager
    {
        public Text scoreText;

        public Text highScoreText;

        public GameObject TopUI;

        public GameObject BottomUI;

        public GameObject NewRecordInGame;

        public Image HighScoreBanner;

        public Image HighScoreInfo;

        public Image NormalCup;

        public Image NewRecordCupLight;

        public Image LightCup;

        public GameObject NewRecordCup;

        public GameObject ObstacleLock;

        public GameObject shapeControllerBg;

        public GameObject ObstaclePreview;

        public Text NextObstacle;

        public Image IconObstacle;

        public Text BombMove;

        private Canvas canvas;

        private int nextObstacleIndex;

        public GameObject ObstacleEffectIcon;

        public GameObject EmergingObstacle;

        public Vector3 EmergingObstaclePos;

        private static GameSceneUIManager instance;
        public Sprite sprOnRota, sprOffRota;
        public Image imgOnOff;
        public bool turnONRota;
        public Text txt_countRota;
        public static GameSceneUIManager Instance => instance;

        protected override void Awake()
        {
            base.Awake();
            instance = this;
            canvas = GetComponent<Canvas>();
            turnONRota = false;
            txt_countRota.text = UserDataManager.Instance.GetService().countRota.ToString();
        }

        protected override void Start()
        {
            base.Start();
        }

        public override void ShowUI()
        {
            base.ShowUI();
            if (UserDataManager.Instance.GetService().TutorialProgress >= 3)
            {
                PlayEmergeAnime();
            }
            else
            {
                ShapeController.Instance.UpdatePosition();
            }
        }

        public void PlayEmergeAnime()
        {
            float delay = 0f;
            float duration = 0.4f;
            Vector3 localPosition = TopUI.transform.localPosition;
            TopUI.transform.localPosition = new Vector3(0f, localPosition.y + 250f);
            TopUI.transform.DOLocalMove(localPosition, duration).SetEase(Ease.OutBack).SetDelay(delay);
            Vector3 localPosition2 = BottomUI.transform.localPosition;
            BottomUI.transform.localPosition = new Vector3(0f, localPosition2.y - 250f);
            BottomUI.transform.DOLocalMove(localPosition2, duration).SetEase(Ease.OutBack).SetDelay(delay)
                .OnComplete(delegate
                {
                    ShapeController.Instance.UpdatePosition();
                });
        }
        public void ONOFFButton()
        {
            if (UserDataManager.Instance.GetService().countRota > 0)
            {
                turnONRota = !turnONRota;
                ChangeONOFF();
            }
            else
            {
                DialogManager.Instance.ShowDialog("ShopDlg");
            }
        }
        private void ChangeONOFF()
        {
            if (turnONRota)
            {
                imgOnOff.sprite = sprOnRota;
                for (int i = 0; i < 3; i++)
                {
                    if (ShapeController.Instance.slots[i] != null)
                    {
                        if (ShapeController.Instance.slots[i].rowSize != ShapeController.Instance.slots[i].colSize || System.Array.Exists(ShapeController.Instance.slots[i].grids, element => element == 0))
                        {
                            ShapeController.Instance.slots[i].rota.gameObject.SetActive(true);
                        }
                    }
                }

            }
            else
            {
                imgOnOff.sprite = sprOffRota;
                txt_countRota.text = UserDataManager.Instance.GetService().countRota.ToString();
                for (int i = 0; i < 3; i++)
                {
                    if (ShapeController.Instance.slots[i] != null)
                    {
                        ShapeController.Instance.slots[i].transform.eulerAngles = Vector3.zero;
                        ShapeController.Instance.slots[i].rota.gameObject.SetActive(false);
                    }
                }
            }
        }
        public void CheckCountRotate()
        {
            txt_countRota.text = UserDataManager.Instance.GetService().countRota.ToString();
            if (UserDataManager.Instance.GetService().countRota <= 0)
            {
                imgOnOff.sprite = sprOffRota;
                for (int i = 0; i < 3; i++)
                {
                    if (ShapeController.Instance.slots[i] != null)
                    {
                        ShapeController.Instance.slots[i].transform.eulerAngles = Vector3.zero;
                        ShapeController.Instance.slots[i].rota.gameObject.SetActive(false);
                    }
                }
            }
        }
        public void ResetNewRecordEffect()
        {
            NewRecordCupLight.gameObject.SetActive(value: false);
            NewRecordCup.gameObject.SetActive(value: false);
            LightCup.gameObject.SetActive(value: false);
            NormalCup.gameObject.SetActive(value: true);
        }

        public void PlayHighScoreCupAnime()
        {
            NormalCup.gameObject.SetActive(value: false);
            NewRecordCupLight.color = new Color(1f, 1f, 1f, 0f);
            NewRecordCupLight.gameObject.SetActive(value: true);
            NewRecordCup.gameObject.SetActive(value: true);
            NewRecordCupLight.DOFade(1f, 0.35f).OnComplete(delegate
            {
                NewRecordCupLight.DOFade(0f, 0.3f).SetDelay(0.3f).OnComplete(delegate
                {
                    NewRecordCupLight.gameObject.SetActive(value: false);
                    NewRecordCup.gameObject.SetActive(value: true);
                });
            });
        }

        public void ShowInGameHighScoreAnime()
        {
            HighScoreInfo.transform.localScale = new Vector3(1.05f, 1.05f, 1f);
            HighScoreInfo.color = new Color(1f, 1f, 1f, 1f);
            HighScoreBanner.transform.localScale = Vector3.zero;
            HighScoreBanner.color = new Color(1f, 1f, 1f, 1f);
            NewRecordInGame.SetActive(value: true);
            AudioManager.Instance.PlayAudioEffect("new_record_in_game");
            HighScoreInfo.transform.DOScale(Vector3.one, 0.2f);
            HighScoreBanner.transform.DOScale(Vector3.one, 0.35f).SetEase(Ease.OutBack);
            HighScoreInfo.DOFade(0f, 0.5f).SetDelay(1f).OnStart(delegate
            {
                HighScoreInfo.transform.DOScale(1.6f * Vector3.one, 0.4f).SetEase(Ease.InBack, 1.5f);
            });
            HighScoreBanner.DOFade(0f, 0.5f).SetDelay(1.15f).OnComplete(delegate
            {
                PlayHighScoreCupAnime();
            })
                .OnStart(delegate
                {
                    HighScoreBanner.transform.DOScale(1.6f * Vector3.one, 0.4f).SetEase(Ease.InBack, 1.5f);
                });
        }

        public void SetHighScore(int highScore)
        {
            highScoreText.text = highScore.ToString();
        }

        public void SetScore(int score)
        {
            scoreText.text = score.ToString();
        }

        public override void RefreshUI()
        {
            scoreText.text = "Score " + 0;
        }

        public void ShowSettingDlg()
        {
            DialogManager.Instance.ShowDialog("SettingDlg");
        }

        public void ShowStoreDlg()
        {
            DialogManager.Instance.ShowDialog("ShopDlg");
        }

        public void Pass()
        {
            GameLogic.Instance.ProcessLevelFinish();
        }

        public void NewRecordPass()
        {
        }

        internal void UpdatePreviewItem(int nextObstacleIndex)
        {
            this.nextObstacleIndex = nextObstacleIndex;
            switch (nextObstacleIndex)
            {
                case 0:
                    BombMove.gameObject.SetActive(value: false);
                    int a = Random.Range(0, 1000) % 6;
                    IconObstacle.sprite = Resources.Load<Sprite>("Textures/GameScene/d" +a);
                    break;
                case 1:
                    BombMove.gameObject.SetActive(value: false);
                    IconObstacle.sprite = Resources.Load<Sprite>("Textures/GameScene/fangkuai2");
                    break;
                case 2:
                    BombMove.gameObject.SetActive(value: true);
                    IconObstacle.sprite = Resources.Load<Sprite>("Textures/GameScene/bomb");
                    break;
            }
        }

        public void UpdateObstacleIcon(Vector3 pos, GameObject go)
        {
            IconObstacle.transform.localScale = Vector3.zero;
            EmergingObstacle = go;
            EmergingObstaclePos = pos;
            Vector3 position = IconObstacle.transform.position;
            ObstacleEffectIcon = Object.Instantiate(Resources.Load<GameObject>("Prefabs/UI/Widgets/ObstacleIcon"));
            ObstacleEffectIcon.transform.SetParent(TopCanvasManager.Instance.transform, worldPositionStays: false);
            GameObject gameObject = ObstacleEffectIcon.transform.Find("BombMove").gameObject;
            gameObject.SetActive(value: false);
            Image component = ObstacleEffectIcon.GetComponent<Image>();
            if (nextObstacleIndex == 2)
            {
                gameObject.SetActive(value: true);
            }
            component.sprite = IconObstacle.GetComponent<Image>().sprite;
            ObstacleEffectIcon.GetComponent<RectTransform>().sizeDelta = new Vector2(Board.Instance.cellSize * 100f, Board.Instance.cellSize * 100f);
            ObstacleEffectIcon.transform.position = position;
            TweenCallback tweenCallback = delegate
            {
            };
            TweenCallback tweenCallback2 = delegate
            {
            };
            int tutorialProgress = UserDataManager.Instance.GetService().TutorialProgress;
            if (nextObstacleIndex == 0 && tutorialProgress == 5)
            {
                GameLogic.Instance.CheckObstacleTutorial(nextObstacleIndex);
            }
            else if (nextObstacleIndex == 1 && tutorialProgress == 6)
            {
                GameLogic.Instance.CheckObstacleTutorial(nextObstacleIndex);
            }
            else if (nextObstacleIndex == 2 && tutorialProgress == 7)
            {
                GameLogic.Instance.CheckObstacleTutorial(nextObstacleIndex);
            }
            else if (nextObstacleIndex == 3 && tutorialProgress == 8)
            {
                GameLogic.Instance.CheckObstacleTutorial(nextObstacleIndex);
            }
            else
            {
                ObstacleEffectIcon.transform.DOScale(Vector3.one * 1.1f, 0.2f).OnComplete(delegate
                {
                    MoveObstacleIcon();
                });
            }
        }

        public void MoveObstacleIcon()
        {
            NextObstacle.text = GameLogic.Instance.ObstacleMove.ToString();
            ObstacleEffectIcon.transform.DOMove(EmergingObstaclePos, 0.65f).SetDelay(0.2f).OnComplete(delegate
            {
                ScaleObstacleIcon();
            })
                .OnStart(delegate
                {
                    IconObstacle.transform.DOScale(Vector3.one, 0.4f);
                });
        }

        public void ScaleObstacleIcon()
        {
            ObstacleEffectIcon.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack).SetDelay(0.1f)
                .OnComplete(delegate
                {
                    UnityEngine.Object.Destroy(ObstacleEffectIcon);
                    EmergingObstacle.GetComponent<SpriteRenderer>().color = Color.white;
                });
        }
    }
}
