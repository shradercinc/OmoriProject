using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InfoCarry : MonoBehaviour
{
    public static InfoCarry instance = null;
    public bool boss;

    public string[] playerWeapons = new string[4]; //0: Omori; 1: Aubrey; 2: Kel; 3: Hero
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

    public void UnlockWeapon(string x)
    {
        switch (x)
        {
            case ("Knife"):
                unlockedWeapons[0] = true;
                break;
            case ("Poison Ivy"):
                unlockedWeapons[1] = true;
                break;
            case ("Pillow"):
                unlockedWeapons[2] = true;
                break;
            case ("Statue"):
                unlockedWeapons[3] = true;
                break;
            case ("Beach Ball"):
                unlockedWeapons[4] = true;
                break;
            case ("Meteor"):
                unlockedWeapons[5] = true;
                break;
            case ("Juice Blender"):
                unlockedWeapons[6] = true;
                break;
            case ("Ol' Reliable"):
                unlockedWeapons[7] = true;
                break;
        }
    }

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
