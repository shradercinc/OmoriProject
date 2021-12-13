using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public List<GameObject> walls;
    bool on = true;
    InfoCarry info;
    GameObject parent;
    public Sprite OffSprite;
    public Sprite OnSprite;
    private SpriteRenderer spr;
    public Sprite SwitchOffSprite;
    public Sprite SwitchOnSprite;
    private bool input = true;

    private void Awake()
    {
        info = FindObjectOfType<InfoCarry>().GetComponent<InfoCarry>();
        parent = gameObject.transform.parent.gameObject;
        spr = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        input = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
       // Debug.Log("Inside of " + parent.name + "'s hitbox.");
        if (collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("touching player");
            if (on)
            {
                //Debug.Log("Switch is On");
                if (input == true)
                {
                    spr.sprite = SwitchOffSprite;
                    //Debug.Log($"switching off spikes");
                    input = false;
                    on = false;
                    GameObject parent = gameObject.transform.parent.gameObject;
                    if (info.disable.Contains(gameObject.name))
                    {
                        info.disable.Remove(gameObject.name);
                    }
                    DisableSpikes();
                }

            }
        }
        if (collision.gameObject.CompareTag("Player") && on == false && input == true)
        {
            spr.sprite = SwitchOnSprite;
            input = false;
            //Debug.Log($"switching on spikes");
            on = true;
            GameObject parent = gameObject.transform.parent.gameObject;
            info.disable.Add(parent.name);
            EnableSpikes();
        }
    }

    public void EnableSpikes()
    {
        for (int i = 0; i < walls.Count; i++)
        {
            if (walls[i].GetComponent<BoxCollider2D>().enabled == false)
            {
                Debug.Log("enabling walls");
                walls[i].GetComponent<BoxCollider2D>().enabled = true;
                walls[i].GetComponent<SpriteRenderer>().sprite = OnSprite;
            }else if (walls[i].GetComponent<BoxCollider2D>().enabled == true)
            {
                Debug.Log("disabling walls");
                walls[i].GetComponent<BoxCollider2D>().enabled = false;
                walls[i].GetComponent<SpriteRenderer>().sprite = OffSprite;
            }

            //walls[i].changeSprite;
        }
    }
    public void DisableSpikes()
    {
        for (int i = 0; i<walls.Count; i++)
        {
            if (walls[i].GetComponent<BoxCollider2D>().enabled == false)
            {
                Debug.Log("enabling walls");
                walls[i].GetComponent<BoxCollider2D>().enabled = true;
                walls[i].GetComponent<SpriteRenderer>().sprite = OnSprite;
            } else if (walls[i].GetComponent<BoxCollider2D>().enabled == true)
            {
                Debug.Log("disabling walls");
                walls[i].GetComponent<BoxCollider2D>().enabled = false;
                walls[i].GetComponent<SpriteRenderer>().sprite = OffSprite;
            }
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            input = true;
        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            input = false;
        }
    }
}
