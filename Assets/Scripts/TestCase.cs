using UnityEngine;

public class TestCase : MonoBehaviour
{
	private void Start()
	{
		MonoBehaviour.print("GA.ProfileSignOff();");
	}

	private void OnGUI()
	{
		if (GUI.Button(new Rect(150f, 100f, 500f, 100f), "Event"))
		{
			string[] array = new string[3]
			{
				"one",
				"1234567890123456000",
				"one"
			};
		}
	}
}
