public class StartGameData : CommandData
{
    public LevelData LevelData { get; private set; }

    public StartGameData(LevelData levelData) => LevelData = levelData;

}
