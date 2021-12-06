using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WASDmove : MonoBehaviour
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

    public static int globDirect = 1;
    public static float leadx = 0;
    public static float leady = 0;
    public int direct = 1;
    public float animWalkT = 0.0f;
    public float animWalkTM = 0.5f;

    private Transform pos;
    private SpriteRenderer ren;


    void Awake()
    {
        pos = GetComponent<Transform>();
        ren = GetComponent<SpriteRenderer>();
        WASDmove.leadx = pos.transform.position.x;
        WASDmove.leady = pos.transform.position.y;
    }

    IEnumerator DirSet(int num, int dir)
    {
        //Debug.Log($"starting delay");
        bool delay = false;
        float counter = 0;
        while (delay == false)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                counter++;
            }
            if (num == 2)
            {
                if (counter >= 1f * 60)
                {
                    Player2Code.P2direct = dir;
                    //Debug.Log("ending delay");
                    delay = true;
                }
            }
            if (num == 3)
            {
                if (counter >= 2f * 60)
                {
                    Player3Code.P3direct = dir;
                    //Debug.Log("ending delay");
                    delay = true;
                }
            }
            if (num == 4)
            {
                if (counter >= 3f * 60)
                {
                    Player4Code.P4direct = dir;
                    //Debug.Log("ending delay");
                    delay = true;
                }
            }
            yield return null;
        }


    }


    private void FixedUpdate()
    {
        animWalkT++;
        if (animWalkT > animWalkTM * 50)
        {
            animWalkT = 0.0f;
        }
    }

    void Update()
    {
        WASDmove.leadx = pos.transform.position.x;
        WASDmove.leady = pos.transform.position.y;

        if (Input.GetKeyDown(KeyCode.W))
        {
            direct = 1;
            StartCoroutine(DirSet(2, 1));
            StartCoroutine(DirSet(3, 1));
            StartCoroutine(DirSet(4, 1));
            //transform.Translate(Vector2.up * Time.deltaTime * speed);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            direct = 2;
            StartCoroutine(DirSet(2, 2));
            StartCoroutine(DirSet(3, 2));
            StartCoroutine(DirSet(4, 2));
            //transform.Translate(Vector2.left * Time.deltaTime * speed);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            direct = 3;
            StartCoroutine(DirSet(2, 3));
            StartCoroutine(DirSet(3, 3));
            StartCoroutine(DirSet(4, 3));
            //transform.Translate(Vector2.down * Time.deltaTime * speed);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            direct = 4;
            StartCoroutine(DirSet(2, 4));
            StartCoroutine(DirSet(3, 4));
            StartCoroutine(DirSet(4, 4));
            //transform.Translate(Vector2.right * Time.deltaTime * speed);
        }

        if (direct == 1)
        {

            if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
            {
                ren.sprite = up1;
            }
            else
            {
                transform.Translate(Vector2.up * Time.deltaTime * speed);
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
                transform.Translate(Vector2.left * Time.deltaTime * speed);
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
                transform.Translate(Vector2.down * Time.deltaTime * speed);
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
                transform.Translate(Vector2.right * Time.deltaTime * speed);
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
