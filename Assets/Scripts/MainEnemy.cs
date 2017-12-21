using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainEnemy : EnemyBase
{
    [SerializeField]
    private Transform enemyPlane;
    [SerializeField]
    private GameObject fx;
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
    private MainPlane mainPlane;

    void Awake()
    {
        enemyPlane = GetComponent<Transform>();
    }

    private void Start()
    {
        InvokeRepeating("Fire", 0f, repeatRate);
        MaxX = MainScreenXY.MaxX;
        MaxY = MainScreenXY.MaxY;
        MinX = MainScreenXY.MinX;
        MinY = MainScreenXY.MinY;
        direction = Vector3.left;
    }

    void Update()
    {
        if (transform.position.x > MaxX)
        {
            direction = Vector3.left;
        }
        else if (transform.position.x < MinX)
        {
            direction = Vector3.right;
        }

        enemyPlane.Translate(direction * speed * Time.deltaTime);
        //OnTriggerEnter2D( bullet);
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            //coll.gameObject.SetActive(false );
            //Damage(1);
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~tomorow Write!!~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
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
    public override void Damage(int val)
    {
        Health -= val;
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
        print("MainEnemy" + Health);
    }
}
