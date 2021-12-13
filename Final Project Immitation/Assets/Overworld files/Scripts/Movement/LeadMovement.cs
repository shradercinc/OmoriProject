using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LeadMovement : MonoBehaviour
{
    public static float speed = 7.5f;
    public TMP_Text menu;
    public TMP_Text descriptions;
    public List<string> gameWeapons;
    public List<string> gameDescriptions;
    InfoCarry info;

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
    public bool inOverWorld = true;

    float movex = 1.0f;
    float movey = 1.0f;

    public void Awake()
    {
        pos = GetComponent<Transform>();
        ren = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        info = FindObjectOfType<InfoCarry>().GetComponent<InfoCarry>();
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
            if (nextDelete != null)
                nextDelete.SetActive(false);
        }
        for (int i = 0; i<info.disable.Count; i++)
        {
            GameObject nextDisable = GameObject.Find(info.disable[i]);
            if (nextDisable != null)
                nextDisable.GetComponent<Switch>().DisableSpikes();
        }
    }

    private void FixedUpdate()
    {
        animWalkT++;
        if (animWalkT > animWalkTM * 50)
        {
            animWalkT = 0.0f;
        }
        if (inOverWorld == true)
        {
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

    IEnumerator Menu()
    {
        inOverWorld = false;
        yield return ChoosePlayer();
        menu.gameObject.transform.parent.gameObject.SetActive(false);
        descriptions.gameObject.transform.parent.gameObject.SetActive(false);
        inOverWorld = true;
    }

    IEnumerator ChoosePlayer()
    {
        bool waiting = true;
        bool undo = true;
        menu.gameObject.transform.parent.gameObject.SetActive(true);

        while (waiting)
        {
            if (undo)
            {
                yield return new WaitForSeconds(0.1f);
                undo = false;
                menu.text = "";
                menu.text += "Change your weapons.";
                menu.text += "\n1: Omori";
                menu.text += "\n2: Aubrey";
                menu.text += "\n3: Kel";
                menu.text += "\n4: Hero";
                menu.text += "\nSpace: Exit";
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                yield return ChooseWeapon(0);
                undo = true;
                yield return new WaitForSeconds(0.1f);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                yield return ChooseWeapon(1);
                undo = true;
                yield return new WaitForSeconds(0.1f);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                yield return ChooseWeapon(2);
                yield return new WaitForSeconds(0.1f);
                undo = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                yield return ChooseWeapon(3);
                undo = true;
                yield return new WaitForSeconds(0.1f);
            }
            else if (Input.GetKeyDown(KeyCode.Space))
                waiting = false;
            else
                yield return null;
        }
    }

    IEnumerator ChooseWeapon(int n)
    {
        bool waiting = true;
        bool undo = true;

        while (waiting)
        {
            if (undo)
            {
                yield return new WaitForSeconds(0.1f);
                undo = false;
                menu.text = "";
                descriptions.text = "";

                switch (n)
                {
                    case (0):
                        menu.text += "Omori's Weapon: " + info.playerWeapons[n];
                        break;
                    case (1):
                        menu.text += "Aubrey's Weapon: " + info.playerWeapons[n];
                        break;
                    case (2):
                        menu.text += "Kel's Weapon: " + info.playerWeapons[n];
                        break;
                    case (3):
                        menu.text += "Hero's Weapon: " + info.playerWeapons[n];
                        break;
                }

                menu.text += "\n1: Bring Nothing";

                if (info.unlockedWeapons[n * 2])
                {
                    menu.text += "\n2: " + gameWeapons[n * 2];
                    descriptions.gameObject.transform.parent.gameObject.SetActive(true);
                    descriptions.text += gameWeapons[n*2] + ": " + gameDescriptions[n * 2] + "\n";
                }
                else
                    menu.text += "\n???";

                if (info.unlockedWeapons[n * 2])
                {
                    menu.text += "\n3: " + gameWeapons[n * 2 + 1];
                    descriptions.gameObject.transform.parent.gameObject.SetActive(true);
                    descriptions.text += gameWeapons[n * 2 + 1] + ": " + gameDescriptions[n * 2 + 1] + "\n";
                }
                else
                    menu.text += "\n???";

                menu.text += "\nSpace: Exit";
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                undo = true;
                info.playerWeapons[n] = "None";
                yield return new WaitForSeconds(0.1f);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (info.unlockedWeapons[n * 2])
                {
                    undo = true;
                    info.playerWeapons[n] = gameWeapons[n * 2];
                    yield return new WaitForSeconds(0.1f);
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (info.unlockedWeapons[n * 2 + 1])
                {
                    undo = true;
                    info.playerWeapons[n] = gameWeapons[n * 2 + 1];
                    yield return new WaitForSeconds(0.1f);
                }
            }
            else if (Input.GetKeyDown(KeyCode.Space))
                waiting = false;
            else
                yield return null;
        }

        descriptions.gameObject.transform.parent.gameObject.SetActive(false);
    }

    void Update()
    {
        movex = Input.GetAxis("Horizontal");
        movey = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Menu());
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
    }
}
