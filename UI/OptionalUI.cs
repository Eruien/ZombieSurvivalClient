using Assets.Scripts;
using UnityEngine;

// 옵션 UI
public class OptionalUI : MonoBehaviour
{
    // 창 닫기 버튼
    public void OnXButton()
    {
        Managers.Sound.PlayOneShot(gameObject, "buttonClick");
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
    }

    // 마스터 볼륨 조절 버튼
    public void OnMasterVolumeSliderChange(float volume)
    {
        Managers.Sound.SetAudioSound(SoundType.Master, volume);
    }

    // BGM 볼륨 조절 버튼
    public void OnBGMVolumeSliderChange(float volume)
    {
        Managers.Sound.SetAudioSound(SoundType.BGM, volume);
    }

    // Effect 볼륨 조절 버튼
    public void OnEffectVolumeSliderChange(float volume)
    {
        Managers.Sound.SetAudioSound(SoundType.Effect, volume);
    }
}
