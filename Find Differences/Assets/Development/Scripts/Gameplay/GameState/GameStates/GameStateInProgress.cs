using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Zenject;

public class GameStateInProgress : GameState, IDisposable
{
    private GameStateManager _gameStateManager;

    private Timer _timer;

    private bool _isPaused = false;

    private string _description = "";
    private SignalBus _signalBus;

    public GameStateInProgress(GameStateType gameStateType) : base(gameStateType)
    {
    }

    public override bool CanSwitchToState(GameStateType gameStateType) => true;

    [Inject]
    private void Construct(GameStateManager gameStateManager, SignalBus signalBus, Timer timer)
    {
        _gameStateManager = gameStateManager;
        _timer = timer;

        _signalBus = signalBus;
    }

    public override async UniTask OnEnter(string description = "")
    {
        Subscribe();
    }

    private async void OnLevelPassed(GameWinSignal signal)
    {
        _description = $"Уровень пройден за {_timer.PassedSeconds} секунд";
        await WinGame();
    }

    private async void OnQuit(GameQuitSignal signal)
    {
        await QuitGame();
    }

    private async void OnTimeOver()
    {
        _description = "Время вышло";
        await LoseGame();
    }

    private async UniTask WinGame()
    {
        Unsubscribe();

        var command = _gameStateManager.CreateClearGameLevelCommand();
        var command1 = _gameStateManager.CreateSetGameStateCommand(GameStateType.Win, _description);

        var commands = new List<Command>()
            {
                command,
                command1,
            };

        await _gameStateManager.ExecuteCommands(commands);
    }

    private async UniTask LoseGame()
    {
        Unsubscribe();

        var command = _gameStateManager.CreateClearGameLevelCommand();
        var command1 = _gameStateManager.CreateSetGameStateCommand(GameStateType.Lose, _description);

        var commands = new List<Command>()
            {
                command,
                command1,
            };

        await _gameStateManager.ExecuteCommands(commands);
    }

    private async UniTask QuitGame()
    {
        Unsubscribe();

        var command = _gameStateManager.CreateClearGameLevelCommand();
        var command1 = _gameStateManager.CreateQuitGameCommand();

        var commands = new List<Command>()
            {
                command,
                command1,
            };

        await _gameStateManager.ExecuteCommands(commands);
    }

    private void Subscribe()
    {
        _timer.TimeFinished += OnTimeOver;

        _signalBus.Subscribe<GameWinSignal>(OnLevelPassed);
        _signalBus.Subscribe<GameQuitSignal>(OnQuit);
    }

    private void Unsubscribe()
    {
        _timer.TimeFinished -= OnTimeOver;

        _signalBus.Unsubscribe<GameWinSignal>(OnLevelPassed);
        _signalBus.Unsubscribe<GameQuitSignal>(OnQuit);
    }

    public void Dispose() => Unsubscribe();
}
