using UnityEngine;
using UnityEngine.UI;

// 장벽 HP UI 클래스
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

    // HP 비율을 받아서 slider값에 전달
    private void SetHPRatio(float ratio)
    {
        slider.value = ratio;
    }
}
