using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField]
    private GameData gameSetting;
    [SerializeField]
    private AudioMixer mixer;

    private float musicVolume;
    private float effectVolume;

    protected override void Awake()
    {
        base.Awake();
        musicVolume = gameSetting.musicVolume;//云端下载音量数据
        effectVolume = gameSetting.effectVolume;
        mixer.SetFloat("Bg", musicVolume);//硬盘存储音量数据
        mixer.SetFloat("Fx", effectVolume);
    }

    public float MusicVolume
    {
        get { return musicVolume; }
        set
        {
            gameSetting.musicVolume = value;//云端存储用户调节数据
            musicVolume = value;//内存存储用户调节数据，速度快
            mixer.SetFloat("Bg", value);//硬盘存储用户调节数据
        }
    }

    public float EffectVolume
    {
        get { return effectVolume; }
        set
        {
            gameSetting.effectVolume = value;
            effectVolume = value;
            mixer.SetFloat("Fx", value);
        }
    }
}
