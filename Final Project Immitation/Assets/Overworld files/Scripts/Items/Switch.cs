using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public GameObject wall;
    InfoCarry info;

    private void Awake()
    {
        info = FindObjectOfType<InfoCarry>().GetComponent<InfoCarry>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Inside of " + gameObject.name + " 's hitbox.");
        if (collision.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.Z))
        {
            gameObject.SetActive(false);
            info.delete.Add(wall.name);

            GameObject parent = gameObject.transform.parent.gameObject;
            info.delete.Add(parent.name);
            parent.SetActive(false);
        }
    }
}