public class SetGameStateData : CommandData
{
    public GameStateType GameStateType { get; private set; }
    public string Descriptiom { get; private set; }

    public SetGameStateData(GameStateType gameStateType, string description = "")
    {
        GameStateType = gameStateType;
        Descriptiom = description;
    }
}
