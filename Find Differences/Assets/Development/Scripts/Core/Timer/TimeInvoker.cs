using System;
using System.Collections;
using UnityEngine;

public class TimeInvoker : IDisposable
{
    public event Action SecondsUpdates;

    private IEnumerator _countdown;
    private MonoBehaviour _context;

    private float _oneSecond = 0;

    public TimeInvoker(MonoBehaviour contex) => _context = contex;

    public void StartUpdate()
    {
        _countdown = Countdown();

        _context.StartCoroutine(_countdown);
    }

    public void Stop()
    {
        if (_countdown != null)
            _context.StopCoroutine(_countdown);
    }

    private void InvokeUpdate() => SecondsUpdates?.Invoke();

    private IEnumerator Countdown()
    {
        while (true)
        {
            float delta = Time.deltaTime;

            _oneSecond += delta;

            if (_oneSecond >= 1f)
            {
                _oneSecond -= 1f;
                InvokeUpdate();
            }

            yield return null;
        }
    }

    public void Dispose()
    {
        if (_countdown != null)
            _context.StopCoroutine(_countdown);
    }
}
