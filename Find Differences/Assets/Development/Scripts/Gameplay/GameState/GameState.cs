using Cysharp.Threading.Tasks;

public abstract class GameState
{
    protected UniTaskCompletionSource _taskCompletion;

    public GameState(GameStateType gameStateType) => GameStateType = gameStateType;

    public GameStateType GameStateType { get; private set; }

    public abstract UniTask OnEnter(string description = "");
    public abstract bool CanSwitchToState(GameStateType gameStateType);
}
