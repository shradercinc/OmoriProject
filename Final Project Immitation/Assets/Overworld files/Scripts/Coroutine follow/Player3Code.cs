using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player3Code : MonoBehaviour
{
    public static int P3direct = 0;

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

    private float animWalkT = 0;
    public float animWalkTM = 0.5f;
    private Transform pos;
    private SpriteRenderer ren;

    void Start()
    {
        pos = GetComponent<Transform>();
        ren = GetComponent<SpriteRenderer>();
        pos.transform.position = new Vector3(WASDmove.leadx, WASDmove.leady, -1);
    }

    private void FixedUpdate()
    {
        animWalkT++;
        if (animWalkT > animWalkTM * 50)
        {
            animWalkT = 0.0f;
        }

    }

    IEnumerator move()
    {
 

        yield return null;
    }

    void Update()
    {
        if (P3direct == 1)
        {

            if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
            {
                ren.sprite = up1;
            }
            else
            {
                transform.Translate(Vector2.up * Time.deltaTime * WASDmove.speed);
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
        if (P3direct == 2)
        {

            if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
            {
                ren.sprite = left1;
            }
            else
            {
                transform.Translate(Vector2.left * Time.deltaTime * WASDmove.speed);
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
        if (P3direct == 3)
        {
            if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
            {
                ren.sprite = down1;
            }
            else
            {
                transform.Translate(Vector2.down * Time.deltaTime * WASDmove.speed);
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
        if (P3direct == 4)
        {
            if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
            {
                ren.sprite = right1;
            }
            else
            {
                transform.Translate(Vector2.right * Time.deltaTime * WASDmove.speed);
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
}
