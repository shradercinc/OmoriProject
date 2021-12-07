using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InfoCarry : MonoBehaviour
{
    public static InfoCarry instance = null;

    public Weapon[] playerWeapons = new Weapon[4]; //0: Omori; 1: Aubrey; 2: Kel; 3: Hero
    public bool[] unlockedWeapons = new bool[8];

    public List<BattleCharacter> enemies;
    public Vector2 playerPosition;
    public List<GameObject> delete;
    public string sceneName;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        /*
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        */
    }
}
