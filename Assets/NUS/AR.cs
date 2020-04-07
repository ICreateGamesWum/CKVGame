using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AR : MonoBehaviour
{
    public int gunDamage = 1;
    public float fireRate = 0.125f;
    public float weaponRange = 150f;
    public float hitForce = 1000f;

    public Camera fpsCam;
    private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);
    //private AudioSource gunAudio;                                                                              
    private float nextFire;

    public GameObject muzzleFlash;
    public GameObject muzzleFlashScoped;
    public float flashTime;

    public float zoom;
    public static bool Scoping;

    public GameObject normalArms;
    public GameObject scopedArms;

    //Reload System
    public float maxClipAmount = 30f;
    public float curInClip;
    public float reloadTime = 3.5f;
    public float startReloadTime = 3.5f;
    public static bool Reloading;

    public GameObject reloadingText;
    public Text clip;
    public Text clipScope;
    public GameObject emptyText;

    void Start()
    {
        //gunAudio = GetComponent<AudioSource>();
        zoom = 60;
    }

    void Update()
    {

        clip.text = curInClip.ToString();
        clipScope.text = curInClip.ToString();

        if(curInClip <= 0 && !Reloading)
        {
            emptyText.SetActive(true);
        }
        else
        {
            emptyText.SetActive(false);
        }

        if (Reloading)
        {
            Scoping = false;
        }

        fpsCam.GetComponent<Camera>().fieldOfView = zoom;

        if (Input.GetMouseButton(1) && !Reloading)
        {
            Scoping = true;
        }
        else
        {
            Scoping = false;
        }

        if (Scoping)
        {
            scopedArms.SetActive(true);
            normalArms.SetActive(false);
            if (zoom > 40)
            {
                zoom -= Time.deltaTime * 240;
            }
            else if (zoom < 40)
            {
                zoom = 40;
            }
        }
        if (!Scoping)
        {
            scopedArms.SetActive(false);
            normalArms.SetActive(true);
            if (zoom < 60)
            {
                zoom += Time.deltaTime * 240;
            }
            else if (zoom > 60)
            {
                zoom = 60;
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
        }

        if (Input.GetMouseButton(0) && Time.time > nextFire)
        {
            if (curInClip > 0)
            {
                // Update the time when our player can fire next
                nextFire = Time.time + fireRate;

                // Create a vector at the center of our camera's viewport
                Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

                // Declare a raycast hit to store information about what our raycast has hit
                RaycastHit hit;

                StartCoroutine(Flash());

                curInClip -= 1;

                // Check if our raycast has hit anything
                if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange))
                {
                    Debug.Log("HIT");

                    if (hit.transform.gameObject.CompareTag("Enemy"))
                    {
                        //hit.transform.gameObject.GetComponent<Enemy>().Hit(gunDamage);
                    }

                    // Get a reference to a health script attached to the collider we hit
                    //ShootableBox health = hit.collider.GetComponent<ShootableBox>();

                    // If there was a health script attached
                    //if (health != null)
                    //{
                    // Call the damage function of that script, passing in our gunDamage variable
                    //    health.Damage(gunDamage);
                    //}

                    // Check if the object we hit has a rigidbody attached
                    if (hit.rigidbody != null && !hit.transform.gameObject.CompareTag("Enemy"))
                    {
                        // Add force to the rigidbody we hit, in the direction from which it was hit
                        hit.rigidbody.AddForce(-hit.normal * hitForce);
                    }

                    //Instantiate(lit, hit.transform.position, hit.transform.rotation);
                }
                else
                {
                    // If we did not hit anything, set the end of the line to a position directly in front of the camera at the distance of weaponRange
                }
            }
            else
            {
                //make clicking sound of empty clip
            }
        }
    }

    public IEnumerator Flash()
    {
        if (!Scoping)
        {
            muzzleFlash.SetActive(true);
            yield return new WaitForSeconds(flashTime);
            muzzleFlash.SetActive(false);
        }

        else if(Scoping)
        {
            muzzleFlashScoped.SetActive(true);
            yield return new WaitForSeconds(flashTime);
            muzzleFlashScoped.SetActive(false);
        }
    }

    public IEnumerator Reload()
    {
        reloadingText.SetActive(true);
        Scoping = false;
        Reloading = true;
        yield return new WaitForSeconds(reloadTime);
        curInClip = maxClipAmount;
        Reloading = false;
        reloadingText.SetActive(false);
    }
}
