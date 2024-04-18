using System;

public class Disposable<T> : IDisposable
{
    public readonly Action<T> _dispose;
    private bool _isDisposed;

    public Disposable(T value, Action<T> dispose)
    {
        Value = value;
        _dispose = dispose;
    }

    public T Value { get; }

    public void Dispose()
    {
        if (_isDisposed)
            return;

        _isDisposed = true;
        _dispose(Value);
    }

    public static Disposable<T> Borrow(T value, Action<T> dispose) => new(value, dispose);
}
