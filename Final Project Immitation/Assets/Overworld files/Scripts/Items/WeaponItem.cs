using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponItem : MonoBehaviour
{
    public TMP_Text dialogueBox;
    public List<string> dialogue;
    InfoCarry info;
    bool pressedZ;
    LeadMovement omori;
    GameObject parent;
    bool dialogueEnable = true;

    private void Awake()
    {
        omori = GameObject.Find("Lead").GetComponent<LeadMovement>();
        info = FindObjectOfType<InfoCarry>().GetComponent<InfoCarry>();
        dialogueBox.gameObject.transform.parent.gameObject.SetActive(false);
        parent = gameObject.transform.parent.gameObject;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            pressedZ = true;
        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            pressedZ = false;
        }
    }

    IEnumerator AddDescription(string x)
    {
        dialogueBox.text = "";
        bool next = true;

        for (int i = 0; i < x.Length; i++)
        {
            dialogueBox.text += x[i];
            yield return new WaitForSeconds(0.01f);
        }
        while (next)
        {
            if (Input.GetKeyDown(KeyCode.Z))
                next = false;
            else
                yield return null;
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Inside of " + gameObject.transform.parent.name + " 's hitbox.");
        if (collision.gameObject.CompareTag("Player") && pressedZ && dialogueEnable)
        {
            info.UnlockWeapon(parent.name);
            info.delete.Add(parent.name);
            StartCoroutine(DisplayDialogue());
        }
    }

    public IEnumerator DisplayDialogue()
    {
        dialogueEnable = false;
        dialogueBox.gameObject.transform.parent.gameObject.SetActive(true);
        omori.inOverWorld = false;

        for (int i = 0; i < dialogue.Count; i++)
        {
            yield return AddDescription(dialogue[i]);
        }

        parent.gameObject.SetActive(false);
        dialogueBox.gameObject.transform.parent.gameObject.SetActive(false);
        omori.inOverWorld = true;
    }
}
