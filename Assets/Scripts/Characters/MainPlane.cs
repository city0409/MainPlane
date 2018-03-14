using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlane : Photon.PunBehaviour, IHealth
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
    private Vector3 targetPosition;
    private Quaternion targetRotation;


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

    

    private void Start()
    {
        MaxX = MainScreenXY.MaxX - 0.3f;
        MaxY = MainScreenXY.MaxY - 0.3f;
        MinX = MainScreenXY.MinX + 0.3f;
        MinY = MainScreenXY.MinY + 0.3f;
        //StartCoroutine(ChangState());
    }
    private void Update()
    {
        if (!photonView.isMine)
        {
            SmoothMove();
        }
      
        ClampFrame();
    }

    #region Move
    public virtual void SetHorizontalMove(float value)
    {
        if (!photonView.isMine) return;
        horizontalMove = value;
    }
    public virtual void SetVerticalMove(float value)
    {
        if (!photonView.isMine) return;
        verticalMove = value;
    }
    //invoke by photon
    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            targetPosition = (Vector3)stream.ReceiveNext();
            targetRotation = (Quaternion)stream.ReceiveNext();
        }
    }
    private void SmoothMove()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, 0.25f);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.25f);
    }
    private void FixedUpdate()
    {
        if (!photonView.isMine) return;
        Vector3 direction = new Vector3(horizontalMove, verticalMove, 0);
        Move(direction);
    }
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (!coll.gameObject.CompareTag(gameObject.tag))
        {
            Damage(100,gameObject);
        }
    }
    private IEnumerator DalayColl()
    {
        yield return new WaitForSeconds(2);
        collPlane.enabled = true;
    }
    private void ClampFrame()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, MinX, MaxX),
                                             Mathf.Clamp(transform.position.y, MinY, MaxY),
                                             transform.position.z);
    }
    private void Move(Vector3 direction)
    {
        rig.velocity = direction * speed;
    }
    #endregion


    #region Fire
    public void FireStart()
    {
        weapon.FireStart();
    }
    public void FireOnce()
    {
        weapon.FireOnce ();
    }
    #endregion


    #region Health
    public void Damage(int val, GameObject initiator)
    {
        Health -= val;
        if (Health <= 0)
        {
            if (AppConst.DebugMode == false)
            {
                DestroySelf();
            }
        }
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
    #endregion

    //private IEnumerator ChangState()
    //{
    //    yield return new WaitForSeconds(2);
    //    IsBlink = !IsBlink;
    //    myState = State.Idle;
    //}
}
