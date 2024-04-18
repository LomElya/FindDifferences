using UnityEngine;

public class StringValueView : ValueView<string>
{
    [SerializeField, TextArea] private string _template;

    public override void Show(string value)
    {
        gameObject.SetActive(true);
        _text.text = $"{_template}{value}";
    }

    public void Show(int value) => Show(value.ToString());

    public void Show(float value) => Show(value.ToString());
}
