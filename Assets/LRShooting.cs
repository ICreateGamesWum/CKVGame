using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LRShooting : MonoBehaviour
{
    public float startTimeBTWShots;
    public float curTimeBTWShots;

    public Transform shotPoint;
    public GameObject bulletPrefab;

    public static bool canShoot = true;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (curTimeBTWShots > 0)
        {
            curTimeBTWShots -= Time.deltaTime;
        }

        if (Input.GetMouseButton(0))
        {
            if (curTimeBTWShots <= 0 && canShoot)
            {
                curTimeBTWShots = startTimeBTWShots;
                //Vector3 spawnPos = new Vector3(shotPoint.transform.position.x, shotPoint.transform.position.y, shotPoint.transform.position.z);
                GameObject bullet = Instantiate(bulletPrefab, shotPoint.transform.position, shotPoint.transform.rotation);
                anim.SetTrigger("shot");
            }
        }
    }
}
