using TMPro;
using UnityEngine;

// �� �� ���� �� ǥ��
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
