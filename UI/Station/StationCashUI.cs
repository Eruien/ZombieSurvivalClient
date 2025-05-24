using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// ������, ���� �ǹ����� ������ �� ����ϴ� UI
public class StationCashUI : MonoBehaviour
{
    // ������ �ǹ�
    [SerializeField]
    private GameObject station;
    // �ǹ��� �����ϰ� �ǹ��� ���õ� UI ����
    [SerializeField]
    private GameObject stayStationUI;
    // �ǹ��� ������ ���� ���� �� �׸�, ���� �� ����
    [SerializeField]
    private Sprite greenSprite;
    [SerializeField]
    private Sprite redSprite;

    // �Ʒ����� �ʱ�ȭ
    
    // ����� ǥ���� text, ����� �Һ�� �� ������ slider
    private TextMeshProUGUI costTextUI;
    private Slider slider;
    private Image progressBarImage;

    // �ǹ��� ����, ���� �Һ�ǰ� �ִ� ����
    private float buildPrice = 0.0f;
    private float currentBuildPrice = 0.0f;

    private void Awake()
    {
        buildPrice = Managers.Data.costDict[station.name].cost;
        currentBuildPrice = buildPrice;
        costTextUI = Managers.FindChildObject(gameObject, "Cost").GetComponent<TextMeshProUGUI>();
        slider = Managers.FindChildObject(gameObject, "ProgressBar").GetComponent<Slider>();
        progressBarImage = Managers.FindChildObject(gameObject, "Fill").GetComponent<Image>();
    }

    private void Update()
    {
        costTextUI.text = ((int)currentBuildPrice).ToString();
    }

    // slider�ٿ� ���� �������� ����
    private void SetMoneyRatio(float ratio)
    {
        slider.value = 1 - ratio;
    }

    // ���� ����� ������� �˻�
    private bool CheckPayMoney()
    {
        if (Managers.Item.CurrentMoney < buildPrice)
        {
            return false;  
        }
        return true;
    }
    
    // ����ڰ� �ö���� �� ����
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            if (CheckPayMoney())
            {
                StartCoroutine(ShowMoneyState());
                
                progressBarImage.sprite = greenSprite;
            }
            else
            {
                progressBarImage.sprite = redSprite;
                slider.value = 1;
            }
           
        }
    }

    // ����ڰ� Ʈ���Ÿ� ����� �� slider �ʱ�ȭ
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            slider.value = 0;
        }
    }

    // �ڷ�ƾ�� ���� �� �� ����ǰ� ���� �˾Ƽ� ��ǥ �ݾױ��� �����ǰ�
    private IEnumerator ShowMoneyState()
    {
        while (currentBuildPrice > 0)
        {
            currentBuildPrice -= Time.fixedDeltaTime * buildPrice * 0.5f;
            Managers.Item.CurrentMoney -= Time.fixedDeltaTime * buildPrice * 0.5f;
            SetMoneyRatio(currentBuildPrice / buildPrice);
            yield return null;
        }

        currentBuildPrice = buildPrice;
        station.SetActive(true);
        stayStationUI.SetActive(true);
        gameObject.SetActive(false);
    }
}
