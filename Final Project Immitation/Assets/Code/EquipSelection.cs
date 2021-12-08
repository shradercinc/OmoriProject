using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipSelection : MonoBehaviour
{
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
    void Awake(){
        weaponTrans = GetComponent<Transform>();    
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
                weaponOne.text = "Weapon one";
                weaponTwo.text = "Weapon two";
                weaponThree.text = "Weapon three";
                weaponDesciption.text = "Weapon One Description";
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
                weaponOne.text = "Weapon one";
                weaponTwo.text = "Weapon two";
                weaponThree.text = "Weapon three";
                weaponDesciption.text = "Weapon Two Description";
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
                weaponOne.text = "Weapon one";
                weaponTwo.text = "Weapon two";
                weaponThree.text = "Weapon three";
                weaponDesciption.text = "Weapon Three Description";
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
}
