
// 새로운 장벽을 생성하는데 사용되는 UI
public class CreateNewFenceCashUI : ConsumeCashUI
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
        // 장벽상태에 따라 UI컬러가 활성화 될 수 있게 이벤트 등록
        InGameSceneManager.fenceScript.gateActiveObserver += ActiveColor;
        InGameSceneManager.fenceScript.gateDeActiveObserver += ActiveColor;
    }

    // 자원 비용, 비용, 장벽 활성화 여부에 따라 UI 컬러값 활성화
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

    // 장벽 이벤트용 UI컬러 함수
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

    // 버튼을 누르면 돈을 소비하고 장벽 생성
    public override void OnConsumeCash()
    {
        base.OnConsumeCash();
        if (Managers.Item.CurrentMoney - cost < 0 || InGameSceneManager.fenceScript.gameObject.activeSelf) return;

        Managers.Item.CurrentMoney -= cost;
        InGameSceneManager.CreateNewFence();
    }
}
