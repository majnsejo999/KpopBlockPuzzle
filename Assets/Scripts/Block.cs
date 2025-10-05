using UnityEngine;

public class Block : MonoBehaviour
{
	public Shader greyScaleShader;

	private Material grayScale;

	private Material normal;

	private SpriteRenderer spriteRenderer;

	private void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		normal = spriteRenderer.material;
		grayScale = new Material(greyScaleShader);
	}

	private void Start()
	{
		
	}

	public void SetBlockGray(bool isGray)
	{
		spriteRenderer.material = ((!isGray) ? normal : grayScale);
		if (!isGray)
		{
			grayScale.SetFloat("_EffectAmount", 0f);
		}
	}

	public void UpdateGrayScale(float num)
	{
		grayScale.SetFloat("_EffectAmount", num);
	}
}
