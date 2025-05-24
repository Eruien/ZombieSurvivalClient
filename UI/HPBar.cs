using UnityEngine;
using UnityEngine.UI;

// HPBar UI 사용자에게 자식으로 붙어 HP UI 표시
public class HPBar : MonoBehaviour
{
    // 월드 UI canvas
    private Canvas canvas;
    // 부모 클래스
    private BaseObject parentScript;
    // hpBar를 표기할 slider
    private Slider slider;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        slider = GetComponentInChildren<Slider>();
        canvas.renderMode = RenderMode.WorldSpace;
        canvas.worldCamera = Camera.main;
    }

    private void Start()
    {
        parentScript = transform.parent.GetComponent<BaseObject>();
    }

    private void Update()
    {
        Transform parent = transform.parent;
        transform.position = parent.position + Vector3.up * (parent.GetComponent<Collider>().bounds.size.y);
        transform.rotation = Camera.main.transform.rotation;
        SetHPRatio(parentScript.hp / parentScript.objectStat.maxhp);
    }

    // HP 비율에 따라 slider 세팅
    private void SetHPRatio(float ratio)
    {
        slider.value = ratio;
    }
}
