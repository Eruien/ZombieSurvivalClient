using TMPro;
using UnityEngine;
using UnityEngine.UI;

// ���� �Һ��ϴ� UI�� �ֻ��� �θ�
public class ConsumeCashUI : MonoBehaviour
{
    // ���� �� �� ����� ��Ÿ���� textUI
    protected TextMeshProUGUI costTextUI;
    // ���� �� �� �ڿ� ����� ��Ÿ���� textUI
    protected TextMeshProUGUI costItemTextUI;
    // �̹����� �÷��� ������ ���� 
    protected Image backgroundImage;
    protected Image sourceImage;
    protected Image cashImage;
    protected Image itemImage;
    
    // ��Ȱ��ȭ ������ �� ȸ�� �÷�, Ȱ��ȭ ���� ��
    protected Color32 grayColor = new Color32(128, 128, 128, 255);
    protected Color32 originalColor = new Color32(255, 255, 255, 255);

    // ���, �ڿ� ���
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

    // �ڿ� ���, ��뿡 ���� UI �÷��� Ȱ��ȭ
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

    // ���� �Һ��� �� UI ��ư Ŭ�� �Ҹ� ���Ŀ� �ڽĿ��� �ൿ ����
    public virtual void OnConsumeCash()
    {
        Managers.Sound.PlayOneShot(gameObject, "buttonClick");
    }
}
