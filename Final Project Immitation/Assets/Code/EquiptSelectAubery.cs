using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquiptSelectAubery : MonoBehaviour
{
    /*
     private Transform weaponTrans;
    public GameObject weaponDes;
    public GameObject thisSquare;
    public GameObject gameSquare;
    public Text weaponOne;
    public Text weaponTwo;
    public Text weaponThree;
    public Text weaponDesciption;
    public int chooseWeapon = 0;
    public int weapon1 = 0;
    public int weapon2 = 0;
    public int weapon3 = 0;
    public bool unlock = false;
    public Weapon[] weapons = new Weapon [8];
    public string WeaponOneName;
    public string WeaponTwoName;
    public string WeaponDescription;
    public InfoCarry info;
    
    //InfoCarry info = FindObjectOfType<InfoCarry>().GetComponent <InfoCarry>();



    void Awake(){
        weaponTrans = GetComponent<Transform>();    

        InfoCarry info = FindObjectOfType<InfoCarry>().GetComponent <InfoCarry>();
    }

    void nextWeapon(){
        if(chooseWeapon < 2){
            chooseWeapon = chooseWeapon + 1;
        }
    }

    void WeaponChange(){
        switch(chooseWeapon){
            case 0:
                weapon1 = 1;
                weapon2 = 0;
                weapon3 = 0;
                weaponTrans.position = new Vector3 (-7.73f, -3.19f);
                weaponDes.SetActive(true);
                WeaponOneName = weapons[2].name;
                WeaponTwoName = weapons[3].name;
                WeaponDescription = weapons[2].description;
                weaponOne.text = WeaponOneName;
                weaponTwo.text = WeaponTwoName;
                weaponThree.text = "------";
                weaponDesciption.text = WeaponDescription;
                if(Input.GetKeyDown(KeyCode.Space) && weapon1 == 1){
                    info.playerWeapons[1] = weapons[2];
                }
                if(Input.GetKeyDown(KeyCode.X) && weapon1 == 1) {
                    thisSquare.SetActive(false);
                    gameSquare.SetActive(true);
                    weaponDes.SetActive(false);
                }
                break;

            case 1:
                weapon1 = 0;
                weapon2 = 1;
                weapon3 = 0;
                weaponTrans.position = new Vector3 (-7.73f, -3.84f);
                WeaponOneName = weapons[2].name;
                WeaponTwoName = weapons[3].name;
                WeaponDescription = weapons[3].description;
                weaponOne.text = WeaponOneName;
                weaponTwo.text = WeaponTwoName;
                weaponThree.text = " ------";
                weaponDesciption.text = WeaponDescription;
                if(Input.GetKeyDown(KeyCode.Space) && weapon2 == 1){
                    info.playerWeapons[1] = weapons[3];
                }
                if(Input.GetKeyDown(KeyCode.X) && weapon2 == 1) {
                    thisSquare.SetActive(false);
                    gameSquare.SetActive(true);
                    weaponDes.SetActive(false);
                }
                break;

            case 2:
                weapon1 = 0;
                weapon2 = 0;
                weapon3 = 1;
                weaponTrans.position = new Vector3 (-7.73f, -4.56f);
                WeaponOneName = weapons[2].name;
                WeaponTwoName = weapons[3].name;
                weaponOne.text = WeaponOneName;
                weaponTwo.text = WeaponTwoName;
                weaponThree.text = " ------";
                weaponDesciption.text = " ------";
                if(Input.GetKeyDown(KeyCode.Space) && weapon3 == 1){
                    info.playerWeapons[1] = null;
                }
                if(Input.GetKeyDown(KeyCode.X) && weapon3 == 1) {
                    thisSquare.SetActive(false);
                    gameSquare.SetActive(true);
                    weaponDes.SetActive(false);
                }
                break;

        }

    }

    void previousWeapon(){
        if (chooseWeapon > 0) {
            chooseWeapon = chooseWeapon - 1;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S)) {
            nextWeapon();
       }   
       if(Input.GetKeyDown(KeyCode.W)) {
           previousWeapon();
       }     

       WeaponChange();
        
        
    }
    */
}

