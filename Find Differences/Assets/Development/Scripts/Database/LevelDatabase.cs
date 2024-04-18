using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelDatabase", menuName = "Database/LevelDatabase", order = 0)]
public class LevelDatabase : ScriptableObject
{
    [field: SerializeField] public List<LevelConfig> Configs { get; private set; }

    public int MaxCountLevel => Configs.Count;

    public string GetKeyScene(int id)
    {
        LevelConfig data = GetConfig(id);

        return data.Data.KEY;
    }

    public LevelData GetData(int id)
    {
        LevelConfig data = GetConfig(id);

        return data.Data;
    }

    public LevelConfig GetConfig(int id)
    {
        LevelConfig data = Configs.FirstOrDefault(x => x.Data.ID == id);

        if (data == null)
        {
            Debug.LogError($"Уровень с ID {id} не найден");
            return null;
        }

        return data;
    }
}
