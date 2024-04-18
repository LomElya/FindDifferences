using TMPro;
using UnityEngine;
using Zenject;

public class TimerView : MonoBehaviour
{
    [SerializeField] private TMP_Text _timerText;

    private Timer _timer;

    [Inject]
    private void Construct(Timer timer)
    {
        _timer = timer;

        _timer.TimeUpdated += OnTimeUpdate;
    }

    private void OnTimeUpdate(float seconds) => _timerText.text = seconds.ConvertToMinutesString();
    private void OnDisable() => _timer.TimeUpdated -= OnTimeUpdate;
}
