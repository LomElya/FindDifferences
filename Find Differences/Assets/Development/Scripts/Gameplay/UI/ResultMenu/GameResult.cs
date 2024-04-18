using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class GameResult : MonoBehaviour
{
    [SerializeField] private GameResultIntroAnimation _introAnimation;

    [Header("Buttons")]
    [SerializeField] private ImageButton _restartButton;
    [SerializeField] private ImageButton _quitButton;

    // [Header("Text")]
    // [SerializeField] private StringValueView _description;

    private Canvas _canvas;

    private GameResultType _resultType;

    protected UniTaskCompletionSource<bool> _taskCompletion;

    private Action _onQuit;
    private Action _callback;

    public static UniTask<Disposable<GameResult>> Load()
    {
        var assetLoader = new LocalAssetLoader();
        return assetLoader.LoadDisposable<GameResult>(AssetsConstants.ResulMenu);
    }

    public async UniTask<bool> Show(GameResultType resultType, Action callback, Action onQuit, string description = "")
    {
        _canvas = GetComponent<Canvas>();
        _taskCompletion = new UniTaskCompletionSource<bool>();

        _callback = callback;
        _onQuit = onQuit;

        _resultType = resultType;

        Subscribe();

        await OnShow();

        var result = await _taskCompletion.Task;

        Unload();
        Unsubscribe();

        return result;
    }

    private void Unload() => _canvas.enabled = false;

    private async UniTask OnShow()
    {
        _canvas.enabled = true;

        OnInteractableButton(false);

        await _introAnimation.Play(_resultType);

        OnInteractableButton(true);
    }

    private void OnInteractableButton(bool isInteractable)
    {
        _restartButton.interactable = isInteractable;
        _quitButton.interactable = isInteractable;
    }

    private void OnRestartButtonClick()
    {
        _canvas.enabled = false;

        _callback?.Invoke();
        _taskCompletion.TrySetResult(true);
    }

    private void OnQuitButtonClick()
    {
        _canvas.enabled = false;

        _onQuit?.Invoke();
        _taskCompletion.TrySetResult(false);
    }

    private void Subscribe()
    {
        _restartButton.Click += OnRestartButtonClick;
        _quitButton.Click += OnQuitButtonClick;
    }

    private void Unsubscribe()
    {
        _restartButton.Click -= OnRestartButtonClick;
        _quitButton.Click -= OnQuitButtonClick;
    }
}
