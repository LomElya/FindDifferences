using Zenject;

public class AddTimeButton : ImageButton
{
    private PurchaseManager _manager;

    private const string addTimeKey = "com.fatcat.finddifferences.time";

    [Inject]
    private void Construct(PurchaseManager manager) => _manager = manager;

    protected override void PointDown() => _manager.BuyConsumable(addTimeKey);
}
