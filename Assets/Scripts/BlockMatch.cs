using BlockGame.New.Core;
using UnityEngine;

public class BlockMatch : MonoBehaviour
{
	public Rigidbody2D body;

	public SpriteRenderer renderer;
	public Sprite[] sprBlock;

	private int directionParam;

	public void Play()
	{
		renderer.size = new Vector2(Board.Instance.cellSize, Board.Instance.cellSize);
		body.gravityScale = 0f;
		directionParam = ((UnityEngine.Random.Range(0, 2) != 0) ? 1 : (-1));
		float num = UnityEngine.Random.Range(0.7f, 1f);
		Timer.Schedule(this, 0.01f, delegate
		{
			AddForce();
		});
		Timer.Schedule(this, 1f, delegate
		{
			PoolMananger.Instance.DespawnBlockMatch(this);
		});
	}

	private void AddForce()
	{
		body.gravityScale = 4.8f;
		int num = UnityEngine.Random.Range(160, 240);
		int num2 = UnityEngine.Random.Range(640, 780);
		int num3 = UnityEngine.Random.Range(120, 280);
		body.AddTorque(num3 * directionParam);
		body.AddForce(new Vector2(directionParam * num, num2));
	}
}
