using System;
using UnityEngine;

public class Timer : IDisposable
{
    public event Action<float> TimeUpdated;
    public event Action TimeFinished;

    private TimeInvoker _timeInvoker;

    public float MaxSeconds { get; private set; }
    public float RemainigSeconds { get; private set; }

    public bool IsActive { get; private set; }
    public bool IsPaused { get; private set; }
    
    public float PassedSeconds =>  MaxSeconds - RemainigSeconds;

    public Timer(MonoBehaviour context)
    {
        SetTimeInvoker(context);
    }

    public Timer(MonoBehaviour context, float seconds)
    {
        SetTimeInvoker(context);
        SetTime(seconds);
    }

    public void SetTime(float seconds)
    {
        MaxSeconds = seconds;
        RemainigSeconds = seconds;

        InvokeSeconds();
    }

    public void Start()
    {
        if (IsActive)
            return;

        if (RemainigSeconds == 0)
        {
            Debug.LogError("Таймер начался со значением 0");
            FinishedTimer();
        }

        IsActive = true;
        IsPaused = false;
        Subscribe();

        _timeInvoker.StartUpdate();

        InvokeSeconds();
    }

    public void Start(float seconds)
    {
        if (IsActive)
            return;

        SetTime(seconds);
        Start();
    }

    public void SetPause(bool isPaused)
    {
        if (isPaused)
            Pause();

        else
            Unpause();
    }

    public void Pause()
    {
        if (IsPaused || !IsActive)
            return;

        IsPaused = true;

        Unsubscribe();
        InvokeSeconds();
    }

    public void Unpause()
    {
        if (!IsPaused || !IsActive)
            return;

        IsPaused = false;

        Subscribe();
        InvokeSeconds();
    }

    public void AddTime(float seconds)
    {
        if (!IsActive)
            return;

        RemainigSeconds += seconds;
        InvokeSeconds();
    }

    public void ReduceTime(float seconds)
    {
        if (!IsActive)
            return;

        RemainigSeconds -= seconds;

        TimChangeSource();
        CheckFinished();
    }

    public void Stop()
    {
        if (!IsActive)
            return;

        Unsubscribe();

        RemainigSeconds = 0f;

        IsPaused = false;
        IsActive = false;

        _timeInvoker.Stop();

        InvokeSeconds();
        FinishedTimer();
    }
    private void OnSecondsUpdates()
    {
        if (IsPaused)
            return;

        RemainigSeconds -= 1;

        TimChangeSource();
        CheckFinished();
    }

    private void CheckFinished()
    {
        if (RemainigSeconds <= 0f)
            Stop();
    }

    private void TimChangeSource()
    {
        if (RemainigSeconds >= 0f)
            InvokeSeconds();
    }

    private void Subscribe() => _timeInvoker.SecondsUpdates += OnSecondsUpdates;
    private void Unsubscribe() => _timeInvoker.SecondsUpdates -= OnSecondsUpdates;
    private void SetTimeInvoker(MonoBehaviour context) => _timeInvoker = new TimeInvoker(context);
    private void InvokeSeconds() => TimeUpdated?.Invoke(RemainigSeconds);
    private void FinishedTimer() => TimeFinished?.Invoke();
    public void Dispose() => Unsubscribe();
}
