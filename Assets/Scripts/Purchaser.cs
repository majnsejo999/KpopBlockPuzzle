using BlockGame.Nova.Conf;
using BlockGame.New.Core;
using BlockGame.New.Core.UI;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Security;
using UnityEngine.SceneManagement;
public class Purchaser : MonoBehaviour, IStoreListener
{
    private static IStoreController m_StoreController;

    private static IExtensionProvider m_StoreExtensionProvider;

    private IAPItem[] iapItems;

    private static Purchaser instance;

    public static Purchaser Instance => instance;
    
    private void Awake()
    {
        instance = this;
    }

    public void Init()
    {
        if (m_StoreController == null)
        {
            InitializePurchasing();
        }
    }

    private void Start()
    {
    }

    public void InitializePurchasing()
    {
        if (!IsInitialized())
        {
            ConfigurationBuilder configurationBuilder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
            iapItems = new IAPItem[4];
            iapItems[0] = new IAPItem();
            iapItems[0].productID = "noads";
            iapItems[0].productType = ProductType.NonConsumable;
            iapItems[0].coinNum = 0;
            iapItems[0].price = 2.99f;
            iapItems[0].itemType = "noads";
            configurationBuilder.AddProduct(iapItems[0].productID, iapItems[0].productType);
            iapItems[1] = new IAPItem();
            iapItems[1].productID = "itemrotate1";
            iapItems[1].productType = ProductType.Consumable;
            iapItems[1].coinNum = 0;
            iapItems[1].price = 1.99f;
            iapItems[1].itemType = "itemrotate";
            configurationBuilder.AddProduct(iapItems[1].productID, iapItems[1].productType);
            iapItems[2] = new IAPItem();
            iapItems[2].productID = "itemrotate2";
            iapItems[2].productType = ProductType.Consumable;
            iapItems[2].coinNum = 0;
            iapItems[2].price = 6.99f;
            iapItems[2].itemType = "itemrotate";
            configurationBuilder.AddProduct(iapItems[2].productID, iapItems[2].productType);
            iapItems[3] = new IAPItem();
            iapItems[3].productID = "itemrotate3";
            iapItems[3].productType = ProductType.Consumable;
            iapItems[3].coinNum = 0;
            iapItems[3].price = 14.99f;
            iapItems[3].itemType = "itemrotate";
            configurationBuilder.AddProduct(iapItems[2].productID, iapItems[2].productType);
            IAPItem[] array = iapItems;
            foreach (IAPItem iAPItem in array)
            {
                configurationBuilder.AddProduct(iAPItem.productID, iAPItem.productType);
            }
            UnityPurchasing.Initialize(this, configurationBuilder);
        }
    }

    private bool IsInitialized()
    {
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }

    public void BuyProduct(int index)
    {
        UnityEngine.Debug.Log("buy product " + iapItems[index].productID);
        BuyProductID(iapItems[index].productID);
    }

    public void FakeProcessPurchase(string productID)
    {
        IAPItem iAPItem = null;
        IAPItem[] array = iapItems;
        foreach (IAPItem iAPItem2 in array)
        {
            if (string.Equals(productID, iAPItem2.productID, StringComparison.Ordinal))
            {
                iAPItem = iAPItem2;
            }
        }
        if (iAPItem.itemType == "noads")
        {
            UserDataManager.Instance.GetService().RemoveAdPurchased = true;
            //AdsControl.instance.DestroyBanner();
            DialogManager.Instance.ShowDialog("InfoDlg");
            InfoDlg.Instance.UpdateInfo(LanguageManager.GetString("#buy_success_desc_1"));
            MainSceneUIManager.Instance.BtnNoAds.gameObject.SetActive(false);
            GameWinDlg.Instance.UpdateUI();
        }
        else if (iAPItem.itemType == "itemrotate")
        {
            UserDataManager.Instance.GetService().countRota += iAPItem.coinNum;
            DialogManager.Instance.ShowDialog("InfoDlg");
            InfoDlg.Instance.UpdateInfo(LanguageManager.GetString("#buy_success_desc_2"));
            if(SceneManager.GetActiveScene().name == "GameScene")
            {
                GameSceneUIManager.Instance.txt_countRota.text = UserDataManager.Instance.GetService().countRota.ToString();
            }
        }
        UserDataManager.Instance.Save();
        AudioManager.Instance.PlayAudioEffect("purchase_success");
        GlobalVariables.Purchasing = false;
    }

    private void BuyProductID(string productId)
    {
        UnityEngine.Debug.Log("buy product id " + productId);
        if (IsInitialized())
        {
            Product product = m_StoreController.products.WithID(productId);
            if (product != null && product.availableToPurchase)
            {
                UnityEngine.Debug.Log($"Purchasing product asychronously: '{product.definition.id}'");
                GlobalVariables.ResumeFromDesktop = false;
                GlobalVariables.Purchasing = true;
                m_StoreController.InitiatePurchase(product);
                return;
            }
            UnityEngine.Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            if (MaskDlg.Instance != null)
            {
                MaskDlg.Instance.Disable();
            }
            InfoDlg.Instance.UpdateInfo(LanguageManager.GetString("#buy_fail_desc"));
            InfoDlg.Instance.Show();
        }
        else
        {
            UnityEngine.Debug.Log("BuyProductID FAIL. Not initialized.");
            if (MaskDlg.Instance != null)
            {
                MaskDlg.Instance.Disable();
            }
            InfoDlg.Instance.UpdateInfo(LanguageManager.GetString("#buy_fail_desc"));
            InfoDlg.Instance.Show();
        }
    }

    public void RestorePurchases()
    {
        if (!IsInitialized())
        {
            UnityEngine.Debug.Log("RestorePurchases FAIL. Not initialized.");
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.OSXPlayer)
        {
            UnityEngine.Debug.Log("RestorePurchases started ...");
            IAppleExtensions extension = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
            extension.RestoreTransactions(delegate (bool result)
            {
                MaskDlg.Instance.Disable();
                UnityEngine.Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
                if (!result)
                {
                }
            });
        }
        else
        {
            UnityEngine.Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
        }
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        UnityEngine.Debug.Log("OnInitialized: PASS");
        m_StoreController = controller;
        m_StoreExtensionProvider = extensions;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        UnityEngine.Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }
    public string getPrice(string productId)
    {
        if (IsInitialized())
        {
            Product product = m_StoreController.products.WithID(productId);
            if (product != null)
            {
                return product.metadata.localizedPriceString;
            }
            return null;
        }
        else
        {
            return null;
        }
    }
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        if (MaskDlg.Instance != null)
        {
            MaskDlg.Instance.Disable();
        }
        bool flag = true;

        try
        {
            //var validator = new CrossPlatformValidator(GooglePlayTangle.Data(),
            //             AppleTangle.Data(), Application.identifier);
            //IPurchaseReceipt[] array = validator.Validate(args.purchasedProduct.receipt);
            //UnityEngine.Debug.Log("Receipt is valid. Contents:");
            //IPurchaseReceipt[] array2 = array;
            //foreach (IPurchaseReceipt purchaseReceipt in array2)
            //{
            //    UnityEngine.Debug.Log(purchaseReceipt.productID);
            //    UnityEngine.Debug.Log(purchaseReceipt.purchaseDate);
            //    UnityEngine.Debug.Log(purchaseReceipt.transactionID);
            //}
        }
        catch (IAPSecurityException)
        {
            UnityEngine.Debug.Log("Invalid receipt, not unlocking content");
            flag = false;
        }
        if (flag && GlobalVariables.Purchasing)
        {
            UnityEngine.Debug.Log("validPurchase success");
            IAPItem iAPItem = null;
            IAPItem[] array3 = iapItems;
            foreach (IAPItem iAPItem2 in array3)
            {
                if (string.Equals(args.purchasedProduct.definition.id, iAPItem2.productID, StringComparison.Ordinal))
                {
                    UnityEngine.Debug.Log($"ProcessPurchase: PASS. Product: '{args.purchasedProduct.definition.id}'");
                    iAPItem = iAPItem2;
                }
            }
            if (iAPItem.productType == ProductType.Consumable)
            {
                if (iAPItem.itemType == "noads")
                {
                    UserDataManager.Instance.GetService().RemoveAdPurchased = true;
                   // AdsControl.instance.DestroyBanner();
                    DialogManager.Instance.ShowDialog("InfoDlg");
                    InfoDlg.Instance.UpdateInfo(LanguageManager.GetString("#buy_success_desc_1"));
                    MainSceneUIManager.Instance.BtnNoAds.gameObject.SetActive(false);
                    GameWinDlg.Instance.UpdateUI();
                }
                else if (iAPItem.itemType == "itemrotate")
                {
                    UserDataManager.Instance.GetService().countRota += iAPItem.coinNum;
                    DialogManager.Instance.ShowDialog("InfoDlg");
                    InfoDlg.Instance.UpdateInfo(LanguageManager.GetString("#buy_success_desc_2"));
                }
            }
            else if (iAPItem.productType == ProductType.NonConsumable)
            {
                if (iAPItem.itemType == "noads")
                {
                    UserDataManager.Instance.GetService().RemoveAdPurchased = true;
                  // AdsControl.instance.DestroyBanner();
                    DialogManager.Instance.ShowDialog("InfoDlg");
                    InfoDlg.Instance.UpdateInfo(LanguageManager.GetString("#buy_success_desc_1"));
                    MainSceneUIManager.Instance.BtnNoAds.gameObject.SetActive(false);
                    GameWinDlg.Instance.UpdateUI();
                }
                else if (iAPItem.itemType == "itemrotate")
                {
                    UserDataManager.Instance.GetService().countRota += iAPItem.coinNum;
                    DialogManager.Instance.ShowDialog("InfoDlg");
                    InfoDlg.Instance.UpdateInfo(LanguageManager.GetString("#buy_success_desc_2"));
                }
            }
            else if (iAPItem.productType != ProductType.Subscription)
            {
            }
        }
        else
        {
            UnityEngine.Debug.Log("validPurchase failed");
        }
        Timer.Schedule(this, 60f, delegate
        {
            GlobalVariables.Purchasing = false;
        });
        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        UnityEngine.Debug.Log($"OnPurchaseFailed: FAIL. Product: '{product.definition.storeSpecificId}', PurchaseFailureReason: {failureReason}");
        if (MaskDlg.Instance != null)
        {
            MaskDlg.Instance.Disable();
        }
        GlobalVariables.Purchasing = false;
        InfoDlg.Instance.UpdateInfo(LanguageManager.GetString("#buy_fail_desc"));
        InfoDlg.Instance.Show();
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        
    }
}
