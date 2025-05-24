
// �Ʊ��� ���ݷ� ���� ���Կ� ����ϴ� UI
public class AttackPowerCashUI : ConsumeCashUI
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

    // ��ư�� ������ ���� �Һ��ϰ� ���� ���ݷ� ����
    public override void OnConsumeCash()
    {
        base.OnConsumeCash();
        if (Managers.Item.CurrentMoney - cost < 0 || Managers.Item.Human - itemCost < 0) return;

        Managers.Item.CurrentMoney -= cost;
        Managers.Item.Human -= itemCost;
        Managers.Data.objectDict["Army"].defaultAttackDamage += 10.0f;
    }

}
