using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;
using UnityEngine;

public class AppodealManager : MonoBehaviour, IRewardedVideoAdListener
{
    private const string APP_KEY = "8202c1628d2a9fc8c04c247246eb5e27ec4b02dbbc05a062";

    [SerializeField] private bool _isTesting;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        Debug.Log("Инициализация рекламы");

        // Appodeal.setTesting(_isTesting);
        // Appodeal.disableLocationPermissionCheck();
        // Appodeal.muteVideosIfCallsMuted(true);

        // Appodeal.initialize(APP_KEY, Appodeal.INTERSTITIAL);

        // Appodeal.setRewardedVideoCallbacks(this);
    }

    public void ShowInterstatiAds()
    {
        // if (Appodeal.canShow(Appodeal.INTERSTITIAL))
        //     Appodeal.show(Appodeal.INTERSTITIAL);

        string textAlert = "Вызов межстраничной рекламы";

        Alert.Instance.ShowAlert(textAlert);

        Debug.Log(textAlert);
    }

    public void ShowRewarded()
    {

    }

    public void onRewardedVideoLoaded(bool precache)
    {
        Debug.Log("Видео загружено");
    }

    public void onRewardedVideoFailedToLoad()
    {
        Debug.Log("ОШИБКА. Видео не загружено.");
    }

    public void onRewardedVideoShowFailed()
    {
        Debug.Log("ОШИБКА. Видео не показано.");
    }

    public void onRewardedVideoShown()
    {
        Debug.Log("Видео показано");
    }

    public void onRewardedVideoClicked()
    {
        Debug.Log("Видео рекламы вызвано");
    }

    public void onRewardedVideoClosed(bool finished)
    {
        Debug.Log($"Реклама закртыта. Просмотрена: \"{finished}\"");
    }

    public void onRewardedVideoFinished(double amount, string name)
    {
        Debug.Log($"Реклама просмотрена. Выдать награду \"{name}\" - \"{amount}\"");
    }

    public void onRewardedVideoExpired()
    {
        Debug.Log("Реклама не может быть показана");
    }
}
