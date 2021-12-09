using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    TMP_Text dialogueBox;
    public List<string> dialogue;
    bool dialogueEnable = true;
    LeadMovement omori;

    private void Awake()
    {
        omori = GameObject.Find("PartyLead").GetComponent<LeadMovement>();
        dialogueBox = GameObject.Find("Dialogue Text").GetComponent<TextMeshProUGUI>();
        dialogueBox.gameObject.SetActive(false);
    }

    IEnumerator AddDescription(string x)
    {
        dialogueBox.text = x;
        bool next = true;
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
        if (collision.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.Z) && dialogueEnable)
        {
            StartCoroutine(DisplayDialogue());
        }
    }

    IEnumerator DisplayDialogue()
    {
        dialogueEnable = false;
        //omori. = false;

        for (int i = 0; i<dialogue.Count; i++)
        {
            dialogueBox.gameObject.transform.parent.gameObject.SetActive(true);
            yield return AddDescription(dialogue[i]);
        }

        dialogueEnable = true;
        dialogueBox.gameObject.transform.parent.gameObject.SetActive(false);
        //omori. = true;
    }

}
