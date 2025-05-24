
// �庮 ���� ���� ���Կ� ���Ǵ� UI
public class UpgradeFenceCashUI : ConsumeCashUI
{
    protected override void Awake()
    {
        base.Awake();
        // ���� ���� �ڿ��� ���¿� ���� UI Ȱ��ȭ ����
        ActiveColor(Managers.Item.Food);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        // Managers ���� �ڿ� �����ڿ� �̺�Ʈ ���
        Managers.Item.foodObserver += ActiveColor;
    }

    // ��ư�� ������ ���� �Һ��ϰ� �庮 ���� ����
    public override void OnConsumeCash()
    {
        base.OnConsumeCash();
        if (Managers.Item.CurrentMoney - cost < 0 || Managers.Item.Food - itemCost < 0) return;

        Managers.Item.CurrentMoney -= cost;
        Managers.Item.Food -= itemCost;
        InGameSceneManager.fenceScript.objectStat.defense += 5;
    }
}
