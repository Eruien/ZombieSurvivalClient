using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

// ���� ������ �Ҹ� ���� �Ѱ� ����
public class SoundManager
{
    // �ߺ� ���� �ʰ� ���ο� �Ҹ��� ���� �� ���� ���� �̸����� ���
    public Dictionary<string, AudioClip> audioClipDict = new Dictionary<string, AudioClip>();
    // �Ҹ��� �׷����� ���� ����
    private AudioMixer audioMixer;
    // �Ҹ� �׷��� �ҷ��ͼ� audio source�� ����
    private AudioMixerGroup[] groups;
    // �ɼǰ��� UI �г�
    private GameObject optionUIPanel;

    // ���� �о߿� ���õ� ����
    public float materVolume = 1.0f;
    public float BGMVolume = 1.0f;
    public float EffectVolume = 1.0f;

    public void Init()
    {
        audioMixer = Managers.Resource.Load<AudioMixer>($"Sound/AudioMixer");
        groups = audioMixer.FindMatchingGroups("Master");
    }
    
    // �Ҹ� �� �� ���� �뵵 Effect�׷�
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

    // �Ҹ� �� �� ���� OneShot�� Ư¡���� �ߺ� ������ ������ Effect �׷�
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

    // ��� ���ǿ� 
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

    // �׷츶�� �Ҹ� ����
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

    // ���� �ٲ� �� ���� �����̴� ����
    public void SetMixerSlider()
    {
        GameObject obj = GameObject.Find("OptionUIPanel");
        Managers.FindChildObject(obj, "MasterSlider").GetComponent<Slider>().value = materVolume;
        Managers.FindChildObject(obj, "BGMSlider").GetComponent<Slider>().value = BGMVolume; 
        Managers.FindChildObject(obj, "EffectSlider").GetComponent<Slider>().value = EffectVolume; 
    }
}
