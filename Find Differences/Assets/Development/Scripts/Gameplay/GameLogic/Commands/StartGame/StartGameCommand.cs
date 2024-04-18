using System;
using Cysharp.Threading.Tasks;
using Zenject;


public class StartGameCommand : Command
{
    private GameProcessor _processor;

    public StartGameCommand(StartGameData data) : base(data)
    {
    }

    [Inject]
    private void Construct(GameProcessor processor)
    {
        _processor = processor;
    }

    public override async UniTask Execute(Action onCompleted)
    {
        StartGameData startGameData = (StartGameData)_commandData;
        LevelData levelData = startGameData.LevelData;

        await _processor.StartGame(levelData);

        onCompleted?.Invoke();
    }
}
