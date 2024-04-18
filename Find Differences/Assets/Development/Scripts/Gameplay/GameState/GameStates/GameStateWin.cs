using System.Collections.Generic;
using Zenject;
using Cysharp.Threading.Tasks;

public class GameStateWin : GameState
{
    private GameStateManager _gameStateManager;

    private LevelDatabase _levelDatabase;
    private Level _level;
    private Counter _counter;
    private AppodealManager _appodealManager;

    private Timer _timer;

    public GameStateWin(GameStateType gameStateType) : base(gameStateType)
    {
    }

    [Inject]
    private void Construct(GameStateManager gameStateManager, LevelDatabase levelDatabase, Level level, Timer timer, Counter counter, AppodealManager appodealManager)
    {
        _gameStateManager = gameStateManager;

        _levelDatabase = levelDatabase;
        _level = level;
        _counter = counter;

        _timer = timer;
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

        await resultMenu.Value.Show(GameResultType.Win, OnRestartLevel, OnQuit, description);

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
