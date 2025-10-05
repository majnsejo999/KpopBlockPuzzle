using UnityEngine;

public class DonotDestroyOnLoad : MonoBehaviour
{
	public static DonotDestroyOnLoad instance;

	private void Start()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
		Object.DontDestroyOnLoad(base.gameObject);
	}
}
