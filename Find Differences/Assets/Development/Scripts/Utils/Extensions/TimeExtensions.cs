using UnityEngine;

public static class TimeExtensions
{
    public static string ConvertToMinutesString(this float seconds)
    {
        float minuts = Mathf.FloorToInt(seconds / 60);
        float _seconds = Mathf.FloorToInt(seconds % 60);

        string time = string.Format("{0:00} : {1:00}", minuts, _seconds);
        return time;
    }
}
