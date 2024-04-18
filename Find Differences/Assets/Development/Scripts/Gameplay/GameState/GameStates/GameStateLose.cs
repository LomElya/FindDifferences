using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Zenject;

public class GameStateLose : GameState
{
    private GameStateManager _gameStateManager;

    private Counter _counter;
    private AppodealManager _appodealManager;

    public GameStateLose(GameStateType gameStateType) : base(gameStateType)
    {
    }

    [Inject]
    private void Construct(GameStateManager gameStateManager, Counter counter, AppodealManager appodealManager)
    {
        _gameStateManager = gameStateManager;
        _counter = counter;
        _appodealManager = appodealManager;
    }

    public override bool CanSwitchToState(GameStateType gameStateType) => true;

    public override async UniTask OnEnter(string description = "")
    {
        await OpenResultMenu(description);
    }

    private async UniTask OpenResultMenu(string description)
    {
        var resultMenu = await GameResult.Load();

        await resultMenu.Value.Show(GameResultType.Lose, OnRestartLevel, OnQuit, description);

        resultMenu.Dispose();
    }

    private async void OnRestartLevel()
    {
        _counter.AddCounter();
        _appodealManager.ShowInterstatiAds();

        var command = _gameStateManager.CreateClearGameLevelCommand();
        var command1 = _gameStateManager.CreateSetGameStateCommand(GameStateType.GameStart);

        var commands = new List<Command>()
            {
                command,
                command1,
            };

        await _gameStateManager.ExecuteCommands(commands);
    }

    private async void OnQuit()
    {
        var command = _gameStateManager.CreateClearGameLevelCommand();
        var command1 = _gameStateManager.CreateQuitGameCommand();

        var commands = new List<Command>()
            {
                command,
                command1,
            };

        await _gameStateManager.ExecuteCommands(commands);
    }
}
