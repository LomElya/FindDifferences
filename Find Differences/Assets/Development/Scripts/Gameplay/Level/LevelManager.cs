using UnityEngine;
using Zenject;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Transform _parent;

    private Disposable<LevelView> _level;

    private SignalBus _signalBus;

    [Inject]
    private void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    public async void Init(LevelData levelData)
    {
        _level?.Dispose();
        _level = await LoadExtencions.LoadDisposableLevel(levelData.KEY, _parent);

        bool isFinished = await _level.Value.Init();

        if (isFinished)
            WinGame();
    }

    public void Dispose() => _level.Dispose();
    private void WinGame() => _signalBus.Fire(new GameWinSignal());
}
