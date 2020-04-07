using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LRBullet : MonoBehaviour
{
    public float speed = 500f;
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

    //void onCollisionHit()
    //{
    //    if (hitWall())
    //    {
    //        RaycastHit hit;
    //        if (Physics.Raycast(transform.position, -Vector3.up, GetComponent<Collider>().bounds.extents.y + 0.1f, wallLayer))
    //        {
    //            if (Physics.Raycast(transform.position, -Vector3.up, out hit, GetComponent<Collider>().bounds.extents.y + 0.1f))
    //            {
    //                if (hit.transform.gameObject.CompareTag("Wall"))
    //                {
    //                    Destroy(this.gameObject);
    //                }
    //            }
    //        }
    //    } 
    //}

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.gameObject.CompareTag("Wall"))
    //    {
    //        Destroy(this.gameObject);
    //    }
    //    else if(collision.gameObject.CompareTag("Enemy"))
    //    {
    //        collision.gameObject.GetComponent<Enemy>().hp -= damage;
    //        collision.gameObject.GetComponent<Enemy>().gotShot = true;
    //        Instantiate(bulletImpact, transform.position, Quaternion.LookRotation(GameObject.FindGameObjectWithTag("Player").transform.position - transform.position));
    //        Destroy(this.gameObject);
    //    }
    //    else
    //    {
    //        onCollisionHit();
    //    }
    //}

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().hp -= damage;
            collision.gameObject.GetComponent<Enemy>().gotShot = true;
            Instantiate(bulletImpact, transform.position, Quaternion.LookRotation(GameObject.FindGameObjectWithTag("Player").transform.position - transform.position));
            Destroy(this.gameObject);
        }
        if (collision.gameObject.CompareTag("Boss1"))
        {
            collision.gameObject.GetComponent<BossScript>().hp -= damage;
            //collision.gameObject.GetComponent<Boss1>().gotShot = true;
            Instantiate(bulletImpact, transform.position, Quaternion.LookRotation(GameObject.FindGameObjectWithTag("Player").transform.position - transform.position));
            Destroy(this.gameObject);
        }
        if (collision.gameObject.CompareTag("portal"))
        {
            collision.gameObject.GetComponent<SummonEnemy>().hp -= damage;
            Instantiate(bulletImpact, transform.position, Quaternion.LookRotation(GameObject.FindGameObjectWithTag("Player").transform.position - transform.position));
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }
    }
}
