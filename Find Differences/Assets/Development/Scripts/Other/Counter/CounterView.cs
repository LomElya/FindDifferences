using UnityEngine;
using Zenject;

public class CounterView : MonoBehaviour
{
    [SerializeField] private StringValueView _text;

    private Counter _counter;

    [Inject]
    private void Construct(Counter counter) => _counter = counter;

    private void Awake()
    {
        OnCounterChange(_counter.GetCounterValue());

        _counter.CounterChange += OnCounterChange;
    }

    private void OnCounterChange(int value) => _text.Show(value);
}
