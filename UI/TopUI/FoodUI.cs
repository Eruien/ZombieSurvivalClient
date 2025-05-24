using TMPro;
using UnityEngine;

// �� �� ���� �ڿ� ǥ��
public class FoodUI : MonoBehaviour
{
    private TextMeshProUGUI costTextUI;

    private void Awake()
    {
        costTextUI = GetComponent<TextMeshProUGUI>();
        SetText(Managers.Item.Food);
    }

    private void OnEnable()
    {
        Managers.Item.foodObserver += SetText;
    }

    public void SetText(int count)
    {
        costTextUI.text = count.ToString();
    }

}
