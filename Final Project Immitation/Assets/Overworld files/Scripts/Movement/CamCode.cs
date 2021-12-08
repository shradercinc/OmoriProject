using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamCode : MonoBehaviour
{

    public Transform playerPosition;
    public float camSp = 0.07f;
    private float x1 = 0;
    private float y1 = 0;

    private float px = 0;
    private float py = 0;

    public float xMax = 50;
    public float xMin = -50;
    public float yMax = 50;
    public float yMin = -50;
    // Start is called before the first frame update
    void Awake()
    {
        x1 = playerPosition.transform.position.x;
        y1 = playerPosition.transform.position.y;
        px = playerPosition.transform.position.x;
        py = playerPosition.transform.position.y;
        transform.position = new Vector3(x1, y1, -10);
    }

    private void FixedUpdate()
    {
        px = playerPosition.transform.position.x;
        py = playerPosition.transform.position.y;
        //                  CAMERA OPERATIONS

        x1 = transform.position.x;
        y1 = transform.position.y;


        if ((px < xMax && px > xMin) && (py < yMax && py > yMin)) //within all limits
        {
            transform.position = Vector3.Lerp(new Vector3(x1, y1, -10), new Vector3(px, py, -10), camSp);
            //Debug.Log("within all limits");
        }

        if (px > xMax && py < yMax && py > yMin)
        {
            transform.position = Vector3.Lerp(new Vector3(x1, y1, -10), new Vector3(xMax, py, -10), camSp);
            Debug.Log("within y limit, exceeding x limit");
        }
        if (px < xMin && py < yMax && py > yMin)
        {
            transform.position = Vector3.Lerp(new Vector3(x1, y1, -10), new Vector3(xMin, py, -10), camSp);
            Debug.Log("within y limit, bellow x limit");
        }
        if (py < yMin && px < xMax && px > xMin)
        {
            transform.position = Vector3.Lerp(new Vector3(x1, y1, -10), new Vector3(px, yMin, -10), camSp);
            Debug.Log("within x limit, bellow y limit");
        }
        if (py > yMax && px < xMax && px > xMin)
        {
            transform.position = Vector3.Lerp(new Vector3(x1, y1, -10), new Vector3(px, yMax, -10), camSp);
            Debug.Log("within x limit, exceeding y limit");
        }

        if (px > xMax && py > yMax)
        {
            transform.position = Vector3.Lerp(new Vector3(x1, y1, -10), new Vector3(xMax, yMax, -10), camSp);
            Debug.Log("exceeding x and y limit");
        }
        if (px < xMin && py > yMax)
        {
            transform.position = Vector3.Lerp(new Vector3(x1, y1, -10), new Vector3(xMin, yMax, -10), camSp);
            Debug.Log("exceeding y limit, bellow x limit");
        }
    }

    private void Update()
    {





        /*
        else
        {
            //Debug.Log("Player Higher than 0");
            if (py >= 0)
            {
                transform.position = Vector3.Lerp(new Vector3(x1, y1, -10), new Vector3(px, py, -10), 0.07f);
            }

        }
        */

    }
}
