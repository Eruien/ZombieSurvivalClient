using TMPro;
using UnityEngine;

// 맨 위 돈 자원 표시
public class MoneyUI : MonoBehaviour
{
    private TextMeshProUGUI costTextUI;
    
    private void Awake()
    {
        costTextUI = GetComponent<TextMeshProUGUI>();
        SetText(Managers.Item.CurrentMoney);
    }

    private void OnEnable()
    {
        Managers.Item.moneyObserver += SetText;
    }

    public void SetText(float count)
    {
        costTextUI.text = count.ToString();
    }
}
