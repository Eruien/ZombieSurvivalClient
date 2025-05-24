using UnityEngine;
using UnityEngine.UI;

// �庮 �̹��� �÷� ������ ���� Ŭ����
public class FenceImage : MonoBehaviour
{
    // ������, ��Ȱ��ȭ �� �� ����
    private Color32 originalColor = new Color32(255, 255, 255, 255);
    private Color32 grayColor = new Color32(128, 128, 128, 255);

    // �庮 �̹���
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

    // ���������� ����
    private void ChangeOriginalColor()
    {
        fenceImage.color = originalColor;
    }

    // ��Ȱ��ȭ �� �� ������ ����
    private void ChangeGrayColor()
    {
        fenceImage.color = grayColor;
    }
}
