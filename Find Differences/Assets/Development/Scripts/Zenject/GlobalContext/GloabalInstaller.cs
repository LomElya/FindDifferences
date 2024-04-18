using Zenject;
using UnityEngine;

public class GloabalInstaller : MonoInstaller
{
    [SerializeField] private PurchaseManager _purchaseManager;
    [SerializeField] private AppodealManager _appodealManager;

    public override void InstallBindings()
    {
        BindSignals();

        BindLoadingOperation();
        BindSaveData();

        BindServices();
    }

    private void BindServices()
    {
        Container.Bind<Level>().AsSingle();
        Container.BindInterfacesAndSelfTo<Timer>().FromInstance(new Timer(this)).AsSingle();
        Container.BindInterfacesAndSelfTo<PurchaseManager>().FromInstance(_purchaseManager).AsSingle();
        Container.BindInterfacesAndSelfTo<AppodealManager>().FromInstance(_appodealManager).AsSingle();
    }

    private void BindLoadingOperation()
    {
        Container.BindInterfacesAndSelfTo<AssetProvider>().AsSingle();
        Container.BindInterfacesAndSelfTo<LoadingScreenProvider>().AsSingle();
    }

    private void BindSignals()
    {
        SignalBusInstaller.Install(Container);

        Container.DeclareSignal<GameWinSignal>().OptionalSubscriber();
        Container.DeclareSignal<GameQuitSignal>().OptionalSubscriber();
    }

    private void BindSaveData()
    {
        BindUserStateCommunicator();
        Container.Bind<PlayerContainer>().AsSingle();
    }

    private void BindUserStateCommunicator()
    {
        Container.BindInterfacesAndSelfTo<LocalStateCommunicator>().AsSingle();
    }
}
