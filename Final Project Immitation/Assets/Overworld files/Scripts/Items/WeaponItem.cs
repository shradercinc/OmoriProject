using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : MonoBehaviour
{
    public int itemNumber;
    InfoCarry info;
    Dialogue dialogue;
    GameObject parent;

    private void Awake()
    {
        info = FindObjectOfType<InfoCarry>().GetComponent<InfoCarry>();
        dialogue = gameObject.GetComponent<Dialogue>();
        parent = gameObject.transform.parent.gameObject;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Inside of " + parent.name + " 's hitbox.");
        if (collision.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.Z))
        {
            info.unlockedWeapons[itemNumber] = true;
            GameObject parent = gameObject.transform.parent.gameObject;
            info.delete.Add(parent.name);
            StartCoroutine(deleteMe());
        }
    }

    IEnumerator deleteMe()
    {
        yield return dialogue.DisplayDialogue();
        parent.SetActive(false);
    }
}
