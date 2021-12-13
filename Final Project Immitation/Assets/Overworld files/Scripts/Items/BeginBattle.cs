using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeginBattle : MonoBehaviour
{
    public List<BattleCharacter> foes;
    InfoCarry info;
    public bool boss = false;

    private void Awake()
    {
        info = FindObjectOfType<InfoCarry>().GetComponent<InfoCarry>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Inside of " + gameObject.transform.parent.name + " 's hitbox.");
        if (collision.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.Z))
        {
            info.enemies = foes;
            info.playerPosition = gameObject.transform.position;
            info.sceneName = SceneManager.GetActiveScene().name;
            info.boss = boss;

            GameObject parent = gameObject.transform.parent.gameObject;
            info.delete.Add(parent.name);
            SceneManager.LoadScene("Omori Battle");
        }
    }

}
