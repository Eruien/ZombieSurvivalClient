
// ���ο� �庮�� �����ϴµ� ���Ǵ� UI
public class CreateNewFenceCashUI : ConsumeCashUI
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
        // �庮���¿� ���� UI�÷��� Ȱ��ȭ �� �� �ְ� �̺�Ʈ ���
        InGameSceneManager.fenceScript.gateActiveObserver += ActiveColor;
        InGameSceneManager.fenceScript.gateDeActiveObserver += ActiveColor;
    }

    // �ڿ� ���, ���, �庮 Ȱ��ȭ ���ο� ���� UI �÷��� Ȱ��ȭ
    protected override void ActiveColor(int itemCount)
    {
        if (itemCount >= itemCost && Managers.Item.CurrentMoney >= cost && !InGameSceneManager.fenceScript.gameObject.activeSelf)
        {
            backgroundImage.color = originalColor;
            sourceImage.color = originalColor;
            cashImage.color = originalColor;
            itemImage.color = originalColor;
        }
        else
        {
            backgroundImage.color = grayColor;
            sourceImage.color = grayColor;
            cashImage.color = grayColor;
            itemImage.color = grayColor;
        }
    }

    // �庮 �̺�Ʈ�� UI�÷� �Լ�
    private void ActiveColor()
    {
        if (Managers.Item.Food >= itemCost && Managers.Item.CurrentMoney >= cost && !InGameSceneManager.fenceScript.gameObject.activeSelf)
        {
            backgroundImage.color = originalColor;
            sourceImage.color = originalColor;
            cashImage.color = originalColor;
            itemImage.color = originalColor;
        }
        else
        {
            backgroundImage.color = grayColor;
            sourceImage.color = grayColor;
            cashImage.color = grayColor;
            itemImage.color = grayColor;
        }
    }

    // ��ư�� ������ ���� �Һ��ϰ� �庮 ����
    public override void OnConsumeCash()
    {
        base.OnConsumeCash();
        if (Managers.Item.CurrentMoney - cost < 0 || InGameSceneManager.fenceScript.gameObject.activeSelf) return;

        Managers.Item.CurrentMoney -= cost;
        InGameSceneManager.CreateNewFence();
    }
}
