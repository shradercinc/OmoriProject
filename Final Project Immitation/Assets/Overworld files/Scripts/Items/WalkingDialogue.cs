using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WalkingDialogue : MonoBehaviour
{
    public TMP_Text dialogueBox;
    public List<string> dialogue;
    LeadMovement omori;
    InfoCarry info;

    private void Awake()
    {
        omori = GameObject.Find("Lead").GetComponent<LeadMovement>();
        info = FindObjectOfType<InfoCarry>().GetComponent<InfoCarry>();
        dialogueBox.gameObject.transform.parent.gameObject.SetActive(false);
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
            if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X))
                next = false;
            else
                yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(DisplayDialogue());
        }
    }

    public IEnumerator DisplayDialogue()
    {
        dialogueBox.gameObject.transform.parent.gameObject.SetActive(true);
        omori.inOverWorld = false;

        for (int i = 0; i < dialogue.Count; i++)
        {
            yield return AddDescription(dialogue[i]);
        }

        dialogueBox.gameObject.transform.parent.gameObject.SetActive(false);
        omori.inOverWorld = true;
        info.delete.Add(gameObject.name);
    }

}
