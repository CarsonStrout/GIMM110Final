using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XbowScript : MonoBehaviour
{
    public GameObject bullet;
    public GameObject spawnPosition;

    public bool canShoot = true;
    public bool moveRight;

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
            GameObject go = Instantiate(bullet, spawnPosition.transform.position, Quaternion.identity);
            if (moveRight)
            {
                go.GetComponent<Bullet>().xSpeed = 7.0f;
            }
            else
            {
                go.GetComponent<Bullet>().xSpeed = -7.0f;
            }
            canShoot = false;
            StartCoroutine(Timer());
            StopCoroutine(Timer());
        }   
    }
}