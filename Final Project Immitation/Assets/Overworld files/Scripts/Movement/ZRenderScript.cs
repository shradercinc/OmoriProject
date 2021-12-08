using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZRenderScript : MonoBehaviour
{
    private Transform pos;
    // Start is called before the first frame update
    void Start()
    {
        pos = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        pos.transform.position = new Vector3(pos.transform.position.x, pos.transform.position.y, pos.transform.position.y);
    }
}
