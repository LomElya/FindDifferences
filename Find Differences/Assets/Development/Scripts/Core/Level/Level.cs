using System;

public class Level
{
    public event Action<int> LevelChange;

    private IStateCommunicator _stateCommunicator;
    private PlayerContainer _playerContainer;
    private PlayerState _playerState => _playerContainer.State;

    public Level(IStateCommunicator stateCommunicator, PlayerContainer playerContainer)
    {
        _stateCommunicator = stateCommunicator;
        _playerContainer = playerContainer;
    }

    public void ChangeLevel(int id)
    {
        if (GetCurrentLevelID() == id)
            return;

        LevelChange?.Invoke(id);

        _playerState.ChangeLevel(id);
        _stateCommunicator.SaveState(_playerState);
    }

    public void NextLevel()
    {
        if (IsLastLevel())
            return;

        int nextLevel = GetCurrentLevelID() + 1;

        if (!_playerContainer.IsOpenedLevel(nextLevel))
            _playerContainer.OpenLevel(nextLevel);

        ChangeLevel(nextLevel);

        LevelChange?.Invoke(nextLevel);
    }

    public void OpenLevel()
    {
        if (IsLastLevel())
            return;

        int nextLevel = GetCurrentLevelID() + 1;

        if (!_playerContainer.IsOpenedLevel(nextLevel))
            _playerContainer.OpenLevel(nextLevel);
    }

    public int GetCurrentLevelID() => _playerContainer.CurrentLevel();
    public bool IsLastLevel() => _playerContainer.IsLastLevel(GetCurrentLevelID());
}
