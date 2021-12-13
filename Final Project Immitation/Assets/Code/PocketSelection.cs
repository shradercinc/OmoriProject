using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PocketSelection : MonoBehaviour
{
    /*
    private Transform trans2;
    public GameObject Item1Name;
    public GameObject Item2Name;
    public GameObject Item3Name;
    public GameObject pocketAction;
    public Text ItemDescription;
    public Text itemOneName;
    public Text itemTwoName;
    public Text itemThreeName;
    public int chooseItem = 0;
    public int itemOne = 0;
    public int itemTwo = 0;
    public int itemThree = 0;
    void Awake(){
        trans2 = GetComponent<Transform>();
    }

    void nextItem(){
        if(chooseItem < 3){
            chooseItem = chooseItem + 1;
        }
    }

    void previousItem(){
        if(chooseItem > 0){
            chooseItem = chooseItem - 1;
        }
    }

    void Statchange(){
        switch(chooseItem){
            case 0:
                itemOne = 1;
                itemTwo = 0;
                itemThree = 0;
                trans2.position = new Vector3 (2,2);
                ItemDescription.text = "Describe item 1";
                itemOneName.text = "Item One Name";
                itemTwoName.text = "Item Two Name";
                itemThreeName.text = "Item Three Name";
                if (Input.GetKeyDown(KeyCode.Space) && itemOne == 1) {
                    pocketAction.SetActive(true);
                    Selection.inAction = true;
                }
                if(Input.GetKeyDown(KeyCode.X) && itemOne == 1){
                    pocketAction.SetActive(false);
                    Selection.inAction = false;
                }
                break;
            
            case 1:
                itemOne = 0;
                itemTwo = 1;
                itemThree = 0;
                trans2.position = new Vector3 (2,1);
                ItemDescription.text = "Describe item 2";
                itemOneName.text = "Item One Name";
                itemTwoName.text = "Item Two Name";
                itemThreeName.text = "Item Three Name";
                if (Input.GetKeyDown(KeyCode.Space) && itemTwo == 1) {
                    pocketAction.SetActive(true);
                    Selection.inAction = true;
                }
                if(Input.GetKeyDown(KeyCode.X) && itemTwo== 1){
                    pocketAction.SetActive(false);
                    Selection.inAction = false;
                }
                break;

            case 2:
                itemOne = 0;
                itemTwo = 0;
                itemThree = 1;
                trans2.position = new Vector3 (2,0);
                ItemDescription.text = "Describe item 3";
                itemOneName.text = "Item One Name";
                itemTwoName.text = "Item Two Name";
                itemThreeName.text = "Item Three Name";
                if (Input.GetKeyDown(KeyCode.Space) && itemThree == 1) {
                    pocketAction.SetActive(true);
                    Selection.inAction = true;
                }
                if(Input.GetKeyDown(KeyCode.X) && itemThree == 1){
                    pocketAction.SetActive(false);
                    Selection.inAction = false;
                }
                break;

            case 3:
                itemOne = 0;
                itemTwo = 0;
                itemThree = 1;
                trans2.position = new Vector3 (2, 0);
                ItemDescription.text = "Describe item 4";
                itemOneName.text = "Item two Name";
                itemTwoName.text = "Item three Name";
                itemThreeName.text = "Item four Name";
                if (Input.GetKeyDown(KeyCode.Space) && itemThree == 1) {
                    pocketAction.SetActive(true);
                    Selection.inAction = true;
                }
                if(Input.GetKeyDown(KeyCode.X) && itemThree == 1){
                    pocketAction.SetActive(false);
                    Selection.inAction = false;
                }
                break;

        }
    }

    //void squareChange(){

    //}


    void Update(){
        if(Input.GetKeyDown(KeyCode.S)) {
            nextItem();
       }   
       if(Input.GetKeyDown(KeyCode.W)) {
           previousItem();
       }     

       Statchange();
        
        
    }
    */
}
