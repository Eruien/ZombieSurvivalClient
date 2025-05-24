using UnityEngine;
using UnityEngine.UI;

// 건물이 생성되고 나서 건물과 관련된 UI
public class StayStationUI : MonoBehaviour
{
    // 사용자가 올라가있으면 초록색으로 표시
    [SerializeField]
    private Sprite greenSprite;
    // 사용자가 올라가 있으면 패널 활성화
    [SerializeField]
    private GameObject stationPanel;
    private Slider slider;
    private Image progressBarImage;

    private void Awake()
    {
        slider = Managers.FindChildObject(gameObject, "ProgressBar").GetComponent<Slider>();
        progressBarImage = Managers.FindChildObject(gameObject, "Fill").GetComponent<Image>();
    }

    // 사용자가 올라가있으면 패널을 활성화 하고 초록색으로 표시
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            progressBarImage.sprite = greenSprite;
            slider.value = 1;
            stationPanel.SetActive(true);
        }
    }

    // 사용자가 트리거에서 벗어나면 slider 초기화
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            slider.value = 0;
            stationPanel.SetActive(false);
        }
    }
}
