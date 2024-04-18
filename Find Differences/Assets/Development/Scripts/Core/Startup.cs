using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Startup : MonoBehaviour
{
    private AssetProvider _assetProvider;
    private LoadingScreenProvider _loadingProvider;

    private PlayerContainer _playerContainer;
    private IStateCommunicator _stateCommunicator;

    [Inject]
    private void Construct(AssetProvider assetProvider, LoadingScreenProvider loadingProvider, PlayerContainer playerContainer, IStateCommunicator stateCommunicator)
    {
        _assetProvider = assetProvider;
        _loadingProvider = loadingProvider;

        _playerContainer = playerContainer;
        _stateCommunicator = stateCommunicator;
    }

    private void Awake() => OnLoadGameScene();

    private async void OnLoadGameScene()
    {
        var loadingOperations = new Queue<ILoadingOperation>();

        loadingOperations.Enqueue(_assetProvider);
        loadingOperations.Enqueue(new SaveLoadingOperation(_playerContainer, _stateCommunicator));
        loadingOperations.Enqueue(new MenuLoadingOperation());

        await _loadingProvider.LoadAndDestroy(loadingOperations);
    }
}
