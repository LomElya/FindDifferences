using UnityEngine;

public sealed class Constants
{
    public sealed class Scenes
    {
        public const string STARTUP = "Startup";
        public const string MAIN_MENU = "MainMenu";
        public const string GAMESCENE = "GameScene";
    }

    public sealed class MainColor
    {
        public static Color NormalColor = new Color(1f, 1f, 1f, 1f);
        public static Color HighlightColor = new Color(0.6156863f, 0.6156863f, 0.6156863f, 1f);
        public static Color PressedColor = new Color(0.1921569f, 0.1921569f, 0.1921569f, 1f);
    }
}
