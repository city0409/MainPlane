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
        musicVolume = gameSetting.musicVolume;
        effectVolume = gameSetting.effectVolume;
        mixer.SetFloat("Bg", musicVolume);
        mixer.SetFloat("Fx", effectVolume);
    }

    public float MusicVolume
    {
        get { return musicVolume; }
        set
        {
            gameSetting.musicVolume = value;
            musicVolume = value;
            mixer.SetFloat("Bg", value);
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
