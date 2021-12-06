using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeginBattle : MonoBehaviour
{
    public List<BattleCharacter> foes;
    InfoCarry info;

    private void Awake()
    {
        info = FindObjectOfType<InfoCarry>().GetComponent<InfoCarry>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            info.enemies = foes;
            info.playerPosition = gameObject.transform.position;
            info.sceneName = SceneManager.GetActiveScene().name;
            info.deadEnemies.Add(gameObject);
            SceneManager.LoadScene("Omori Battle");
        }
    }
}
