using Cysharp.Threading.Tasks;
using UnityEngine;

public static class LoadExtencions
{
    public static UniTask<Disposable<LevelView>> LoadDisposableLevel(string key, Transform parent = null)
    {
        var assetLoader = new LocalAssetLoader();
        return assetLoader.LoadDisposable<LevelView>(key, parent);
    }

     public static UniTask<LevelView> LoadLevel(string key, Transform parent = null)
    {
        var assetLoader = new LocalAssetLoader();
        return assetLoader.Load<LevelView>(key, parent);
    }
}
