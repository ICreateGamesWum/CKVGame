using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMoveLvl : MonoBehaviour
{
    //player
    private GameObject player;

    //boundries
    public float maxZ;
    public float minZ;

    public float maxX;
    public float minX;

    //player distance default
    public float playerMaxZ;
    public float playerMinZ;

    //current player distance
    public float curPlayerMaxZ;
    public float curPlayerMinZ;

    //distance to move cam up or down
    public float playerMaxZoffset;
    public float playerMinZoffset;

    //player distance default
    public float playerMaxX;
    public float playerMinX;

    public float speed;
    
    //extra to offset
    public bool offsetZbool;
    public int zOffset;
    public int prevZOffset;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        prevZOffset = 1;
        curPlayerMaxZ = playerMaxZ;
        curPlayerMinZ = playerMinZ;
    }

    // Update is called once per frame
    void Update()
    {
        float playerDistZ = player.transform.position.z - transform.position.z;

        if (playerDistZ >= curPlayerMaxZ)
        {
            float newZ = transform.position.z + speed * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, transform.position.y, newZ);
        }
        if (playerDistZ <= curPlayerMinZ)
        {
            float newZ = transform.position.z - speed * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, transform.position.y, newZ);
        }

        if(transform.position.z < minZ)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, minZ);
        }
        if (transform.position.z > maxZ)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, maxZ);
        }

        float playerDistX = player.transform.position.x - transform.position.x;

        if (playerDistX >= playerMaxX)
        {
            float newX = transform.position.x + speed * Time.deltaTime;
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        }
        if (playerDistX <= playerMinX)
        {
            float newX = transform.position.x - speed * Time.deltaTime;
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        }

        if (transform.position.x < minX)
        {
            transform.position = new Vector3(minX, transform.position.y, transform.position.z);
        }
        if (transform.position.x > maxX)
        {
            transform.position = new Vector3(maxX, transform.position.y, transform.position.z);
        }

        if(Input.GetKeyUp(KeyCode.Z))
        {
            offsetZ();
        }

        if (offsetZbool)
        {
            offsetZbool = false;
        }
    }

    void offsetZ()
    {
        //1 is for offset cam down, 2 for offset cam middle (no offset), 3 for cam offset top
        if(zOffset == 1)
        {
            prevZOffset = zOffset;
            zOffset = 2;
            curPlayerMinZ = playerMinZ;
            Debug.Log("made2 from 1");
        }
        else if(zOffset == 3)
        {
            prevZOffset = zOffset;
            zOffset = 2;
            curPlayerMaxZ = playerMaxZ;
            Debug.Log("made2 from 3");
        }
        else if(zOffset == 2)
        {
            if(prevZOffset == 1)////////////////////////////////////////////////////////////////////werkt nog niet, wel van 3 naar 2 en 1 naar 2 maar niet van 2 naar iets anders. vermoedelijk door prevoffsetZ dat niet werkt
            {
                prevZOffset = zOffset;
                zOffset = 3;
                curPlayerMaxZ = playerMaxZoffset;
                Debug.Log("made 3 from 2 from 1");
            }
            else if(prevZOffset == 3)
            {
                prevZOffset = zOffset;
                zOffset = 1;
                curPlayerMinZ = playerMinZoffset;
                Debug.Log("made 1 from 2 from 3");
            }
        }
    }
}
