using UnityEngine;


[CreateAssetMenu(fileName = "LevelConfig", menuName = "Config/LevelConfig")]
public class LevelConfig : ScriptableObject
{
    [field: SerializeField] public LevelData Data;
}
