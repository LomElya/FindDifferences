using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Purchasing;
using Zenject;

public sealed class GameProcessor : MonoBehaviour
{
    [SerializeField] private LevelManager _levelManager;
    [SerializeField] private ImageButton _buttonExit;

    private Timer _timer;
    private SignalBus _signalBus;

    private UniTaskCompletionSource _startCompletion;
    private UniTaskCompletionSource _stopCompletion;

    [Inject]
    private void Construct(Timer timer, SignalBus signalBus)
    {
        _timer = timer;
        _signalBus = signalBus;
    }

    public UniTask StartGame(LevelData levelData)
    {
        _startCompletion = new UniTaskCompletionSource();
        Subscribe();

        _timer.Stop();
        _timer.Start(levelData.MaxTimeSeconds);

        _levelManager.Init(levelData);

        _startCompletion?.TrySetResult();
        return _startCompletion.Task;
    }

    public UniTask EndGame()
    {
        _stopCompletion = new UniTaskCompletionSource();
        Unsubscribe();

        _timer.Stop();

        _levelManager.Dispose();

        _stopCompletion?.TrySetResult();
        return _stopCompletion.Task;
    }

    private void OnClickButtonExit() => _signalBus.Fire(new GameQuitSignal());

    private void OnSuccessConsumable(PurchaseEventArgs args) => OnPurchaseConsumableTest();
    private void OnPurchaseConsumableTest()
    {
        Alert.Instance.ShowAlert("Куплено 10с");
        _timer.AddTime(10f);
    }

    private void Subscribe()
    {
        _buttonExit.Click += OnClickButtonExit;
        PurchaseManager.OnPurchaseConsumableTest += OnPurchaseConsumableTest;
    }

    private void Unsubscribe()
    {
        _buttonExit.Click -= OnClickButtonExit;
        PurchaseManager.OnPurchaseConsumableTest -= OnPurchaseConsumableTest;
    }

    private void OnDestroy() => Unsubscribe();
}
