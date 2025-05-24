using UnityEngine;
using UnityEngine.UI;

// �庮 HP UI Ŭ����
public class FenceHPBar : MonoBehaviour
{
    [SerializeField]
    private BaseObject fence;

    private Slider slider;

    private void Awake()
    {
        slider = GetComponentInChildren<Slider>();
    }

    private void Update()
    {
        SetHPRatio(fence.hp / fence.objectStat.maxhp);
    }

    // HP ������ �޾Ƽ� slider���� ����
    private void SetHPRatio(float ratio)
    {
        slider.value = ratio;
    }
}
