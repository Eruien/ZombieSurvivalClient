
// 장벽 회복력 증가 구입에 사용되는 UI
public class RecoveryFenceCashUI : ConsumeCashUI
{
    protected override void Awake()
    {
        base.Awake();
        // 현재 음식 자원의 상태에 따라 UI 활성화 결정
        ActiveColor(Managers.Item.Food);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        // Managers 음식 자원 관찰자에 이벤트 등록
        Managers.Item.foodObserver += ActiveColor;
    }

    // 버튼을 누르면 돈을 소비하고 장벽 회복력 증가
    public override void OnConsumeCash()
    {
        base.OnConsumeCash();
        if (Managers.Item.CurrentMoney - cost < 0 || Managers.Item.Food - itemCost < 0) return;

        Managers.Item.CurrentMoney -= cost;
        Managers.Item.Food -= itemCost;
        InGameSceneManager.fenceScript.recoveryRate += 1.0f; 
    }
}
