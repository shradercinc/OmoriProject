using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoCarry : MonoBehaviour
{
    public static InfoCarry instance = null;
    public List<Weapon> playerWeapons;
    public List<BattleCharacter> enemies;
    BattleManager manager;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            manager = FindObjectOfType<BattleManager>().GetComponent<BattleManager>();

            for (int i = 0; i < enemies.Count; i++)
            {
                manager.CreateFoe(enemies[i], enemies[i].name);
            }

        }
        else if (instance != this)
            Destroy(gameObject);
    }
}
