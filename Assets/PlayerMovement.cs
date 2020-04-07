using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;

    public float jumpPower = 5f;

    public Rigidbody rb;

    public GameObject playerCollider;
    float distToGround;

    float translation;
    float strafe;

    public bool grounded()
    {
        return Physics.Raycast(playerCollider.transform.position, -Vector3.up, playerCollider.GetComponent<Collider>().bounds.extents.y + 0.1f);
    }

    public void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        distToGround = playerCollider.GetComponent<Collider>().bounds.extents.y;
    }

    public void FixedUpdate()
    {
        if (grounded())
        {
            translation = Input.GetAxis("Vertical") * speed;
            strafe = Input.GetAxis("Horizontal") * speed;
            translation *= Time.deltaTime;
            strafe *= Time.deltaTime;
        }

        transform.Translate(strafe, 0, translation);

        if (Input.GetKeyDown(KeyCode.Space) && grounded())
        {
            rb.AddForce(transform.up * jumpPower * 10);
        }
    }
}
