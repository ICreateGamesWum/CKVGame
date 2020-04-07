using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerEvents : MonoBehaviour
{
    public float hp = 10;
    public float maxHp = 10;
    public Image hpBar;
    public float pickupHp = 10f;

    public int laptops;
    public TextMeshProUGUI laptopText;

    public Animator menuOpenCloseAnim;
    public bool openMenu;
    // Start is called before the first frame update
    void Start()
    {
        laptopText.text = laptops.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        hpBar.fillAmount = hp / maxHp;

        if(hp <= 0)
        {
            Time.timeScale = 0;
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (!openMenu)
            {
                StartCoroutine(OpenMenu());
            }
        }

    }

    public IEnumerator OpenMenu()
    {
        openMenu = true;
        menuOpenCloseAnim.SetBool("Open", true);
        yield return new WaitForSeconds(0.3f);
        Time.timeScale = 0;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Laptop"))
        {
            Destroy(collision.gameObject);
            laptops++;
            laptopText.text = laptops.ToString();
        }
        else if(collision.gameObject.CompareTag("HP"))
        {
            Destroy(collision.gameObject);
            if(hp + pickupHp <= maxHp)
            {
                hp += pickupHp;
            }
            else if(hp != maxHp)
            {
                float hpDiff = maxHp - hp;
                hp += hpDiff;
            }
        }
    }
}
