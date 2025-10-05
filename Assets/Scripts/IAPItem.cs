using System;
using UnityEngine.Purchasing;

[Serializable]
public class IAPItem
{
	public string productID;

	public ProductType productType;

	public string itemType;

	public int coinNum;

	public float price;
}
