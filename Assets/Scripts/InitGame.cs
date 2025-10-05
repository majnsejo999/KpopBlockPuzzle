using DG.Tweening;
using BlockGame.New.Core;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitGame : MonoBehaviour
{
    private AsyncOperation asyncMainScene;

    private void Awake()
    {
        Input.multiTouchEnabled = false;
        Application.targetFrameRate = 60;
        LanguageManager.Load();
    }

    private void Start()
    {
        AdjustScreen();
        InitAnalytics();
        ShowLoadingAnim();
        InitUserData();
        LoadGameResources();
        InitGpService();
        if (UserDataManager.Instance.GetService().TutorialProgress >= 5)
        {
            UserDataManager.Instance.GetService().AdvancedGameModeUnlocked = true;
        }
    }

    public void AdjustScreen()
    {
        float num = Screen.height;
        float num2 = Screen.width;
        float orthographicSize = Camera.main.orthographicSize;
        float orthographicSize2 = 3.6f * ((float)Screen.height * 1f / (float)Screen.width);
        Camera.main.orthographicSize = orthographicSize2;
    }

    private void OnApplicationQuit()
    {
        UnityEngine.Debug.Log("MainScene OnApplicationQuit");
        ApplicationController.ProcessApplicationQuit();
    }

    private void ShowLoadingAnim()
    {
        Transform transform = GameObject.Find("LoadingImg").transform;
        transform.DORotate(new Vector3(0f, 0f, -360f), 2.4f, RotateMode.LocalAxisAdd).SetEase(Ease.Linear);
    }

    private void InitUserData()
    {
        UserDataManager.Instance.InitDesEncrypt(GlobalConstants.DesKey);
        UserDataManager.Instance.Load();
    }

    private void LoadGameResources()
    {
        Timer.Schedule(this, 0.1f, delegate
        {
            StartCoroutine(AsyncLoad());
        });
    }

    private void InitAnalytics()
    {
    }

    private void InitializeFirebase()
    {

    }

    private IEnumerator AsyncLoad()
    {
        Transform transform = GameObject.Find("MyGame").transform;
        TutorialManager.LoadTutorialInfo();
        ShapeInfoManager.Init();
        ShopInfoManager.Init();
        StageManager.Instance.LoadStageData();
        Purchaser.Instance.Init();
        GameObject mainSceneUI = Instantiate(Resources.Load<GameObject>("Prefabs/UI/MainSceneUI"));
        mainSceneUI.transform.SetParent(base.transform, false);
        yield return null;
        GameObject gameSceneUI = Instantiate(Resources.Load<GameObject>("Prefabs/UI/GameSceneUI"));
        gameSceneUI.transform.SetParent(base.transform, false);
        yield return null;
        GameObject dialogs = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Dialogs/Dialogs"));
        dialogs.transform.SetParent(base.transform, false);
        yield return null;
        Object.Instantiate(Resources.Load<GameObject>("Prefabs/UI/TopCanvas"));
        yield return null;
        string[] gameDialogs = DialogManager.Instance.GameDialogs;
        foreach (string dlg in gameDialogs)
        {
            DialogManager.Instance.CreateDialog(dialogs, dlg);
            yield return null;
        }
        GameObject maskDlg = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Prefabs/UI/Dialogs/MaskDlg"));
        maskDlg.transform.SetParent(base.transform, false);
        maskDlg.SetActive(false);
        //MyNotification..RegisterForNotification();
        MyNotification.CancelAllScheduleNotification();
        StartCoroutine(LoadMainScene());
        yield return null;
    }

    private IEnumerator LoadMainScene()
    {
        UnityEngine.Debug.Log("LoadMainScene");
        if (GlobalVariables.FirstEnter)
        {
            SceneTransManager.Instance.SetCurrentScene("GameScene");
            asyncMainScene = SceneManager.LoadSceneAsync(2);
        }
        else
        {
            asyncMainScene = SceneManager.LoadSceneAsync(1);
        }
        asyncMainScene.allowSceneActivation = false;
        while (!asyncMainScene.isDone)
        {
            UnityEngine.Debug.Log("asyncMainScene.progress " + asyncMainScene.progress);
            if (asyncMainScene.progress == 0.9f)
            {
                asyncMainScene.allowSceneActivation = true;
            }
            yield return null;
        }
        if (GlobalVariables.FirstEnter)
        {
            AudioManager.Instance.PlayAudioMusic("bgm_01");
        }
        yield return asyncMainScene;
    }

    private void InitGpService()
    {
        //PlayGamesClientConfiguration configuration = new PlayGamesClientConfiguration.Builder().EnableSavedGames().Build();
        //PlayGamesPlatform.InitializeInstance(configuration);
        //PlayGamesPlatform.DebugLogEnabled = true;
        //PlayGamesPlatform.Activate();
        //if (!Social.localUser.authenticated && GlobalVariables.FirstEnter)
        //{
        //	GlobalVariables.ResumeFromDesktop = false;
        //	Social.localUser.Authenticate(delegate(bool success)
        //	{
        //		UnityEngine.Debug.Log("Authenticate callback retrieved.");
        //		if (success)
        //		{
        //			UnityEngine.Debug.Log("Authentication success!");
        //			((PlayGamesPlatform)Social.Active).SetGravityForPopups(Gravity.TOP);
        //		}
        //	});
        //}
        //if (Social.localUser.authenticated)
        //{
        //	SocialPlatformAchievementConfig.ReportNewClassicHighScore();
        //	SocialPlatformAchievementConfig.ReportNewHighScore();
        //}
    }

    private void InitGameCenter()
    {
    }
}
