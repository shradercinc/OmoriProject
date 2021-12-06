using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCode : MonoBehaviour
{
    private SpriteRenderer ren;
    public bool cansee = false; 
    // Start is called before the first frame update
    void Awake()
    {
        ren = GetComponent<SpriteRenderer>();
        if (cansee == false)
        {
            ren.enabled = false;
        }
        else 
        {
            ren.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
