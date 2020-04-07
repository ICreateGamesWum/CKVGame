using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SummonEnemy : MonoBehaviour
{
    public Image hpImage;
    public Image progressBar;

    public float progress;
    public float speed;

    public float hp = 10;
    public float maxHp = 10;

    public GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        progress += Time.deltaTime * speed;
        progressBar.fillAmount = progress;

        hpImage.fillAmount = hp / maxHp;

        if(progress >= 1)
        {
            Instantiate(enemy, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }

        if(hp<=0)
        {
            Destroy(this.gameObject);
        }
    }
}
