﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType { OneBullet=1, TwoBullet , ThreeBullet , FourBullet , FiveBullet }

public class Weapon : MonoBehaviour 
{
    [SerializeField]
    private GameObject bullet;

    private MainPlane mainPlane;
    private float fireTimer;
    [SerializeField]
    private float fireRate = 0.3f;

    private WeaponType currentWeaponType=WeaponType.OneBullet ;
    public WeaponType CurrentWeaponType { get { return currentWeaponType; }private  set { currentWeaponType = value; } }

    private void Awake () 
	{
        mainPlane = GetComponent<MainPlane>();
	}
	
	public  void FireStart () 
	{
        if (mainPlane.Health <= 0) return;

        fireTimer += Time.deltaTime;
        if (fireTimer >fireRate )
        {
            CreateBullet();
            //Instantiate(bullet, transform.position, Quaternion.identity);
            fireTimer = 0;
        }
	}

    public void FireOnce()
    {
        if (mainPlane.Health <= 0) return;
        CreateBullet();
        //Instantiate(bullet, transform.position, Quaternion.identity);

        fireTimer = 0;
    }

    public void ChangeWeaponType(WeaponType weaponType)
    {
        currentWeaponType = weaponType;
    }
    public void ChangeWeaponType(int weaponTypeID)
    {
        if (weaponTypeID > (int)WeaponType.FiveBullet) return;

        currentWeaponType =(WeaponType) weaponTypeID;

    }

    private void CreateBullet()
    {
        switch (currentWeaponType )
        {
            case WeaponType.OneBullet:
                Instantiate(bullet, transform.position, Quaternion.identity);
                break;
            case WeaponType.TwoBullet:
                Instantiate(bullet, transform.position + new Vector3 (0.2f,0f,0f), Quaternion.identity);
                Instantiate(bullet, transform.position + new Vector3(-0.2f, 0f, 0f), Quaternion.identity);
                break;
            case WeaponType.ThreeBullet:
                Instantiate(bullet, transform.position, Quaternion.AngleAxis (15,Vector3.back ));
                Instantiate(bullet, transform.position, Quaternion.AngleAxis(0, Vector3.back));
                Instantiate(bullet, transform.position, Quaternion.AngleAxis(-15, Vector3.back));
                break;
            case WeaponType.FourBullet:
                Instantiate(bullet, transform.position + new Vector3(0.3f, 0f, 0f), Quaternion.identity);
                Instantiate(bullet, transform.position + new Vector3(-0.3f, 0f, 0f), Quaternion.identity);
                Instantiate(bullet, transform.position + new Vector3(0.15f, 0f, 0f), Quaternion.identity);
                Instantiate(bullet, transform.position + new Vector3(-0.15f, 0f, 0f), Quaternion.identity);
                break;
            case WeaponType.FiveBullet:
                Instantiate(bullet, transform.position, Quaternion.AngleAxis(30, Vector3.back));
                Instantiate(bullet, transform.position, Quaternion.AngleAxis(15, Vector3.back));
                Instantiate(bullet, transform.position, Quaternion.AngleAxis(0, Vector3.back));
                Instantiate(bullet, transform.position, Quaternion.AngleAxis(-15, Vector3.back));
                Instantiate(bullet, transform.position, Quaternion.AngleAxis(-30, Vector3.back));
                break;
            default:
                break;
        }
    }
}
