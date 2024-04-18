using System;
using UnityEngine;
using TMPro;

public class ValueView<T> : MonoBehaviour where T : IConvertible
{
    [SerializeField] protected TMP_Text _text;

    public TMP_Text Text => _text;

    public virtual void Show(T value)
    {
        gameObject.SetActive(true);
        _text.text = value.ToString();
    }

    public void Hide() => gameObject.SetActive(false);
    public virtual void Calculate(T value) => _text.text = value.ToString();
    public virtual void SetColor(Color color) => _text.color = color;
    public virtual void SetFrontSize(float value) => _text.fontSize = value;
    public virtual float GetFrontSize() => _text.fontSize;

}
