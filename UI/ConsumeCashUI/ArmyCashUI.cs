
// 군인을 구입할 때 사용하는 UI
public class ArmyCashUI : ConsumeCashUI
{
    protected override void Awake()
    {
        base.Awake();
        // 현재 인적 자원의 상태에 따라 UI 활성화 결정
        ActiveColor(Managers.Item.Human);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        // Managers 인적 자원 관찰자에 이벤트 등록
        Managers.Item.humanObserver += ActiveColor;
    }

    // 버튼을 누르면 돈을 소비하고 군인 스폰
    public override void OnConsumeCash()
    {
        base.OnConsumeCash();
        if (Managers.Item.CurrentMoney - cost < 0 || Managers.Item.Human - itemCost < 0) return;

        Managers.Item.CurrentMoney -= cost;
        Managers.Item.Human -= itemCost;
        Managers.Memory.ArmySpawn();
    }
}
