using System;
using Cysharp.Threading.Tasks;
using Zenject;

public class ClearGameLevelCommand : Command
{
    private GameProcessor _gameProcessor;

    public ClearGameLevelCommand(ClearGameLevelData data) : base(data)
    {
    }

    [Inject]
    private void Construct(GameProcessor gameProcessor)
    {
        _gameProcessor = gameProcessor;
    }

    public override async UniTask Execute(Action onCompleted)
    {
        await _gameProcessor.EndGame();

        onCompleted?.Invoke();
    }
}
