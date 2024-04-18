using System;
using Cysharp.Threading.Tasks;

public abstract class Command
{
    protected CommandData _commandData;

    public Command(CommandData data)
    {
        _commandData = data;
    }

    public abstract UniTask Execute(Action onCompleted);
}
