using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;
using BlockGame.New.Core;
using System.Threading.Tasks;

public class FirebaseControl : MonoBehaviour
{
    public static FirebaseControl instance;
    public string nameNoti;

    public string[] txtNoti;

    public bool firebaseIsReady;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }       
    }
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
#if UNITY_ANDROID
            MyNotification.CancelAllDisplayNotification();
            MyNotification.CancelAllScheduleNotification();
            txtNoti[5] = "Reminder! Your best now: " + UserDataManager.Instance.GetService().HighScore + "! Break your own record today!";
            DateTime d = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 0, 0);

            if (DateTime.Now.Hour < 10)
            {
                MyNotification.SendPush("Wood Block", txtNoti[Random.Range(0, txtNoti.Length)], (int)d.AddDays(0.167f).Subtract(DateTime.Now).TotalSeconds, true, "d0");
                MyNotification.SendPush("Wood Block", txtNoti[Random.Range(0, txtNoti.Length)], (int)d.AddDays(0.5f).Subtract(DateTime.Now).TotalSeconds, true, "d0-1");
            }
            else
            {
                if (DateTime.Now.Hour < 16)
                {
                    MyNotification.SendPush("Wood Block", txtNoti[Random.Range(0, txtNoti.Length)], (int)d.AddDays(0.5f).Subtract(DateTime.Now).TotalSeconds, true, "d0-1");
                }
            }

            MyNotification.SendPush("Wood Block", txtNoti[Random.Range(0, txtNoti.Length)], (int)d.AddDays(1).Subtract(System.DateTime.Now).TotalSeconds, true, "d1");
            MyNotification.SendPush("Wood Block", txtNoti[Random.Range(0, txtNoti.Length)], (int)d.AddDays(1.167f).Subtract(System.DateTime.Now).TotalSeconds, true, "d1-1");
            MyNotification.SendPush("Wood Block", txtNoti[Random.Range(0, txtNoti.Length)], (int)d.AddDays(1.5f).Subtract(System.DateTime.Now).TotalSeconds, true, "d1-2");
            MyNotification.SendPush("Wood Block", txtNoti[Random.Range(0, txtNoti.Length)], (int)d.AddDays(2).Subtract(System.DateTime.Now).TotalSeconds, true, "d2");
            MyNotification.SendPush("Wood Block", txtNoti[Random.Range(0, txtNoti.Length)], (int)d.AddDays(3).Subtract(System.DateTime.Now).TotalSeconds, true, "d3");
            MyNotification.SendPush("Wood Block", txtNoti[Random.Range(0, txtNoti.Length)], (int)d.AddDays(5).Subtract(System.DateTime.Now).TotalSeconds, true, "d5");
            MyNotification.SendPush("Wood Block", txtNoti[Random.Range(0, txtNoti.Length)], (int)d.AddDays(7).Subtract(System.DateTime.Now).TotalSeconds, true, "d7");
#endif
        }
        else
        {
#if UNITY_ANDROID
            MyNotification.CancelAllDisplayNotification();
            MyNotification.CancelAllScheduleNotification();

            nameNoti = MyNotification.GetNotificationCallback();
#endif
        }

    }

    private void OnApplicationQuit()
    {
#if UNITY_ANDROID
        MyNotification.CancelAllDisplayNotification();
        MyNotification.CancelAllScheduleNotification();
        txtNoti[5] = "Reminder! Your best now: " + UserDataManager.Instance.GetService().HighScore + "! Break your own record today!";
        DateTime d = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 0, 0);

        if (DateTime.Now.Hour < 9)
        {
            MyNotification.SendPush("Wood Block", txtNoti[Random.Range(0, txtNoti.Length)], (int)d.AddDays(0.167f).Subtract(DateTime.Now).TotalSeconds, true, "d0");
            MyNotification.SendPush("Wood Block", txtNoti[Random.Range(0, txtNoti.Length)], (int)d.AddDays(0.5f).Subtract(DateTime.Now).TotalSeconds, true, "d0-1");
        }
        else
        {
            if (DateTime.Now.Hour < 15)
            {
                MyNotification.SendPush("Wood Block", txtNoti[Random.Range(0, txtNoti.Length)], (int)d.AddDays(0.5f).Subtract(DateTime.Now).TotalSeconds, true, "d0-1");
            }
        }
        MyNotification.SendPush("Wood Block", txtNoti[Random.Range(0, txtNoti.Length)], (int)d.AddDays(1).Subtract(DateTime.Now).TotalSeconds, true, "d1");
        MyNotification.SendPush("Wood Block", txtNoti[Random.Range(0, txtNoti.Length)], (int)d.AddDays(1.167f).Subtract(DateTime.Now).TotalSeconds, true, "d1-1");
        MyNotification.SendPush("Wood Block", txtNoti[Random.Range(0, txtNoti.Length)], (int)d.AddDays(1.5f).Subtract(DateTime.Now).TotalSeconds, true, "d1-2");
        MyNotification.SendPush("Wood Block", txtNoti[Random.Range(0, txtNoti.Length)], (int)d.AddDays(2).Subtract(DateTime.Now).TotalSeconds, true, "d2");
        MyNotification.SendPush("Wood Block", txtNoti[Random.Range(0, txtNoti.Length)], (int)d.AddDays(3).Subtract(DateTime.Now).TotalSeconds, true, "d3");
        MyNotification.SendPush("Wood Block", txtNoti[Random.Range(0, txtNoti.Length)], (int)d.AddDays(5).Subtract(DateTime.Now).TotalSeconds, true, "d5");
        MyNotification.SendPush("Wood Block", txtNoti[Random.Range(0, txtNoti.Length)], (int)d.AddDays(7).Subtract(DateTime.Now).TotalSeconds, true, "d7");
#endif
    }

    private void Start()
    {
#if UNITY_ANDROID
        MyNotification.CancelAllDisplayNotification();
        MyNotification.CancelAllScheduleNotification();
#endif
    }
}
