using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

public class ClearGameOperation : ILoadingOperation
{
    public string Description => "Clearing game...";

    private readonly IGameModeCleaner _gameCleanUp;
    private LoadingScreenProvider _loadingProvider;

    public ClearGameOperation(IGameModeCleaner gameCleanUp, LoadingScreenProvider loadingProvider)
    {
        _gameCleanUp = gameCleanUp;
        _loadingProvider = loadingProvider;
    }

    public async UniTask Load(Action<float> onProgress)
    {
        onProgress?.Invoke(0.2f);

        onProgress?.Invoke(0.5f);

        await LoadMenuOperation();

        onProgress?.Invoke(0.75f);

        await UnLoadSceneOperation();

        onProgress?.Invoke(1f);
    }

    private async UniTask LoadMenuOperation()
    {
        await _loadingProvider.LoadAndDestroy(new MenuLoadingOperation());
    }

    private async UniTask UnLoadSceneOperation()
    {
        var unloadOperation = SceneManager.UnloadSceneAsync(_gameCleanUp.SceneName);

        while (unloadOperation.isDone == false)
            await UniTask.Yield();
    }
}
