using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

public class MenuLoadingOperation : ILoadingOperation
{
    public string Description => "Main menu loading...";

    private LoadSceneMode _mode;

    public MenuLoadingOperation(LoadSceneMode mode = LoadSceneMode.Additive)
    {
        _mode = mode;
    }

    public async UniTask Load(Action<float> onProgress)
    {
        onProgress?.Invoke(0.5f);
        var loadOp = SceneManager.LoadSceneAsync(Constants.Scenes.MAIN_MENU, _mode);

        while (loadOp.isDone == false)
            await UniTask.Yield();

        var scene = SceneManager.GetSceneByName(Constants.Scenes.MAIN_MENU);

        var mainMenu = scene.GetRoot<MainMenu>();

        onProgress?.Invoke(0.85f);

        mainMenu.Init();

        onProgress?.Invoke(1f);
    }
}
