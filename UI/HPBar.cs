using UnityEngine;
using UnityEngine.UI;

// HPBar UI ����ڿ��� �ڽ����� �پ� HP UI ǥ��
public class HPBar : MonoBehaviour
{
    // ���� UI canvas
    private Canvas canvas;
    // �θ� Ŭ����
    private BaseObject parentScript;
    // hpBar�� ǥ���� slider
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

    // HP ������ ���� slider ����
    private void SetHPRatio(float ratio)
    {
        slider.value = ratio;
    }
}
