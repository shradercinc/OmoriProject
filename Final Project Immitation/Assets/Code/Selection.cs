using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour
{
    private Transform trans;
    public GameObject pocketChoice;
    public GameObject pocketSnack;
    public GameObject pocketToy;
    public GameObject pocketImportant;
    public int chooseStage = 0;
    public int equip = 0;
    public int tag = 0;
    public int pokect = 0;
    public int skill = 0;
    public int option = 0;
    public int first = 0;
    public int second = 0;
    public int third = 0;
    public int snack = 0;
    public int toy = 0;
    public int important = 0;
    public int forth = 0;
    public int five = 0;


    void Awake(){
        trans = GetComponent<Transform>();

    }

    public void Next(){
        if(chooseStage < 4){
            chooseStage = chooseStage + 1;
        }
    }

    public void Back(){
        if(chooseStage > 0){
            chooseStage = chooseStage - 1;
        }
    }

   /* public void PostitionChange(){
        if(chooseStage == 0){
            trans.position = new Vector3 (-8, 4);
        }
        else if(chooseStage == 1){
            trans.position = new Vector3 (-5, 4);
        }
        else if(chooseStage == 2){
            trans.position = new Vector3 (-2, 4);
        }
        else if(chooseStage == 3){
            trans.position = new Vector3 (2, 4);
        }
        else if(chooseStage == 4){
            trans.position = new Vector3 (5, 4);
        }
    }
*/
    void Actions (){
        switch(chooseStage){
            case 0:
                if(Input.GetKeyDown(KeyCode.Space) && first == 0){
                    tag = 1;
                    first = 1;
                    trans.position = new Vector3(-6, -2);
                }
                if(Input.GetKeyDown(KeyCode.X)) {
                    tag = 0;
                    first = 0;
                    trans.position = new Vector3 (-5, 4); 
                }
                if(tag == 0){
                    trans.position = new Vector3 (-8, 4);   
                }
                //else if(equip == 1){
                    //trans.position = new Vector3(-6, -2);
               // }
                if(Input.GetKeyDown(KeyCode.D)) {
                    if(first < 4 && tag == 1){
                        first = first + 1;
                    }
                }   
                if(Input.GetKeyDown(KeyCode.A)) {
                    if(first > 1 && tag == 1){
                        first = first - 1;
                    }
                } 
                
                switch(first){
                    case 1:
                        trans.position = new Vector3(-6, -2);
                        break;

                    case 2:
                        trans.position = new Vector3(-2,-2);
                        break;
                    
                    case 3:
                        trans.position = new Vector3 (2, -2);
                        break;
                    
                    case 4:
                        trans.position = new Vector3 (7, -2);
                        break;
                }
                break;

            case 1:
                if(Input.GetKeyDown(KeyCode.Space) && second == 0){
                    equip = 1;
                    second = 1;
                    trans.position = new Vector3(-6, -2);
                }
                if(Input.GetKeyDown(KeyCode.X)) {
                    equip = 0;
                    second = 0;
                    trans.position = new Vector3 (-5, 4); 
                }
                if(equip == 0){
                    trans.position = new Vector3 (-5, 4);   
                }
                //else if(equip == 1){
                    //trans.position = new Vector3(-6, -2);
               // }
                if(Input.GetKeyDown(KeyCode.D)) {
                    if(second < 4 && equip == 1){
                        second = second + 1;
                    }
                }   
                if(Input.GetKeyDown(KeyCode.A)) {
                    if(second > 1 && equip == 1){
                        second = second - 1;
                    }
                } 
                
                switch(second){
                    case 1:
                        trans.position = new Vector3(-6, -2);
                        break;

                    case 2:
                        trans.position = new Vector3(-2,-2);
                        break;
                    
                    case 3:
                        trans.position = new Vector3 (2, -2);
                        break;
                    
                    case 4:
                        trans.position = new Vector3 (7, -2);
                        break;
                }



                break;

            case 2:
                 if(Input.GetKeyDown(KeyCode.Space) && third == 0){
                    pokect = 1;
                    third = 1;
                    pocketChoice.SetActive(true);
                    trans.position = new Vector3(-6, -2);
                }
                if(Input.GetKeyDown(KeyCode.X)) {
                    pokect = 0;
                    third = 0;
                    pocketChoice.SetActive(false);
                    trans.position = new Vector3 (-5, 4); 
                }
                if(pokect == 0){
                    trans.position = new Vector3 (-2, 4);   
                }
                //else if(equip == 1){
                    //trans.position = new Vector3(-6, -2);
               // }
                if(Input.GetKeyDown(KeyCode.D)) {
                    if(third < 3 && pokect == 1){
                        third = third + 1;
                    }
                }   
                if(Input.GetKeyDown(KeyCode.A)) {
                    if(third > 1 && pokect == 1){
                        third = third - 1;
                    }
                } 
                
                switch(third){
                    case 1:
                        if(snack == 0){
                            trans.position = new Vector3(-8, 2);
                        }
                        if(Input.GetKeyDown(KeyCode.Space)){
                            snack = 1;

                        }
                        break;

                    case 2:
                        if(toy == 0){
                            trans.position = new Vector3(-5,2);
                        }
                        break;
                    
                    case 3:
                        if(important == 0){
                            trans.position = new Vector3 (-3, 2);
                        }
                        break;
                    
                }
                break;
                break;

            case 3:
                trans.position = new Vector3 (2, 4);
                break;

            case 4:
                trans.position = new Vector3 (5, 4);
                break;


        }
    }
    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.D) && second == 0 && first == 0 && third == 0) {
            Next();
       }   
       if(Input.GetKeyDown(KeyCode.A) && second == 0 && first == 0 && third == 0) {
           Back();
       }     

       Actions();
        
    }
}
