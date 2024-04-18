using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class LocalAssetLoader
{
    private GameObject _cachedObject;

    public async UniTask<T> Load<T>(string assetId, Transform parent = null)
    {
        var handle = Addressables.InstantiateAsync(assetId, parent);
        _cachedObject = await handle.Task;

        if (_cachedObject.TryGetComponent(out T component) == false)
            throw new NullReferenceException($"Объект типа  {typeof(T)} имеет значение null при попытке загрузить его из addressables");

        return component;
    }

    public async UniTask<Disposable<T>> LoadDisposable<T>(string assetId, Transform parent = null)
    {
        var component = await Load<T>(assetId, parent);

        return Disposable<T>.Borrow(component, _ => Unload());
    }

    public void Unload()
    {
        if (_cachedObject == null)
            return;

        _cachedObject.SetActive(false);
        Addressables.ReleaseInstance(_cachedObject);
        _cachedObject = null;
    }
}
