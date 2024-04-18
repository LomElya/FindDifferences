using System.Collections.Generic;
using Zenject;
using Cysharp.Threading.Tasks;

public class GameStateStart : GameState
{
    private GameStateManager _gameStateManager;

    private LevelDatabase _levelDatabase;
    private Level _level;

    public GameStateStart(GameStateType gameStateType) : base(gameStateType)
    {
    }

    [Inject]
    private void Construct(GameStateManager gameStateManager, LevelDatabase levelDatabase, Level level)
    {
        _gameStateManager = gameStateManager;

        _level = level;

        _levelDatabase = levelDatabase;
    }

    public override bool CanSwitchToState(GameStateType gameStateType) => true;

    public override async UniTask OnEnter(string description = "")
    {
        int idCurrentLevel = _level.GetCurrentLevelID();
        LevelData levelData = _levelDatabase.GetData(idCurrentLevel);

        var command1 = _gameStateManager.CreateStartGameCommand(levelData);
        var command2 = _gameStateManager.CreateSetGameStateCommand(GameStateType.GameInProgress);

        var commands = new List<Command>()
        {
            command1,
            command2
        };

        await _gameStateManager.ExecuteCommands(commands);
    }
}
