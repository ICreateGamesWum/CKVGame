using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public int bulletCount = 5;
    public float spreadFactor = 0.05f;
    public float fireRate = 0.5f;

    public Transform shotPoint;

    private float nextFire = 0.0f;
    public static bool canShoot = true;

    public Animator anim;
    Quaternion bulletRot;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time > nextFire && canShoot)
        {
            for (int i = 0; i < bulletCount; i++)
            {
                float randomX = Random.Range(-spreadFactor, spreadFactor);
                float randomY = Random.Range(-spreadFactor, spreadFactor);
                GameObject bullet = Instantiate(bulletPrefab, shotPoint.position, shotPoint.transform.rotation);
                bullet.transform.rotation *= Quaternion.Euler(new Vector3(randomX, randomY, 0));
            }

            nextFire = Time.time + fireRate;
            anim.SetTrigger("knockback");
        }
    }
}
