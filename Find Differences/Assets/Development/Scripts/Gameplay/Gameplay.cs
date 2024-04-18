using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.ResourceManagement.ResourceProviders;
using Zenject;

public class Gameplay : MonoBehaviour, IGameModeCleaner
{
    public string SceneName => Constants.Scenes.GAMESCENE;

    private GameState _currentGameState;
    private GameStateFabric _fabric;

    private AssetProvider _assetProvider;

    private Dictionary<GameStateType, GameState> _states = new();

    public SceneInstance GameScene { get; private set; }

    [Inject]
    private void Construct(GameStateFabric fabric, AssetProvider assetProvider)
    {
        _fabric = fabric;
        _assetProvider = assetProvider;
    }

    public async UniTask Init()
    {
        _currentGameState = _fabric.CreateGameState(GameStateType.GameStart);
        _states.Add(GameStateType.GameStart, _currentGameState);

        await _currentGameState.OnEnter();
    }

    public async UniTask SetState(GameStateType gameStateType, string description = "")
    {
        if (!_currentGameState.CanSwitchToState(gameStateType))
            return;

        GameState newGameState;

        if (_states.ContainsKey(gameStateType))
            newGameState = _states[gameStateType];
        else
        {
            newGameState = _fabric.CreateGameState(gameStateType);
            _states.Add(gameStateType, newGameState);
        }

        _currentGameState = newGameState;

        await newGameState.OnEnter(description);
    }

    public async UniTask SetGameScene(string nameScene) => GameScene = await _assetProvider.LoadSceneAdditive(nameScene);
}
