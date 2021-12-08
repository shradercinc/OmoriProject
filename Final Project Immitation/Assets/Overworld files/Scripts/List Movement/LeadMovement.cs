using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeadMovement : MonoBehaviour
{
    public static float speed = 7.5f;

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

    private Transform pos;
    private SpriteRenderer ren;
    public static List<Vector2> PrevPos = new List<Vector2>();
    public int ListLen = 200;
    private int listPos = 0;
    public int direct = 3;
    public float animWalkT = 0.0f;
    public float animWalkTM = 0.5f;

    public int layerOrder = 0;
    public int myPosition = 1;
    public Transform P1pos;
    public Transform P2pos;
    public Transform P3pos;
    public Transform P4pos;
    public float spawnx = 0;
    public float spawny = 0;
    private Rigidbody2D rb;

    float movex = 1.0f;
    float movey = 1.0f;

    public void Awake()
    {
        pos = GetComponent<Transform>();
        ren = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        InfoCarry info = FindObjectOfType<InfoCarry>().GetComponent<InfoCarry>();
        pos.transform.position = info.playerPosition;

        while(listPos < ListLen)
        {
            PrevPos.Add(new Vector2(spawnx, spawny));
            listPos++;
        }
        for (int i = 0; i < PrevPos.Count; i++)
        {
            //Debug.Log($"number {i} contains: {PrevPos[i]}");
        }
        for (int i = 0; i < info.delete.Count; i++)
        {
            GameObject nextDelete = GameObject.Find(info.delete[i]);
            nextDelete.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
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
                rb.velocity = new Vector2(0, 0);
            }
            else
            {
                rb.velocity = new Vector2(0, speed);
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
                rb.velocity = new Vector2(0, 0);
            }
            else
            {
                rb.velocity = new Vector2(-speed, 0);
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
                rb.velocity = new Vector2(0, 0);
            }
            else
            {
                rb.velocity = new Vector2(0, -speed);
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
                rb.velocity = new Vector2(0, 0);
            }
            else
            {
                rb.velocity = new Vector2(speed, 0);
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
        pos.transform.position = new Vector3(pos.transform.position.x, pos.transform.position.y, pos.transform.position.y);
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            updateList();
        }
    }

    void updateList()
    {
        for (int i = PrevPos.Count - 1; i >= 0; i--)
        {
            if (i != 0)
            {
                PrevPos[i] = PrevPos[i - 1];
            }
            else
            {
                PrevPos[i] = new Vector2(pos.transform.position.x, pos.transform.position.y);
            }
        }
        //Debug.Log($"position 0: {PrevPos[0]}");


    }

    void Update()
    {
        movex = Input.GetAxis("Horizontal");
        movey = Input.GetAxis("Vertical");

        /*
        if (pos.transform.position.y < P1pos.transform.position.y)
        {
            layerOrder += 1;
        }
        if (pos.transform.position.y < P2pos.transform.position.y)
        {
            layerOrder += 1;
        }
        if (pos.transform.position.y < P3pos.transform.position.y)
        {
            layerOrder += 1;
        }
        if (pos.transform.position.y < P4pos.transform.position.y)
        {
            layerOrder += 1;
        }
        ren.sortingOrder = layerOrder;
        layerOrder = 0;
        */




        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < PrevPos.Count; i++)
            {
                Debug.Log($"position {i} has value {PrevPos[i]}");
            }
        }


        if (Input.GetKeyDown(KeyCode.W) )
        {
            direct = 1;

        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            direct = 2;

        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            direct = 3;

        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            direct = 4;

        }
        /*
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
        */
    }
}
