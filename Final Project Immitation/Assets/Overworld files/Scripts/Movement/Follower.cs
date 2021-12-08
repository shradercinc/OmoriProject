using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public int Distance = 5;
    private Transform pos;
    private SpriteRenderer ren;
    public int direct = 3;

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

    public float animWalkT = 0.0f;
    public float animWalkTM = 0.5f;

    public int myPosition = 1;
    public Transform P1pos;
    public Transform P2pos;
    public Transform P3pos;
    public Transform P4pos;

    private void Awake()
    {
        pos = GetComponent<Transform>();
        ren = GetComponent<SpriteRenderer>();

        InfoCarry info = FindObjectOfType<InfoCarry>().GetComponent<InfoCarry>();
        pos.transform.position = info.playerPosition;
    }
    private void FixedUpdate()
    {
        pos.transform.position = new Vector3(LeadMovement.PrevPos[Distance].x, LeadMovement.PrevPos[Distance].y, LeadMovement.PrevPos[Distance].y);
        animWalkT++;

        if (animWalkT > animWalkTM * 50)
        {
            animWalkT = 0.0f;
        }

        if (direct == 1)
        {
            if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
            {
                ren.sprite = up1;
            }
            else
            {

                if (animWalkT < (animWalkTM / 2) * 50)
                {
                    ren.sprite = up2;
                }
                else
                {
                    ren.sprite = up3;
                }
            }
        }
        if (direct == 2)
        {
            if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
            {
                ren.sprite = left1;
            }
            else
            {

                if (animWalkT < (animWalkTM / 2) * 50)
                {
                    ren.sprite = left2;
                }
                else
                {
                    ren.sprite = left3;
                }
            }
        }
        if (direct == 3)
        {
            if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
            {
                ren.sprite = down1;
            }
            else
            {

                if (animWalkT < (animWalkTM / 2) * 50)
                {
                    ren.sprite = down2;
                }
                else
                {
                    ren.sprite = down3;
                }
            }
        }
        if (direct == 4)
        {
            if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
            {
                ren.sprite = right1;
            }
            else
            {

                if (animWalkT < (animWalkTM / 2) * 50)
                {
                    ren.sprite = right2;
                }
                else
                {
                    ren.sprite = right3;
                }
            }
        }

    }

    private void Update()
    {
        if (LeadMovement.PrevPos[Distance].x - (LeadMovement.PrevPos[Distance - 1].x) > 0)
        {
            direct = 2;
        }
        if (LeadMovement.PrevPos[Distance].x - (LeadMovement.PrevPos[Distance - 1].x) < 0)
        {
            direct = 4;
        }
        if (LeadMovement.PrevPos[Distance].y - (LeadMovement.PrevPos[Distance - 1].y) > 0)
        {
            direct = 3;
        }
        if (LeadMovement.PrevPos[Distance].y - (LeadMovement.PrevPos[Distance - 1].y) < 0)
        {
            direct = 1;
        }
    }
}
