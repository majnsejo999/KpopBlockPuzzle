using DG.Tweening;
using BlockGame.New.Core.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BlockGame.New.Core
{
    public class ShapeController : MonoBehaviour
    {
        private static int ShapeInitXOffset = 5;

        private static Vector3 ShapeInitPosOffset = new Vector3(ShapeInitXOffset, 0f, 0f);

        private List<ShapeInfo> shapes;

        public GameObject[] slotsParent;

        public Shape[] slots;

        private int[] shapeIds;

        private int totalCol;

        private float _shapeDistance = 2.18f;

        public GameObject Selector;

        public GameObject Settled;

        private static ShapeController instance;

        public static ShapeController Instance => instance;

        public void Awake()
        {
            instance = this;
            InitShapeSelector();
        }

        private void Start()
        {
        }

        public void UpdatePosition()
        {
            base.transform.localScale = new Vector3(ScreenManager.ResolutionAdaptionRatio, ScreenManager.ResolutionAdaptionRatio);
            Vector3 position = GameSceneUIManager.Instance.shapeControllerBg.transform.position;
            base.transform.position = new Vector3(position.x, position.y - 0.25f * ScreenManager.ResolutionAdaptionRatio, position.z);
        }

        private void InitShapeSelector()
        {
            slots = new Shape[ShapeInfoManager.SHAPE_SELECTOR_SLOT];
            shapeIds = new int[ShapeInfoManager.SHAPE_SELECTOR_SLOT];
        }

        public void CreateShapeSelector()
        {
            int tutorialProgress = UserDataManager.Instance.GetService().TutorialProgress;
            if (tutorialProgress > 2)
            {
                Refresh();
            }
            else
            {
                CreateTutorial(tutorialProgress);
            }
        }

        public void CreateTutorial(int progress)
        {
            Clear();
            switch (progress)
            {
                case 0:
                    {
                        ShapeInfo shapeInfoById3 = ShapeInfoManager.GetShapeInfoById(10);
                        int color3 = 3;
                        GameObject gameObject3 = CreateShape(1, shapeInfoById3, color3);
                        break;
                    }
                case 1:
                    {
                        ShapeInfo shapeInfoById2 = ShapeInfoManager.GetShapeInfoById(11);
                        int color2 = 3;
                        GameObject gameObject2 = CreateShape(1, shapeInfoById2, color2);
                        break;
                    }
                default:
                    {
                        ShapeInfo shapeInfoById = ShapeInfoManager.GetShapeInfoById(0);
                        int color = 2;
                        GameObject gameObject = CreateShape(1, shapeInfoById, color);
                        break;
                    }
            }
        }

        public void AddShapes(bool ensureFill = false)
        {
            totalCol = 0;
            AudioManager.Instance.PlayAudioEffect("slide_in");
            shapes = ShapeInfoManager.shapeInfo[GameLogic.Instance.RandomGroup()].ToList();
            if (!ensureFill)
            {
                for (int i = 0; i < ShapeInfoManager.SHAPE_SELECTOR_SLOT; i++)
                {
                    ShapeInfo randomShapeInfo = GetRandomShapeInfo(i);
                    GameObject gameObject = CreateShape(i, randomShapeInfo);
                }
                return;
            }
            bool flag = false;
            int num = 0;
            int num2 = 3;
            for (int j = 0; j < ShapeInfoManager.SHAPE_SELECTOR_SLOT; j++)
            {
                if (!flag)
                {
                    int num3 = UnityEngine.Random.Range(0, num2);
                    if (num3 != num)
                    {
                        ShapeInfo randomShapeInfo2 = GetRandomShapeInfo(j);
                        GameObject gameObject2 = CreateShape(j, randomShapeInfo2);
                        num2--;
                    }
                    else
                    {
                        ShapeInfo info = shapes[2];
                        GameObject gameObject3 = CreateShape(j, info);
                        flag = true;
                    }
                }
                else
                {
                    ShapeInfo randomShapeInfo3 = GetRandomShapeInfo(j);
                    GameObject gameObject4 = CreateShape(j, randomShapeInfo3);
                }
            }
        }

        public GameObject CreateShape(int index, ShapeInfo info, int color = -1)
        {
            int rows = info.Rows;
            int cols = info.Cols;
            int[] grids = info.Grids;
            if (color == -1)
            {
                color = UnityEngine.Random.Range(0, 6);
            }
            GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Prefabs/Game/Shape"));
            gameObject.transform.SetParent(base.gameObject.transform, worldPositionStays: false);
            Shape component = gameObject.GetComponent<Shape>();
            component.Create(rows, cols, grids, index, color);
            gameObject.SetActive(true);
            gameObject.transform.localPosition = new Vector3(_shapeDistance * (float)(index - 1), 0.25f);
            component.ShapeStartPos = gameObject.transform.position;
            component.ShapeStartScale = gameObject.transform.localScale;
            if (UserDataManager.Instance.GetService().TutorialProgress >= 3)
            {
                gameObject.transform.position += ShapeInitPosOffset;
                Transform transform = gameObject.transform;
                Vector3 position = gameObject.transform.position;
                transform.DOMoveX(position.x - (float)ShapeInitXOffset, 0.24f);
            }
            slots[index] = component;
            if (GameSceneUIManager.Instance.turnONRota)
            {
                if (slots[index].rowSize != slots[index].colSize || System.Array.Exists(slots[index].grids, element => element == 0))
                {
                    slots[index].rota.gameObject.SetActive(true);
                }
            }
            shapeIds[index] = info.Id;
            return gameObject;
        }

        public ShapeInfo GetRandomShapeInfo(int shapeIndex)
        {
            int index = Random.Range(0, shapes.Count);
            ShapeInfo result = shapes[index];
            shapes.RemoveAt(index);
            return result;
        }

        public void Clear()
        {
            for (int i = 0; i < ShapeInfoManager.SHAPE_SELECTOR_SLOT; i++)
            {
                if (slots[i] != null)
                {
                    Destroy(slots[i].gameObject);
                    slots[i] = null;
                }
            }
        }

        public void Refresh(bool ensureFill = false)
        {
            Clear();
            AddShapes(ensureFill);
        }

        public void MoveAllShapeBack()
        {
            Board.Instance.HideBlockPreview();
            Shape[] array = slots;
            foreach (Shape shape in array)
            {
                if (shape != null)
                {
                    shape.MoveShapeBack();
                }
            }
        }
    }
}
