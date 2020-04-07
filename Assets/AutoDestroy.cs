using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float deathTime;
    void Start()
    {
        Destroy(this.gameObject, deathTime);
    }
}
