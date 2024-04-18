using Newtonsoft.Json;

public class PlayerLevelState
{
    public int ID { get; private set; }

    public bool IsLevelOpen { get; private set; }
    public bool Selected { get; private set; }

    public PlayerLevelState()
    {
        IsLevelOpen = false;
        Selected = false;
    }

    [JsonConstructor]
    public PlayerLevelState(int id, bool isLevelOpen, bool selected)
    {
        ID = id;
        IsLevelOpen = isLevelOpen;
        Selected = selected;
    }

    public void OpenLevel(bool isOpen) => IsLevelOpen = isOpen;
    public void SelectLevel(bool isSelect) => Selected = isSelect;
}
