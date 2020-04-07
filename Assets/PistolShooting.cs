using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolShooting : MonoBehaviour
{
    public static bool canShoot = true;

    public Transform shotPoint;
    public GameObject bulletPrefab;

    public float fireRate = 0.1f;
    private float nextFire = 0.0f;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1") && Time.time > nextFire && canShoot)
        {
            GameObject bullet = Instantiate(bulletPrefab, shotPoint.transform.position, shotPoint.transform.rotation);
            nextFire = Time.time + fireRate;
            anim.SetTrigger("shot");
        }
    }
}
