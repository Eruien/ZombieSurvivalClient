using TMPro;
using UnityEngine;

// �� �� ���� �ڿ� ǥ��
public class HumanUI : MonoBehaviour
{
    private TextMeshProUGUI costTextUI;

    private void Awake()
    {
        costTextUI = GetComponent<TextMeshProUGUI>();
        SetText(Managers.Item.Human);
    }

    private void OnEnable()
    {
        Managers.Item.humanObserver += SetText;
    }

    public void SetText(int count)
    {
        costTextUI.text = count.ToString();
    }
}
