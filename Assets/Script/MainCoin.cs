﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCoin : MonoBehaviour 
{
    public AudioSource audioCoin;
    private Collider2D collCoin;
    private Renderer rend;

    private void Awake () 
	{
        collCoin = GetComponent<Collider2D>();
        rend = GetComponent<Renderer>();
        audioCoin = GetComponent<AudioSource>();

    }
    private void OnTriggerEnter2D(Collider2D plane)
    {
        if (plane.gameObject .CompareTag ("Player"))
        {
            collCoin.enabled = false;
            audioCoin.Play();
            rend.enabled = false;
            MainLevelDirector.Instance.Score += 10;
            Destroy(this .gameObject , audioCoin.clip.length );

        }
    }
}
