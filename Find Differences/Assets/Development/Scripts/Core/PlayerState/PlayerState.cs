using System.Collections.Generic;
using Newtonsoft.Json;

public class PlayerState
{
    public List<PlayerLevelState> Levels { get; private set; }

    public int CurrentLevelID { get; private set; }
    public int LevelCounter { get; private set; }

    public PlayerState()
    {
        Levels = new();
        CurrentLevelID = 0;
        LevelCounter = 0;
    }

    [JsonConstructor]
    public PlayerState(List<PlayerLevelState> levels, int currentLevelID, int levelCounter)
    {
        Levels = new(levels);
        CurrentLevelID = currentLevelID;
        LevelCounter = levelCounter;
    }

    public void ChangeLevel(int id)
    {
        CurrentLevelID = id;

        foreach (var level in Levels)
            level.SelectLevel(false);

        Levels[CurrentLevelID].SelectLevel(true);
    }

    public void OpenLevel(int id) => Levels[id].OpenLevel(true);

    public void AddCounter() => LevelCounter++;
}
