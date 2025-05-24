using Assets.Scripts;
using UnityEngine;

// �ɼ� UI
public class OptionalUI : MonoBehaviour
{
    // â �ݱ� ��ư
    public void OnXButton()
    {
        Managers.Sound.PlayOneShot(gameObject, "buttonClick");
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
    }

    // ������ ���� ���� ��ư
    public void OnMasterVolumeSliderChange(float volume)
    {
        Managers.Sound.SetAudioSound(SoundType.Master, volume);
    }

    // BGM ���� ���� ��ư
    public void OnBGMVolumeSliderChange(float volume)
    {
        Managers.Sound.SetAudioSound(SoundType.BGM, volume);
    }

    // Effect ���� ���� ��ư
    public void OnEffectVolumeSliderChange(float volume)
    {
        Managers.Sound.SetAudioSound(SoundType.Effect, volume);
    }
}
