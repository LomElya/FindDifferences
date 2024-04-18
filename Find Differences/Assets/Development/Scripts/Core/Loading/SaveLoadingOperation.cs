using Cysharp.Threading.Tasks;
using System;

public sealed class SaveLoadingOperation : ILoadingOperation
{
    public string Description => "Save loading...";

    private readonly PlayerContainer _playerContainer;
    private readonly IStateCommunicator _stateCommunicator;

    private Action<float> _onProgress;

    public SaveLoadingOperation(PlayerContainer playerContainer, IStateCommunicator stateCommunicator)
    {
        _playerContainer = playerContainer;
        _stateCommunicator = stateCommunicator;
    }

    public async UniTask Load(Action<float> onProgress)
    {
        _onProgress = onProgress;

        _onProgress.Invoke(0.3f);

        _playerContainer.SetState(await GetState());

        onProgress?.Invoke(1f);
    }

    private async UniTask<PlayerState> GetState()
    {
        PlayerState result = await _stateCommunicator.GetState();

        if (_stateCommunicator.IsDataAlreadyExist() == false)
        {
            result = new PlayerState();
            await _stateCommunicator.SaveState(result);
        }

        _onProgress.Invoke(0.6f);

        return result;
    }
}