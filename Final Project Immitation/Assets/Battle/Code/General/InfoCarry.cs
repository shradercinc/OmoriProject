using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InfoCarry : MonoBehaviour
{
    public static InfoCarry instance = null;
    public bool boss;

    public Weapon[] playerWeapons = new Weapon[4]; //0: Omori; 1: Aubrey; 2: Kel; 3: Hero
    public bool[] unlockedWeapons = new bool[8];
    
    //0: Knife
    //1: Poison Ivy
    //2: Pillow
    //3: Statue
    //4: Beach Ball
    //5: Meteor
    //6: Juice Blender
    //7: Ol' Reliable

    public List<BattleCharacter> enemies;
    public Vector2 playerPosition;
    public List<string> delete;
    public List<string> disable;
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
    }

}
