using System.Diagnostics;
using UnityEngine;

public class GameTools : MonoBehaviour
{
	public void BtnConvertClicked()
	{
		string empty = string.Empty;
		empty = Application.dataPath + "/GameTools/Scripts/MultiLanguage.py " + Application.dataPath + "/GameTools/Excel/Lang.xlsx " + Application.dataPath + "/Resources/Configs/";
		ProcessPythonCode(empty);
	}

	private void ProcessPythonCode(string arguments)
	{
		ProcessStartInfo processStartInfo = new ProcessStartInfo();
		processStartInfo.FileName = "/usr/local/bin/python3";
		UnityEngine.Debug.Log("psi.FileName: " + processStartInfo.FileName);
		processStartInfo.UseShellExecute = false;
		processStartInfo.RedirectStandardOutput = true;
		processStartInfo.Arguments = arguments;
		UnityEngine.Debug.Log(processStartInfo.Arguments);
		Process process = Process.Start(processStartInfo);
		string str = process.StandardOutput.ReadToEnd();
		process.WaitForExit();
		UnityEngine.Debug.Log("command output " + str);
	}
}
