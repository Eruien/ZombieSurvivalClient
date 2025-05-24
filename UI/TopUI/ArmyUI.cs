using TMPro;
using UnityEngine;

// 맨 위 군인 수 표시
public class ArmyUI : MonoBehaviour
{
    private TextMeshProUGUI costTextUI;

    private void Awake()
    {
        costTextUI = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        Managers.Memory.armyCountObserver += SetText;
    }

    public void SetText(int count)
    {
        costTextUI.text = count.ToString();
    }
}
