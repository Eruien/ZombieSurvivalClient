using TMPro;
using UnityEngine;
using UnityEngine.UI;

// 돈을 소비하는 UI의 최상위 부모
public class ConsumeCashUI : MonoBehaviour
{
    // 구입 할 때 비용을 나타내는 textUI
    protected TextMeshProUGUI costTextUI;
    // 구입 할 때 자원 비용을 나타내는 textUI
    protected TextMeshProUGUI costItemTextUI;
    // 이미지의 컬러값 조절을 위해 
    protected Image backgroundImage;
    protected Image sourceImage;
    protected Image cashImage;
    protected Image itemImage;
    
    // 비활성화 상태일 때 회색 컬러, 활성화 원래 색
    protected Color32 grayColor = new Color32(128, 128, 128, 255);
    protected Color32 originalColor = new Color32(255, 255, 255, 255);

    // 비용, 자원 비용
    protected float cost = 0.0f;
    protected int itemCost = 0;

    protected virtual void Awake()
    {
        cost = Managers.Data.costDict[this.GetType().Name].cost;
        itemCost = Managers.Data.costDict[this.GetType().Name].itemCost;
        costTextUI = Managers.FindChildObject(gameObject, "Cost").GetComponent<TextMeshProUGUI>();
        costTextUI.text = cost.ToString();
        costItemTextUI = Managers.FindChildObject(gameObject, "ItemCost").GetComponent<TextMeshProUGUI>();
        costItemTextUI.text = itemCost.ToString();
        backgroundImage = GetComponent<Image>();
        sourceImage = Managers.FindChildObject(gameObject, "SourceImage").GetComponent<Image>();
        cashImage = Managers.FindChildObject(gameObject, "CashImage").GetComponent<Image>();
        itemImage = Managers.FindChildObject(gameObject, "ItemImage").GetComponent<Image>();
    }

    protected virtual void OnEnable()
    {
       
    }

    // 자원 비용, 비용에 따라 UI 컬러값 활성화
    protected virtual void ActiveColor(int itemCount)
    {
        if (itemCount >= itemCost && Managers.Item.CurrentMoney >= cost)
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

    // 돈을 소비할 때 UI 버튼 클릭 소리 이후엔 자식에서 행동 결정
    public virtual void OnConsumeCash()
    {
        Managers.Sound.PlayOneShot(gameObject, "buttonClick");
    }
}
