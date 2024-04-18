using System;
using Cysharp.Threading.Tasks;
using Zenject;

public class QuitGameCommand : Command
{
    private LoadingScreenProvider _loadingProvider;
    private Gameplay _gameplay;
    private GameProcessor _processor;

    public QuitGameCommand(QuitGameData data) : base(data)
    {
    }

    [Inject]
    private void Construct(LoadingScreenProvider loadingProvider, Gameplay gameplay, GameProcessor processor)
    {
        _loadingProvider = loadingProvider;
        _gameplay = gameplay;
        _processor = processor;
    }

    public override async UniTask Execute(Action onCompleted)
    {
        await _processor.EndGame();

        await _loadingProvider.LoadAndDestroy(new ClearGameOperation(_gameplay, _loadingProvider));

        onCompleted?.Invoke();
    }
}