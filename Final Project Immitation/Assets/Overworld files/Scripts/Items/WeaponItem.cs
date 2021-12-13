using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : MonoBehaviour
{
    InfoCarry info;
    Dialogue dialogue;
    GameObject parent;
    bool pressedZ;

    private void Awake()
    {
        info = FindObjectOfType<InfoCarry>().GetComponent<InfoCarry>();
        dialogue = gameObject.GetComponent<Dialogue>();
        parent = gameObject.transform.parent.gameObject;
    }

    public void Update()
    {
        pressedZ = (Input.GetKeyDown(KeyCode.Z));
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Inside of " + gameObject.transform.parent.name + " 's hitbox.");
        if (collision.gameObject.CompareTag("Player") && pressedZ)
        {
            info.UnlockWeapon(gameObject.name);
            GameObject parent = gameObject.transform.parent.gameObject;
            info.delete.Add(parent.name);
            StartCoroutine(DeleteMe());
        }
    }

    IEnumerator DeleteMe()
    {
        yield return dialogue.DisplayDialogue();
        parent.SetActive(false);
    }
}
