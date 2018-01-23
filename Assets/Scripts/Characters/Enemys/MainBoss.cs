using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBoss : EnemyBase
{
    [SerializeField]
    private Transform bossPlane;
    [SerializeField]
    private GameObject bossFx;
    [SerializeField]
    private float speed = 1f;
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private float repeatRate;

    private float MaxX;
    private float MaxY;
    private float MinX;
    private float MinY;
    private Vector3 direction;
    private Vector3 leftDirection;
    private Vector3 rightDirection;

    private MainPlane mainPlane;

    void Awake()
    {
        bossPlane = GetComponent<Transform>();
    }

    private void Start()
    {
        InvokeRepeating("Fire", 0f, repeatRate);
        MaxX = MainScreenXY.MaxX - 1;
        MaxY = MainScreenXY.MaxY;
        MinX = MainScreenXY.MinX + 1;
        MinY = MainScreenXY.MinY - 1;

        leftDirection = (Vector3.left + Vector3.down * 0.1f).normalized;
        rightDirection = (Vector3.right + Vector3.down * 0.1f).normalized;
        direction = leftDirection;
    }

    void Update()
    {
        if (transform.position.y < MinY)
        {
            Destroy(gameObject);
        }
        else if (transform.position.x > MaxX)
        {
            direction = leftDirection;
        }
        else if (transform.position.x < MinX)
        {
            direction = rightDirection;
        }

        bossPlane.Translate(direction * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            //coll.gameObject.SetActive(false );
            //Damage(1);
            //        collEnemy.enabled = false;

            //        audioEnemy.Play();
            //        rend.enabled = false;
            //        Destroy(this.gameObject, audioEnemy.clip.length);

        }
    }

    private void Fire()
    {
        Instantiate(bullet, transform.position, Quaternion.identity);
    }
    public override void Damage(int val, GameObject initiator)
    {
        Health -= val;
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
        print("Boss" + Health);
    }

    public void OnDestroySelf()
    {
        //Instantiate ();
    } 
}
