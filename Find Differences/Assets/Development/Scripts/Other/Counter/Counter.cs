using System;
public class Counter
{
    public event Action<int> CounterChange;

    private IStateCommunicator _stateCommunicator;
    private PlayerContainer _playerContainer;
    private PlayerState _playerState => _playerContainer.State;

    public Counter(IStateCommunicator stateCommunicator, PlayerContainer playerContainer)
    {
        _stateCommunicator = stateCommunicator;
        _playerContainer = playerContainer;
    }

    public void AddCounter()
    {
        _playerState.AddCounter();

        CounterChange?.Invoke(GetCounterValue());

        _stateCommunicator.SaveState(_playerState);
    }

    public int GetCounterValue() => _playerContainer.GetCounter();

}
