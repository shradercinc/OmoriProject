using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public List<GameObject> walls;
    bool on = true;
    InfoCarry info;
    GameObject parent;

    private void Awake()
    {
        info = FindObjectOfType<InfoCarry>().GetComponent<InfoCarry>();
        parent = gameObject.transform.parent.gameObject;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Inside of " + parent.name + " 's hitbox.");
        if (collision.gameObject.CompareTag("Player") && on && Input.GetKeyDown(KeyCode.Z))
        {
            on = false;
            GameObject parent = gameObject.transform.parent.gameObject;
            info.disable.Add(parent.name);
            DisableSpikes();
        }
    }

    public void DisableSpikes()
    {
        for (int i = 0; i<walls.Count; i++)
        {
            Destroy(walls[i].GetComponent<BoxCollider2D>());
            //walls[i].changeSprite;
        }
    }

}
