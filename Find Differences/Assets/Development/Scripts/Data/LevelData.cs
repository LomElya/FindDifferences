using UnityEngine;

[System.Serializable]
public class LevelData
{
    [field: SerializeField] public int ID;
    [field: SerializeField] public string KEY = "Key";

    [field: SerializeField, Range(20, 600)] public float MaxTimeSeconds = 120f;
}