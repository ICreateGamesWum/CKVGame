using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunWallFix : MonoBehaviour
{

    //private GameObject flashlight;

    // Start is called before the first frame update
    void Start()
    {
        //flashlight = GameObject.FindGameObjectWithTag("flash");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Wall"))
        {
            Shooting.canShoot = false;
            LRShooting.canShoot = false;
            Shotgun.canShoot = false;
            PistolShooting.canShoot = false;
            //flashlight.SetActive(false);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            Shooting.canShoot = false;
            //flashlight.SetActive(false);
            LRShooting.canShoot = false;
            PistolShooting.canShoot = false;
            Shotgun.canShoot = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Shooting.canShoot = true;
        //flashlight.SetActive(true);
        LRShooting.canShoot = true;
        Shotgun.canShoot = true;
        PistolShooting.canShoot = true;
    }
}
