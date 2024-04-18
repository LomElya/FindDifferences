using UnityEngine;
using Zenject;

public class MainMenu : MonoBehaviour
{
  [SerializeField] ImageButton _startGameButton;
  [SerializeField] ImageButton _exitButton;

  private LoadingScreenProvider _loadingProvider;

  [Inject]
  private void Construct(LoadingScreenProvider loadingProvider)
  {
    _loadingProvider = loadingProvider;
  }

  public void Init()
  {
    Subscribe();
  }

  private async void OnClickStartGameButton()
  {
    await _loadingProvider.LoadAndDestroy(new GameLoadingOperation());
  }

  private void OnClickExitGameButton()
  {
    Application.Quit();
  }

  private void Subscribe()
  {
    _startGameButton.Click += OnClickStartGameButton;
    _exitButton.Click += OnClickExitGameButton;
  }

  private void Unsubscribe()
  {
    _startGameButton.Click -= OnClickStartGameButton;
    _exitButton.Click -= OnClickExitGameButton;
  }

  private void OnDestroy() => Unsubscribe();
}
