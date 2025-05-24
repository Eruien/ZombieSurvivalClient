using UnityEngine;
using UnityEngine.UI;

// �ǹ��� �����ǰ� ���� �ǹ��� ���õ� UI
public class StayStationUI : MonoBehaviour
{
    // ����ڰ� �ö������� �ʷϻ����� ǥ��
    [SerializeField]
    private Sprite greenSprite;
    // ����ڰ� �ö� ������ �г� Ȱ��ȭ
    [SerializeField]
    private GameObject stationPanel;
    private Slider slider;
    private Image progressBarImage;

    private void Awake()
    {
        slider = Managers.FindChildObject(gameObject, "ProgressBar").GetComponent<Slider>();
        progressBarImage = Managers.FindChildObject(gameObject, "Fill").GetComponent<Image>();
    }

    // ����ڰ� �ö������� �г��� Ȱ��ȭ �ϰ� �ʷϻ����� ǥ��
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            progressBarImage.sprite = greenSprite;
            slider.value = 1;
            stationPanel.SetActive(true);
        }
    }

    // ����ڰ� Ʈ���ſ��� ����� slider �ʱ�ȭ
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            slider.value = 0;
            stationPanel.SetActive(false);
        }
    }
}
