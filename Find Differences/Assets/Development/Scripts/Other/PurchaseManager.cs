using System;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

public class PurchaseManager : MonoBehaviour, IDetailedStoreListener
{
    public delegate void OnSuccessConsumable(PurchaseEventArgs args);
    public delegate void OnSuccessConsumableTest();
    public delegate void OnFailedPurchaseroduct(Product product, PurchaseFailureReason failureReason);
    public delegate void OnSuccessNonConsumable(PurchaseEventArgs args);

    [field: SerializeField] public string[] NonConsumableProducts { get; private set; }
    [field: SerializeField] public string[] ConsumableProducts { get; private set; }

    private static IStoreController _storeController;
    private static IExtensionProvider _storeExtensionProvider;

    public static event OnSuccessConsumable OnPurchaseConsumable;
    public static event OnSuccessConsumableTest OnPurchaseConsumableTest;
    public static event OnSuccessNonConsumable OnPurchaseNonConsumable;
    public static event OnFailedPurchaseroduct PurchaseFailed;

    private int _currentProductIndex;

    private void Awake()
    {
        InitializePurchasing();
    }

    public static bool CheckBuyState(string id)
    {
        Product product = _storeController.products.WithID(id);

        if (product.hasReceipt)
            return true;
        else
            return false;
    }

    public void InitializePurchasing()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        foreach (string s in ConsumableProducts)
            builder.AddProduct(s, ProductType.Consumable);

        foreach (string s in NonConsumableProducts)
            builder.AddProduct(s, ProductType.NonConsumable);

        UnityPurchasing.Initialize(this, builder);
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        _storeController = controller;
        _storeExtensionProvider = extensions;
    }

    public void BuyConsumable(string id)
    {

        OnPurchaseConsumableTest();
        for (int i = 0; i < ConsumableProducts.Length; i++)
            if (ConsumableProducts[i] == id)
            {
                BuyConsumable(i);
                return;
            }
    }

    public void BuyConsumable(int index)
    {
        _currentProductIndex = index;
        BuyProductID(ConsumableProducts[index]);
    }

    public void BuyNonConsumable(int index)
    {
        _currentProductIndex = index;
        BuyProductID(NonConsumableProducts[index]);
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        if (ConsumableProducts.Length > 0 && String.Equals(args.purchasedProduct.definition.id, ConsumableProducts[_currentProductIndex], StringComparison.Ordinal))
            OnSuccessC(args);
        else if (NonConsumableProducts.Length > 0 && String.Equals(args.purchasedProduct.definition.id, NonConsumableProducts[_currentProductIndex], StringComparison.Ordinal))
            OnSuccessNC(args);
        else
            Debug.LogError($"Процесс покупки: ОШИБКА. Неизвестный продукт:'{args.purchasedProduct.definition.id}'");

        return PurchaseProcessingResult.Complete;
    }

    private void BuyProductID(string productId)
    {
        if (!IsInitialized())
            return;

        Product product = _storeController.products.WithID(productId);

        if (product != null && product.availableToPurchase)
        {
            _storeController.InitiatePurchase(product);
        }
        else
        {
            Debug.LogError("BuyProductID: FAIL. Товар не приобретен, либо не найден, либо недоступен для покупки.");
            OnPurchaseFailed(product, PurchaseFailureReason.ProductUnavailable);
        }

    }

    protected virtual void OnSuccessC(PurchaseEventArgs args)
    {
        if (OnPurchaseConsumable != null)
            OnPurchaseConsumable(args);

        Debug.Log($"{ConsumableProducts[_currentProductIndex]}  Куплено!");
    }

    protected virtual void OnSuccessNC(PurchaseEventArgs args)
    {
        if (OnPurchaseNonConsumable != null)
            OnPurchaseNonConsumable(args);

        Debug.Log($"{NonConsumableProducts[_currentProductIndex]}  Куплено!");
    }

    protected virtual void OnFailedProduct(Product product, PurchaseFailureReason failureReason)
    {
        if (PurchaseFailed != null)
            PurchaseFailed(product, failureReason);

        Debug.LogError($"OnPurchaseFailed: ОШИБКА. Продукт: '{product.definition.storeSpecificId}', Причина сбоя: {failureReason}");
    }

    private bool IsInitialized() => _storeController != null && _storeExtensionProvider != null;

    public void OnInitializeFailed(InitializationFailureReason error) => Debug.LogError($"OnInitializeFailed InitializationFailureReason: {error}");
    public void OnInitializeFailed(InitializationFailureReason error, string message) => Debug.LogError($"OnInitializeFailed InitializationFailureReason: {error} {message}");
    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason) => OnFailedProduct(product, failureReason);
    public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription) => OnFailedProduct(product, failureDescription.reason);
}
