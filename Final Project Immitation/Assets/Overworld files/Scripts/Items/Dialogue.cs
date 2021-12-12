using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TMP_Text dialogueBox;
    public List<string> dialogue;
    bool dialogueEnable = true;
    LeadMovement omori;
    GameObject parent;

    private void Awake()
    {
        omori = GameObject.Find("PartyLead").GetComponent<LeadMovement>();
        parent = gameObject.transform.parent.gameObject;
        parent.gameObject.SetActive(false);
    }

    IEnumerator AddDescription(string x)
    {
        dialogueBox.text = "";
        bool next = true;

        for (int i = 0; i<x.Length; i++)
        {
            dialogueBox.text += x[i];
            yield return new WaitForSeconds(0.01f);
        }
        while (next)
        {
            if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X))
                next = false;
            else
                yield return null;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Inside of " + parent.name + " 's hitbox.");
        if (collision.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.Z) && dialogueEnable)
        {
            StartCoroutine(DisplayDialogue());
        }
    }

    public IEnumerator DisplayDialogue()
    {
        dialogueEnable = false;
        dialogueBox.gameObject.transform.parent.gameObject.SetActive(true);
        omori.inOverWorld = false;

        for (int i = 0; i<dialogue.Count; i++)
        {
            yield return AddDescription(dialogue[i]);
        }

        dialogueEnable = true;
        dialogueBox.gameObject.transform.parent.gameObject.SetActive(false);
        omori.inOverWorld = true;
    }

}
