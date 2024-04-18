public class PlayerContainer
{
    public PlayerState State { get; private set; }

    public void SetState(PlayerState state) => State = state;

    public void ChangeLevel(int id) => State.ChangeLevel(id);

    public int CurrentLevel() => State.CurrentLevelID;
    public bool IsOpenedLevel(int id) => State.Levels[id].IsLevelOpen;
    public bool IsSelectedLevel(int id) => State.Levels[id].Selected;
    public bool IsLastLevel(int id) => State.Levels.Count - 1 <= id;

    public void OpenLevel(int id) => State.OpenLevel(id);

    public int GetCounter() => State.LevelCounter;
    public void AddCounter() => State.AddCounter();
}
