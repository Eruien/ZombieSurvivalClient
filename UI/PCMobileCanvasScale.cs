using UnityEngine;
using UnityEngine.UI;

// 인게임에서 PC랑 Mobile 환경에 따라 캔버스 match 조절
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
