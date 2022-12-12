using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XbowScript : MonoBehaviour
{
    public GameObject bullet;
    public GameObject spawnRight;
    public GameObject spawnLeft;

    public bool canShoot = true;

    void Start()
    {
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        print(Time.time);
        yield return new WaitForSeconds(1);
        print(Time.time);
        canShoot = true;
        spawnBullet();
    }

    void spawnBullet()
    {
        if (canShoot == true)
        {
            GameObject go = GameObject.Instantiate(bullet, spawnLeft.transform.position, Quaternion.identity);
            go.GetComponent<Bullet>().xSpeed = -7.0f;
            canShoot = false;
            StartCoroutine(Timer());
            StopCoroutine(Timer());
        }   
    }
}