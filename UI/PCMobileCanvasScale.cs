using UnityEngine;
using UnityEngine.UI;

// �ΰ��ӿ��� PC�� Mobile ȯ�濡 ���� ĵ���� match ����
public class PCMobileCanvasScale : MonoBehaviour
{
    CanvasScaler scaler;

    private void Awake()
    {
        scaler = GetComponent<CanvasScaler>();

        // PC
#if UNITY_STANDALONE
        scaler.matchWidthOrHeight = 0.0f;

        // Mobile
#elif UNITY_IOS || UNITY_ANDROID
        scaler.matchWidthOrHeight = 0.35f;
#endif
    }

}
