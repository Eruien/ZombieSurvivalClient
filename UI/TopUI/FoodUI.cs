using TMPro;
using UnityEngine;

// 맨 위 음식 자원 표시
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
