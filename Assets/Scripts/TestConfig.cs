using UnityEngine;

public class TestConfig : MonoBehaviour
{
	public int TestDiscardNum;

	public int TestUndoNum;

	private static TestConfig instance;

	public static TestConfig Instance => instance;

	private void Awake()
	{
		instance = this;
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}
