using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitEffect : MonoBehaviour
{
    public GameObject hitImage;
    public Animator anim;

    public float imageAlpha;
    public float speed = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (imageAlpha > 0)
        {
            imageAlpha -= Time.deltaTime * speed;
        }
        hitImage.GetComponent<Image>().color = new Color(hitImage.GetComponent<Image>().color.r, hitImage.GetComponent<Image>().color.g, hitImage.GetComponent<Image>().color.b, imageAlpha);
    }

    public void onHitPlayer()
    {
        imageAlpha = 1;
        anim.SetTrigger("shake");
    }




    //IEnumerator wait()
    //{
    //    yield return new WaitForSeconds(1f);
    //    StartCoroutine(wait());
    //    StartCoroutine(flashScreen());
    //}

    //public IEnumerator flashScreen()
    //{
    //    hitImage.SetActive(true);
    //    yield return new WaitForSeconds(0.2f);
    //    hitImage.SetActive(false);
    //}
}
