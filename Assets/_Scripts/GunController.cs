using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject bulletPrefab; 
    public Transform shootPoint;    
    public float bulletSpeed = 20f; 
    public float fireRate = 0.5f;   

    private float nextFireTime = 0f;


    public void Shoot()
    {
        if (Time.time >= nextFireTime)
        {
            var bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = shootPoint.forward * bulletSpeed;

            Destroy(bullet, 5f);

            nextFireTime = Time.time + fireRate;
        }
    }
}
