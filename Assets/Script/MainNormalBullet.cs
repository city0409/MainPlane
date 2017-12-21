using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainNormalBullet : MainBullet
{

    protected override void Move()
    {
        transform.Translate(Vector3.up * Time.deltaTime * bulletSpeed);
    }

}
