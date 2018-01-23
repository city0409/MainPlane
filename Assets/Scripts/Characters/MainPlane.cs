using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlane : MonoBehaviour, IHealth
{
    protected float horizontalMove;
    protected float verticalMove;
    [SerializeField]
    private Transform bullet;
    [SerializeField]
    private float speed = 1f;
    private Rigidbody2D rig;
    private Renderer rend;
    [SerializeField]
    private GameObject fx;
    private Collider2D collPlane;
    private float MaxX;
    private float MaxY;
    private float MinX;
    private float MinY;
    //private Vector3 direction;
    private int health = 100;
    //private Animation anim;
    private Weapon weapon;

    //enum State { Blink, Idle }
    //private State myState;
    //private bool IsBlink;

    public delegate void OnDead();
    public event OnDead OnDeadEvent;

    public int Health
    {
        get
        {
            return health;
        }
        private set
        {
            health = value;
        }
    }

    private void Awake()
    {
        weapon = GetComponent<Weapon>();
        rig = GetComponent<Rigidbody2D>();
        rig.velocity = Vector3.up;
        rend = GetComponent<Renderer>();
        collPlane = GetComponent<Collider2D>();
        collPlane.enabled = false;
        StartCoroutine(DalayColl());
        //myState = State.Blink;

    }

    private IEnumerator DalayColl()
    {
        yield return new WaitForSeconds(2);
        collPlane.enabled = true;
    }

    private void Start()
    {
        MaxX = MainScreenXY.MaxX - 1;
        MaxY = MainScreenXY.MaxY - 1;
        MinX = MainScreenXY.MinX + 1;
        MinY = MainScreenXY.MinY + 1;
        //StartCoroutine(ChangState());
    }
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            FireOnce();
        }
        if (Input.GetButton("Fire1"))
        {
            FireStart();
        }
        ClampFrame();
    }

    private void ClampFrame()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, MinX, MaxX),
                                             Mathf.Clamp(transform.position.y, MinY, MaxY),
                                             transform.position.z);
    }
    public virtual void SetHorizontalMove(float value)
    {
        horizontalMove = value;
    }
    public virtual void SetVerticalMove(float value)
    {
        verticalMove = value;
    }
    private void FixedUpdate()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        Move(direction);
    }
    private void Move(Vector3 direction)
    {
        rig.velocity = direction * speed;
    }
   
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (!coll.gameObject.CompareTag(gameObject.tag))
        {
            Damage(100,gameObject);
            //collPlane.enabled = false;

            //audioPlane.Play();
            //rend.enabled = false;
            //Destroy(this.gameObject, audioPlane.clip.length);

        }
    }

    public void FireStart()
    {
        weapon.FireStart();
    }
    public void FireOnce()
    {
        weapon.FireOnce ();
    }
    public void Damage(int val, GameObject initiator)
    {
        if (Health <= 0) return;
        Health -= val;
        if (Health <= 0)
        {
            DestroySelf();
        }
        print("MainPlane" + Health);
    }

    public void DestroySelf()
    {
        Instantiate(fx, transform.position, Quaternion.identity);
        if (OnDeadEvent != null)
        {
            OnDeadEvent();
        }
        Destroy(gameObject);

    }
    
    //private IEnumerator ChangState()
    //{
    //    yield return new WaitForSeconds(2);
    //    IsBlink = !IsBlink;
    //    myState = State.Idle;
    //}
}
