using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPannel : MonoBehaviour 
{
    [SerializeField]
    private Slider musicSlider;
    [SerializeField]
    private Slider effectSlider;

    private void Start () 
	{
        musicSlider.value = AudioManager.Instance.MusicVolume;
        effectSlider.value = AudioManager.Instance.EffectVolume;
    }
	
    public void OnMusicSlider(float value)
    {
        AudioManager.Instance.MusicVolume = value;
    }

    public void OnEffectSlider(float value)
    {
        AudioManager.Instance.EffectVolume = value;
    }

}
