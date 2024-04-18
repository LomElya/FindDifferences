using System.IO;
using UnityEditor;
using UnityEngine;

public static class HelperMenu
{
    private static string PlayerStatePath => $"{Application.persistentDataPath}/playerState.json";

    [MenuItem("Tools/Progress/Clear Save Data(удалить файл сохранения)")]
    public static void ClearData()
    {
        var path = PlayerStatePath;

        if (File.Exists(path))
            File.Delete(path);
    }

    [MenuItem("Tools/Progress/Show state file(показать файл сохранения)")]
    public static void ShowUserStateFile()
    {
        EditorUtility.RevealInFinder(PlayerStatePath);
    }

    [MenuItem("Tools/Assets/Clear Asset Bundle Cache(очистить кэш)")]
    public static void DoClearAssetBundleCache()
    {
        AssetBundle.UnloadAllAssetBundles(true);
        Debug.Log($"Результат очистки кэша: {Caching.ClearCache()}");
    }
}
