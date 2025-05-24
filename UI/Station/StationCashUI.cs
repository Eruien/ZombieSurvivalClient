using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// 경찰서, 병원 건물등을 구입할 때 사용하는 UI
public class StationCashUI : MonoBehaviour
{
    // 생성할 건물
    [SerializeField]
    private GameObject station;
    // 건물을 생성하고 건물과 관련된 UI 생성
    [SerializeField]
    private GameObject stayStationUI;
    // 건물을 생성할 돈이 있을 때 그린, 없을 때 레드
    [SerializeField]
    private Sprite greenSprite;
    [SerializeField]
    private Sprite redSprite;

    // 아래에서 초기화
    
    // 비용을 표기할 text, 비용이 소비될 때 움직일 slider
    private TextMeshProUGUI costTextUI;
    private Slider slider;
    private Image progressBarImage;

    // 건물의 가격, 현재 소비되고 있는 가격
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

    // slider바에 돈의 비율따라 조정
    private void SetMoneyRatio(float ratio)
    {
        slider.value = 1 - ratio;
    }

    // 현재 비용이 충분한지 검사
    private bool CheckPayMoney()
    {
        if (Managers.Item.CurrentMoney < buildPrice)
        {
            return false;  
        }
        return true;
    }
    
    // 사용자가 올라왔을 때 실행
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

    // 사용자가 트리거를 벗어날을 때 slider 초기화
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            slider.value = 0;
        }
    }

    // 코루틴을 통해 한 번 실행되고 나면 알아서 목표 금액까지 차감되게
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
