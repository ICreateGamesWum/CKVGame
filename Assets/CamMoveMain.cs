using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMoveMain : MonoBehaviour
{
    public int cameraDragSpeed = 50;

    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            float speed = cameraDragSpeed * Time.deltaTime;
            Camera.main.transform.position -= new Vector3(Input.GetAxis("Mouse Y") * speed, 0, Input.GetAxis("Mouse X") * -speed);
        }
    }
}
