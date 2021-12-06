using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InfoCarry : MonoBehaviour
{
    public static InfoCarry instance = null;

    public Weapon[] playerWeapons = new Weapon[4]; //0: Omori; 1: Aubrey; 2: Kel; 3: Hero
    public List<BattleCharacter> enemies;

    public Vector2 playerPosition;
    public GameObject omori;
    public List<GameObject> deadEnemies;

    BattleManager manager;
    Scene scene;
    public string sceneName;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        scene = SceneManager.GetActiveScene();
        if (scene.name == "Omori Battle")
        {
            manager = FindObjectOfType<BattleManager>().GetComponent<BattleManager>();
            for (int i = 0; i < enemies.Count; i++)
            {
                manager.CreateFoe(enemies[i], enemies[i].name);
            }
        }
        else
        {
            omori.transform.position = playerPosition;
            for (int i = 0; i < deadEnemies.Count; i++)
                deadEnemies[i].SetActive(false);
        }

    }
}
