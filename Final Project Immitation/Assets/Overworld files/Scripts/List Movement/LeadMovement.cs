using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeadMovement : MonoBehaviour
{
    public static float speed = 5.0f;


    public Sprite down1;
    public Sprite down2;
    public Sprite down3;

    public Sprite up1;
    public Sprite up2;
    public Sprite up3;

    public Sprite left1;
    public Sprite left2;
    public Sprite left3;

    public Sprite right1;
    public Sprite right2;
    public Sprite right3;

    public Transform pos;
    public SpriteRenderer ren;
    public static List<Vector2> PrevPos = new List<Vector2>();
    public int ListLen = 200;
    private int listPos = 0;
    public void Awake()
    {
        pos = GetComponent<Transform>();
        ren = GetComponent<SpriteRenderer>();
        while(listPos < ListLen)
        {
            PrevPos.Add(new Vector2(0, 0));
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            
        }
    }
}
