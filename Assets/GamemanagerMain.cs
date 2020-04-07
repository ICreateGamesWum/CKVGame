using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamemanagerMain : MonoBehaviour
{
    public bool selected;
    public GameObject target;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.CompareTag("Building"))
                {
                    if (selected)
                    {
                        target.GetComponent<Outline>().enabled = false;
                        target = hit.transform.gameObject;
                        target.GetComponent<Outline>().enabled = true;
                    }
                    else
                    {
                        target = hit.transform.gameObject;
                        selected = true;
                        target.GetComponent<Outline>().enabled = true;
                    }
                }
                else if(!hit.transform.gameObject.CompareTag("Building"))
                {
                    if(selected)
                    {
                        target.GetComponent<Outline>().enabled = false;
                        selected = false;
                        target = null;
                    }
                }
            }
            else
            {
                if (selected)
                {
                    target.GetComponent<Outline>().enabled = false;
                    selected = false;
                    target = null;
                }
            }
        }
    }
}
