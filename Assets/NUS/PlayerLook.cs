using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    //vectors used to make the player look to the right location
    Vector2 mouseLook;
    Vector2 smoothV;

    //speed at wich the camera rotates when looking
    public float sensitivity = 3.0f;
    public float smoothing = 2.0f;

    //player object to rotate that not only the head rotates
    public GameObject character;
    //public GameObject arms;

    //this object / main camera
    public GameObject cam;

    //this float is used to clamp rotation
    float xAxisClamp = 0f;

    //to indicate the player is able to move and doesnt have a menu opened
    public static bool moveAble = true;

    // Update is called once per frame
    void FixedUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        float rotAmountX = mouseX * sensitivity;
        float rotAmountY = mouseY * sensitivity;

        xAxisClamp -= rotAmountY;

        Vector3 rotPlayer = cam.transform.rotation.eulerAngles;
        Vector3 rotPlay = character.transform.rotation.eulerAngles;
        //Vector3 rotArms = arms.transform.rotation.eulerAngles;

        rotPlayer.x -= rotAmountY;
        //rotArms.x -= rotAmountY;
        rotPlay.y += rotAmountX;


        if (xAxisClamp > 90)
        {
            xAxisClamp = 90;
            rotPlayer.x = 90;
            //rotArms.x = 90;
        }
        else if (xAxisClamp < -90)
        {
            xAxisClamp = -90;
            rotPlayer.x = 270;
            //rotArms.x = 270;
        }

        if (moveAble)
        {
            //arms.transform.rotation = Quaternion.Euler(rotArms);
            cam.transform.rotation = Quaternion.Euler(rotPlayer);
            character.transform.rotation = Quaternion.Euler(rotPlay);
        }
        RotateCamera();
    }

    void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        float rotAmountX = mouseX * sensitivity;
        float rotAmountY = mouseY * sensitivity;

        xAxisClamp -= rotAmountY;

        Vector3 rotPlayer = cam.transform.rotation.eulerAngles;
        Vector3 rotPlay = character.transform.rotation.eulerAngles;
        //Vector3 rotArms = arms.transform.rotation.eulerAngles;

        rotPlayer.x -= rotAmountY;
        //rotArms.x -= rotAmountY;
        rotPlay.y += rotAmountX;


        if (xAxisClamp > 90)
        {
            xAxisClamp = 90;
            rotPlayer.x = 90;
            //rotArms.x = 90;
        }
        else if (xAxisClamp < -90)
        {
            xAxisClamp = -90;
            rotPlayer.x = 270;
            //rotArms.x = 270;
        }

        if (moveAble)
        {
            //arms.transform.rotation = Quaternion.Euler(rotArms);
            cam.transform.rotation = Quaternion.Euler(rotPlayer);
            character.transform.rotation = Quaternion.Euler(rotPlay);
        }
    }
}
