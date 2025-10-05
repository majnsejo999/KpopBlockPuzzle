using System.Collections;
using UnityEngine;

public class NativeShare : MonoBehaviour
{
	public bool IsScreenShot;

	public string Subject;

	public string ShareMessage;

	public string Url;

	private bool isProcessing;

	public string ScreenshotName;

	private void Start()
	{
	}

	public void ShareScreenshotWithText()
	{
	}

	public void Share()
	{
		GlobalVariables.ResumeFromDesktop = false;
		if (!isProcessing)
		{
			StartCoroutine(ShareScreenshot());
		}
	}

	private IEnumerator ShareScreenshot()
	{
		isProcessing = true;
		yield return new WaitForEndOfFrame();
		yield return new WaitForSeconds(0.4f);
		if (!Application.isEditor)
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("android.content.Intent");
			AndroidJavaObject androidJavaObject = new AndroidJavaObject("android.content.Intent");
			androidJavaObject.Call<AndroidJavaObject>("setAction", new object[1]
			{
				androidJavaClass.GetStatic<string>("ACTION_SEND")
			});
			androidJavaObject.Call<AndroidJavaObject>("putExtra", new object[2]
			{
				androidJavaClass.GetStatic<string>("EXTRA_TEXT"),
				ShareMessage + "\n" + Url
			});
			androidJavaObject.Call<AndroidJavaObject>("setType", new object[1]
			{
				"text/plain"
			});
			AndroidJavaClass androidJavaClass2 = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject @static = androidJavaClass2.GetStatic<AndroidJavaObject>("currentActivity");
			AndroidJavaObject androidJavaObject2 = androidJavaClass.CallStatic<AndroidJavaObject>("createChooser", new object[2]
			{
				androidJavaObject,
				Subject
			});
			@static.Call("startActivity", androidJavaObject2);
		}
		isProcessing = false;
	}
}
