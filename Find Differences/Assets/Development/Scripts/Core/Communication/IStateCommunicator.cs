using Cysharp.Threading.Tasks;

public interface IStateCommunicator
{
    UniTask<bool> SaveState(PlayerState state);
    UniTask<PlayerState> GetState();

    bool IsDataAlreadyExist();
}