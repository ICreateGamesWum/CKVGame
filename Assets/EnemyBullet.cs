using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 10000f;
    private Rigidbody rigidbody;

    public GameObject bulletImpact;

    public float damage = 1f;

    //public LayerMask wallLayer;

    //public bool hitWall()
    //{
    //    return Physics.Raycast(transform.position, -Vector3.up, GetComponent<Collider>().bounds.extents.y + 0.1f, wallLayer);
    //}

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        //rigidbody.AddForce(transform.forward * speed * Time.deltaTime);
        Destroy(this.gameObject, 1f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigidbody.velocity += speed * Time.deltaTime * transform.forward;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerEvents>().hp -= damage;
            //StartCoroutine(collision.gameObject.GetComponent<HitEffect>().flashScreen());
            collision.gameObject.GetComponent<HitEffect>().onHitPlayer();
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }
    }
}
