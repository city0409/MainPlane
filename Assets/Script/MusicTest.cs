using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicTest : MonoBehaviour 
{
    public AudioMixer mixer;
    public AudioSource audioBG;
	private void Start () 
	{
		
	}
	
	private void Update () 
	{
        if (Input .GetKey (KeyCode .A))
        {
            audioBG.Play();
        }
        else if(Input.GetKey(KeyCode.S))
        {
            audioBG.Pause ();
        }
    }
}
