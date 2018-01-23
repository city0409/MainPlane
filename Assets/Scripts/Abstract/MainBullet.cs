using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MainBullet : MonoBehaviour
{
    [SerializeField]
    protected float bulletSpeed = 5f;
    public AudioSource audioBullet;
    [SerializeField]
    protected int power = 1;
    [SerializeField]
    private string myTag;

    private void Awake()
    {
        audioBullet = GetComponent<AudioSource>();
        audioBullet.Play();
    }

    private void Update()
    {
        Move();
    }

    protected abstract void Move();

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<IHealth>() != null && !collision.CompareTag(myTag))
        {
            collision.GetComponent<IHealth>().Damage(power,gameObject);
            //print ("Bullet"+collision .get)
        }
    }
}
