using System;
using Cysharp.Threading.Tasks;
using Zenject;

public class SetGameStateCommand : Command
{
    private Gameplay _gameplay;

    [Inject]
    public void Construct(Gameplay gameplay)
    {
        _gameplay = gameplay;
    }

    public SetGameStateCommand(SetGameStateData data) : base(data)
    {
    }

    public override async UniTask Execute(Action onCompleted)
    {
        var setGameStateData = (SetGameStateData)_commandData;

        GameStateType stateType = setGameStateData.GameStateType;
        string description = setGameStateData.Descriptiom;

        await _gameplay.SetState(stateType, description);
        onCompleted?.Invoke();
    }
}