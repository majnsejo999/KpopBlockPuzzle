using BlockGame.New.Core.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
public class ItemRank : MonoBehaviour
{
    public Image imgAva, khungBoard, imgFlag;
    public Text txtName, txtScore, txtRank;
    public Sprite khungBoardNormal, khungBoardUser;
    public SpriteAtlas spriteAtlas;
    public void Init(int indexAva, string name, int score, int indexRank,string country, bool isUser = false)
    {
        if (!isUser)
        {
            khungBoard.sprite = khungBoardNormal;
        }
        else
        {
            khungBoard.sprite = khungBoardUser;
        }
        imgAva.sprite = MainSceneUIManager.Instance.sprAva[indexAva];
        if (name.Length >= 15)
        {
            name = name.Remove(15, name.Length - 15) + "...";
            txtName.text = name;
        }
        else
        {
            txtName.text = name;
        }
        txtName.text = name;
        txtScore.text = score.ToString();
        txtRank.text = (indexRank + 1).ToString();
        if (spriteAtlas.GetSprite(country.ToLower()) == null)
        {
            imgFlag.sprite = spriteAtlas.GetSprite("nocountry");
        }
        else
        {
            imgFlag.sprite = spriteAtlas.GetSprite(country.ToLower());
        }
    }

}
