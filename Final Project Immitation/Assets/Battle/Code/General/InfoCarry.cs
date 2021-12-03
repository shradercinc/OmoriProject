using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InfoCarry : MonoBehaviour
{
    public static InfoCarry instance = null;
    public Weapon[] playerWeapons = new Weapon[4]; //0: Omori; 1: Aubrey; 2: Kel; 3: Hero
    public List<BattleCharacter> enemies;
    public Vector3 playerPosition;
    public GameObject omori;

    BattleManager manager;
    Scene scene; 

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            manager = FindObjectOfType<BattleManager>().GetComponent<BattleManager>();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        scene = SceneManager.GetActiveScene();
        if (scene.name == "Omori Battle")
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                manager.CreateFoe(enemies[i], enemies[i].name);
            }
        }
        else
        {
            omori.transform.position = playerPosition;
        }

    }

}
