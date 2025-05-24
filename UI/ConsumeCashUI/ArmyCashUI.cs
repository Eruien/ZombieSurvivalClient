
// ������ ������ �� ����ϴ� UI
public class ArmyCashUI : ConsumeCashUI
{
    protected override void Awake()
    {
        base.Awake();
        // ���� ���� �ڿ��� ���¿� ���� UI Ȱ��ȭ ����
        ActiveColor(Managers.Item.Human);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        // Managers ���� �ڿ� �����ڿ� �̺�Ʈ ���
        Managers.Item.humanObserver += ActiveColor;
    }

    // ��ư�� ������ ���� �Һ��ϰ� ���� ����
    public override void OnConsumeCash()
    {
        base.OnConsumeCash();
        if (Managers.Item.CurrentMoney - cost < 0 || Managers.Item.Human - itemCost < 0) return;

        Managers.Item.CurrentMoney -= cost;
        Managers.Item.Human -= itemCost;
        Managers.Memory.ArmySpawn();
    }
}
