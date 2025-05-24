using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

// 사운드 관리자 소리 관련 총괄 관리
public class SoundManager
{
    // 중복 되지 않게 새로운 소리가 들어올 때 마다 음악 이름으로 등록
    public Dictionary<string, AudioClip> audioClipDict = new Dictionary<string, AudioClip>();
    // 소리를 그룹으로 만들어서 관리
    private AudioMixer audioMixer;
    // 소리 그룹을 불러와서 audio source를 세팅
    private AudioMixerGroup[] groups;
    // 옵션관련 UI 패널
    private GameObject optionUIPanel;

    // 각종 분야에 관련된 볼륨
    public float materVolume = 1.0f;
    public float BGMVolume = 1.0f;
    public float EffectVolume = 1.0f;

    public void Init()
    {
        audioMixer = Managers.Resource.Load<AudioMixer>($"Sound/AudioMixer");
        groups = audioMixer.FindMatchingGroups("Master");
    }
    
    // 소리 한 번 실행 용도 Effect그룹
    public void PlaySound(GameObject obj, string name)
    {
        AudioSource source = obj.GetComponent<AudioSource>();

        if (source == null)
        {
            source = obj.AddComponent<AudioSource>();
        }

        AudioClip clip = null;

        if (!audioClipDict.TryGetValue(name, out clip))
        {
            clip = Managers.Resource.Load<AudioClip>($"Sound/{name}");
            audioClipDict.Add(name, clip);
        }

        source.clip = clip;
        source.outputAudioMixerGroup = groups[2];
        
        if (!source.isPlaying)
        {
            source.Play();
        }
    }

    // 소리 한 번 실행 OneShot의 특징으로 중복 실행이 가능함 Effect 그룹
    public void PlayOneShot(GameObject obj, string name)
    {
        AudioSource source = obj.GetComponent<AudioSource>();

        if (source == null)
        {
            source = obj.AddComponent<AudioSource>();
        }

        AudioClip clip = null;

        if (!audioClipDict.TryGetValue(name, out clip))
        {
            clip = Managers.Resource.Load<AudioClip>($"Sound/{name}");
            audioClipDict.Add(name, clip);
        }

        source.outputAudioMixerGroup = groups[2];
        source.PlayOneShot(clip);
    }

    // 배경 음악용 
    public void PlayBackgroundSound(string name)
    {
        AudioSource source = Managers.Instance.gameObject.GetComponent<AudioSource>();

        if (source == null)
        {
            source = Managers.Instance.gameObject.AddComponent<AudioSource>();
        }

        AudioClip clip = null;

        if (!audioClipDict.TryGetValue(name, out clip))
        {
            clip = Managers.Resource.Load<AudioClip>($"Sound/{name}");
            audioClipDict.Add(name, clip);
        }

        source.outputAudioMixerGroup = groups[1];
        source.loop = true;
        source.clip = clip;
        source.Play();
    }

    // 그룹마다 소리 세팅
    public void SetAudioSound(SoundType type, float volume)
    {
        volume = Mathf.Clamp(volume, 0.0001f, 1.0f);

        switch (type)
        {
            case SoundType.Master:
                materVolume = volume;
                break;
            case SoundType.BGM:
                BGMVolume = volume;
                break;
            case SoundType.Effect:
                EffectVolume = volume;
                break;
        }
       
        audioMixer.SetFloat(type.ToString(), Mathf.Log10(volume) * 20);
    }

    // 씬이 바뀔 때 마다 슬라이더 변경
    public void SetMixerSlider()
    {
        GameObject obj = GameObject.Find("OptionUIPanel");
        Managers.FindChildObject(obj, "MasterSlider").GetComponent<Slider>().value = materVolume;
        Managers.FindChildObject(obj, "BGMSlider").GetComponent<Slider>().value = BGMVolume; 
        Managers.FindChildObject(obj, "EffectSlider").GetComponent<Slider>().value = EffectVolume; 
    }
}
