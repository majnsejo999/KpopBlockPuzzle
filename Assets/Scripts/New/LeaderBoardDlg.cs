using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;
using System;
using System.Text;
using System.Security.Cryptography;
using BlockGame.New.Core;
using Newtonsoft.Json;
using BlockGame.New.Core.UI;

public class LeaderBoardDlg : BaseDialog
{
    public GameObject popuploading, popupLoadDataFail, popupLeaderBoard;
    public ItemRank itemRank, itemRankUser;
    public List<ItemRank> listItemRank;
    public Transform contentRank;
    private string timeClient;
    private string validateClient;
    private float timeOut = 7;
    public bool EndTimeOut;
    public LeaderBoard data;
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }
    private void OnEnable()
    {
        popupLeaderBoard.SetActive(false);
        popupLoadDataFail.SetActive(false);
        popuploading.SetActive(true);
        if (!string.IsNullOrEmpty(UserDataManager.Instance.GetService().playerName))
            StartCoroutine(pushDataUserStartGame());
    }
    public IEnumerator pushDataUserStartGame()
    {
        timeOut = 7f;
        StartCoroutine(CheckTimeOut());
        UnityWebRequest www = UnityWebRequest.Post("https://gamypuzzle.com/jewelblock/?", DataPush());
        yield return www.SendWebRequest();
        if (!www.isNetworkError && !www.isHttpError)
        {
            if (www.isDone)
            {
                if (!string.IsNullOrEmpty(www.downloadHandler.text))
                {
                    try
                    {
                        DataCallBackFromSever dataCallBackFromSever = JsonUtility.FromJson<DataCallBackFromSever>(www.downloadHandler.text.ToString());
                        if (dataCallBackFromSever.status == 0)
                        {
                            if (!string.IsNullOrEmpty(dataCallBackFromSever.ranking.ToString()))
                            {
                               //  data = JsonConvert.DeserializeObject<LeaderBoard>(dataCallBackFromSever.ranking.ToString());
                                data.leaderBoards = dataCallBackFromSever.ranking;
                                ShowLeaderBoard();
                            }
                            else
                            {
                                Invoke("ShowLoadDataFail", 2f);
                            }
                        }
                        else
                        {
                            Invoke("ShowLoadDataFail", 2f);
                        }
                    }
                    catch
                    {
                        Invoke("ShowLoadDataFail", 2f);
                    }
                }
                else
                {
                    Invoke("ShowLoadDataFail", 2f);
                }
            }
            else
            {
                Invoke("ShowLoadDataFail", 2f);
            }
        }
        else
        {
            Invoke("ShowLoadDataFail", 2f);
        }
    }
    public IEnumerator CheckTimeOut()
    {
        yield return new WaitForSeconds(1);
        timeOut -= 1;
        if (timeOut < 0)
        {
            Debug.Log("time out");
            ShowLoadDataFail();
        }
        else
        {
            if (!EndTimeOut)
                StartCoroutine(CheckTimeOut());
        }
    }
    public void ShowLeaderBoard()
    {
        EndTimeOut = true;
        StopCoroutine(CheckTimeOut());
        popupLeaderBoard.SetActive(true);
        popupLoadDataFail.SetActive(false);
        popuploading.SetActive(false);
        int a = listItemRank.Count;
        if (a == 0)
        {
            for (int i = 0; i < data.leaderBoards.Count; i++)
            {
                ItemRank itemRankClone = Instantiate(itemRank, contentRank);
                listItemRank.Add(itemRankClone);
                if (UserDataManager.Instance.GetService().idDevice != data.leaderBoards[i].device_id)
                {
                    itemRankClone.Init(data.leaderBoards[i].avatar, data.leaderBoards[i].user_name, data.leaderBoards[i].score, data.leaderBoards[i].index,data.leaderBoards[i].country);
                }
                else
                {
                    itemRankUser.Init(data.leaderBoards[i].avatar, data.leaderBoards[i].user_name, data.leaderBoards[i].score, data.leaderBoards[i].index, data.leaderBoards[i].country, true);
                    itemRankClone.Init(data.leaderBoards[i].avatar, data.leaderBoards[i].user_name, data.leaderBoards[i].score, data.leaderBoards[i].index, data.leaderBoards[i].country, true);
                }
            }
        }
        else
        {
            if(a >= data.leaderBoards.Count)
            {
                for (int i = 0; i < data.leaderBoards.Count; i++)
                {                  
                    if (UserDataManager.Instance.GetService().idDevice != data.leaderBoards[i].device_id)
                    {
                        listItemRank[i].Init(data.leaderBoards[i].avatar, data.leaderBoards[i].user_name, data.leaderBoards[i].score, data.leaderBoards[i].index, data.leaderBoards[i].country);
                    }
                    else
                    {
                        itemRankUser.Init(data.leaderBoards[i].avatar, data.leaderBoards[i].user_name, data.leaderBoards[i].score, data.leaderBoards[i].index, data.leaderBoards[i].country,true);
                        listItemRank[i].Init(data.leaderBoards[i].avatar, data.leaderBoards[i].user_name, data.leaderBoards[i].score, data.leaderBoards[i].index, data.leaderBoards[i].country, true);
                    }
                }
            }
            else
            {
                for (int i = 0; i < a; i++)
                {
                    if (UserDataManager.Instance.GetService().idDevice != data.leaderBoards[i].device_id)
                    {
                        listItemRank[i].Init(data.leaderBoards[i].avatar, data.leaderBoards[i].user_name, data.leaderBoards[i].score, data.leaderBoards[i].index,data.leaderBoards[i].country);
                    }
                    else
                    {
                        itemRankUser.Init(data.leaderBoards[i].avatar, data.leaderBoards[i].user_name, data.leaderBoards[i].score, data.leaderBoards[i].index, data.leaderBoards[i].country,true);
                        listItemRank[i].Init(data.leaderBoards[i].avatar, data.leaderBoards[i].user_name, data.leaderBoards[i].score, data.leaderBoards[i].index, data.leaderBoards[i].country, true);
                    }
                }
                for (int i = a; i < data.leaderBoards.Count; i++)
                {
                    ItemRank itemRankClone = Instantiate(itemRank, contentRank);
                    listItemRank.Add(itemRankClone);
                    if (UserDataManager.Instance.GetService().idDevice != data.leaderBoards[i].device_id)
                    {
                        itemRankClone.Init(data.leaderBoards[i].avatar, data.leaderBoards[i].user_name, data.leaderBoards[i].score, data.leaderBoards[i].index, data.leaderBoards[i].country);
                    }
                    else
                    {
                        itemRankUser.Init(data.leaderBoards[i].avatar, data.leaderBoards[i].user_name, data.leaderBoards[i].score, data.leaderBoards[i].index, data.leaderBoards[i].country, true);
                        itemRankClone.Init(data.leaderBoards[i].avatar, data.leaderBoards[i].user_name, data.leaderBoards[i].score, data.leaderBoards[i].index, data.leaderBoards[i].country, true);
                    }
                }
            }
        }
    }
    public List<IMultipartFormSection> DataPush()
    {
        List<IMultipartFormSection> savedata = new List<IMultipartFormSection>();
        TimeSpan span = DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1, 0, 0, 0));
        timeClient = ((int)span.TotalSeconds).ToString();
        validateClient = MD5Hash(UserDataManager.Instance.GetService().idDevice + "puzzle!@#" + timeClient);
        savedata.Add(new MultipartFormDataSection("idDevice", UserDataManager.Instance.GetService().idDevice));
        if (UserDataManager.Instance.GetService().HighBasicScore >= UserDataManager.Instance.GetService().HighScore)
        {
            savedata.Add(new MultipartFormDataSection("score", UserDataManager.Instance.GetService().HighBasicScore.ToString()));
        }
        else
        {
            savedata.Add(new MultipartFormDataSection("score", UserDataManager.Instance.GetService().HighScore.ToString()));
        }
        if (!string.IsNullOrEmpty(UserDataManager.Instance.GetService().playerName))
        {
            savedata.Add(new MultipartFormDataSection("nameUser", UserDataManager.Instance.GetService().playerName));
        }
        savedata.Add(new MultipartFormDataSection("avatar", UserDataManager.Instance.GetService().avatar.ToString()));
        savedata.Add(new MultipartFormDataSection("timeClient", timeClient));
        savedata.Add(new MultipartFormDataSection("validate", validateClient));
        return savedata;
    }
    public static string MD5Hash(string input)
    {
        StringBuilder hash = new StringBuilder();
        MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
        byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

        for (int i = 0; i < bytes.Length; i++)
        {
            hash.Append(bytes[i].ToString("x2"));
        }
        return hash.ToString();
    }
    public void ShowLoadDataFail()
    {
        popuploading.SetActive(false);
        popupLoadDataFail.SetActive(true);
        popupLeaderBoard.SetActive(false);
        EndTimeOut = true;
        StopCoroutine(CheckTimeOut());
    }
    public void ClosePopup()
    {
        popuploading.SetActive(false);
        popupLoadDataFail.SetActive(false);
    }
    public override void Show()
    {
        base.Show();
    }
}

[Serializable]
public class ItemLeaderBoard
{
    public string device_id;
    public string user_name;
    public string country;
    public int score;
    public int avatar;
    public int index;
}
[Serializable]
public class LeaderBoard
{
    public List<ItemLeaderBoard> leaderBoards;
}
[Serializable]
public struct DataCallBackFromSever
{
    public int status;
    public List<ItemLeaderBoard> ranking;
}