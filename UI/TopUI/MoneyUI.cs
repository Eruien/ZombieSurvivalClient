using TMPro;
using UnityEngine;

// �� �� �� �ڿ� ǥ��
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
