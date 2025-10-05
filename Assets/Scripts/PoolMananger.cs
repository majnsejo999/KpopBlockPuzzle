using System.Collections.Generic;
using UnityEngine;

public class PoolMananger : MonoBehaviour
{
	private const int MaxBlockMatch = 81;

	public Sprite[] BlockSprites;

	public GameObject BlockMatchPrefab;

	[SerializeField]
	public Stack<BlockMatch> blockMatchPool;

	private static PoolMananger instance;

	public static PoolMananger Instance => instance;

	private void Awake()
	{
		instance = this;
		InitPools();
	}

	private void InitPools()
	{
		blockMatchPool = new Stack<BlockMatch>();
		for (int i = 0; i < 81; i++)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(BlockMatchPrefab);
			gameObject.SetActive(value: false);
			blockMatchPool.Push(gameObject.GetComponent<BlockMatch>());
		}
	}

	public BlockMatch SpawnBlockMatch(int blockType)
	{
		BlockMatch blockMatch = blockMatchPool.Pop();
		blockMatch.renderer.sprite = BlockSprites[blockType];
		blockMatch.gameObject.SetActive(value: true);
		int a = Random.Range(0, 100) % 6;
		blockMatch.renderer.sprite = blockMatch.sprBlock[a];
		return blockMatch;
	}

	public void DespawnBlockMatch(BlockMatch item)
	{
		item.gameObject.SetActive(value: false);
		item.transform.eulerAngles = Vector3.zero;
		item.transform.localScale = Vector3.one;
		blockMatchPool.Push(item);
	}
}
