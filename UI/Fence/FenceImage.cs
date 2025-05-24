using UnityEngine;
using UnityEngine.UI;

// 장벽 이미지 컬러 관리를 위한 클래스
public class FenceImage : MonoBehaviour
{
    // 원본색, 비활성화 될 때 색깔
    private Color32 originalColor = new Color32(255, 255, 255, 255);
    private Color32 grayColor = new Color32(128, 128, 128, 255);

    // 장벽 이미지
    private Image fenceImage;
    
    private void Awake()
    {
        fenceImage = GetComponent<Image>();
    }

    private void OnEnable()
    {
        InGameSceneManager.fenceScript.deathObserver += ChangeGrayColor;
        InGameSceneManager.fenceScript.gateActiveObserver += ChangeOriginalColor;
    }

    // 원본색으로 변경
    private void ChangeOriginalColor()
    {
        fenceImage.color = originalColor;
    }

    // 비활성화 될 때 색으로 변경
    private void ChangeGrayColor()
    {
        fenceImage.color = grayColor;
    }
}
