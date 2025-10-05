using UnityEngine;
using UnityEngine.UI;
using System.Collections;
namespace BlockGame.New.Core.UI
{
    public class SignInDlg : BaseDialog
    {
        public Button btnOK;
        public Image imgAva;
        private int indexAva = 0;
        public InputField inputFieldName;
        protected override void Awake()
        {
            base.Awake();
        }

        protected override void Start()
        {
            base.Start();
            btnOK.interactable = false;
        }
        public void ClickText()
        {
            if (!string.IsNullOrEmpty(inputFieldName.text) && !btnOK.interactable)
            {
                btnOK.interactable = true;
            }
        }
        public void ClickOK()
        {
            UserDataManager.Instance.GetService().playerName = inputFieldName.textComponent.text;
            UserDataManager.Instance.GetService().avatar = indexAva;
            
            Close();
            DialogManager.Instance.ShowDialog("LeaderBoardDlg");
        }
        public void NextAva()
        {
            indexAva += 1;
            if (indexAva >= 10)
            {
                indexAva = 0;
            }
            imgAva.sprite = MainSceneUIManager.Instance.sprAva[indexAva];
        }
        public void Preview()
        {
            indexAva -= 1;
            if (indexAva < 0)
            {
                indexAva = 9;
            }
            imgAva.sprite = MainSceneUIManager.Instance.sprAva[indexAva];
        }
        public override void Show()
        {
            base.Show();
        }

    }
}
