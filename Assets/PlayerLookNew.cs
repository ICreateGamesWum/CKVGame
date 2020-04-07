using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookNew : MonoBehaviour
{
    Vector2 mouseLook;
    Vector2 smoothV;
    [SerializeField]
    public static float sensitivity = 5.0f;
    public float smoothing = 2.0f;

    public GameObject character;
    //public GameObject arms;
    public GameObject cam;
    //public GameObject playerArms;

    float xAxisClamp = 0f;

    public static bool moveAble = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        

        //var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        //md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        //smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
        //smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
        //mouseLook += smoothV;

        //transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        //character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);

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
