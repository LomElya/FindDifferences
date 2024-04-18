using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private Gameplay _gameplay;
    [SerializeField] private GameProcessor _gameProcessor;

    public override void InstallBindings()
    {
        BindGamePlay();

        Container.Bind<Counter>().AsSingle();
    }

    private void BindGamePlay()
    {
        Container.BindInstance(_gameProcessor);

        Container.BindInterfacesAndSelfTo<Gameplay>().FromInstance(_gameplay).AsSingle();

        var gameStateFabric = new GameStateFabric();
        Container.BindInstance(gameStateFabric);
        Container.QueueForInject(gameStateFabric);

        var turnManager = new GameStateManager(this);
        Container.Bind<GameStateManager>().FromInstance(turnManager).AsSingle();
        Container.QueueForInject(turnManager);
    }
}
