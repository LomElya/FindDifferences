public class IntValueView : ValueView<int>
{
    protected int _value;

    public override void Show(int value)
    {
        base.Show(value);
        _value = value;
    }
}
