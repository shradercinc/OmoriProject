using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeginBattle : MonoBehaviour
{
    public List<BattleCharacter> foes;
    InfoCarry info;
    bool pressedZ = false;
    public bool boss = false;

    private void Awake()
    {
        info = FindObjectOfType<InfoCarry>().GetComponent<InfoCarry>();
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

    public void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Inside of " + gameObject.transform.parent.name + " 's hitbox.");
        if (collision.gameObject.CompareTag("Player") && pressedZ)
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
