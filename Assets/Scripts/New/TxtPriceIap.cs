using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
public class TxtPriceIap : MonoBehaviour
{
    [SerializeField]
    Text txtPrices;
    string priceCountry;
    public string idProduct;
    private void OnEnable()
    {
        priceCountry = Purchaser.Instance.getPrice(idProduct);
        if (!string.IsNullOrEmpty(priceCountry))
        {
            txtPrices.text = priceCountry;
        }
    }
}
