using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour
{
    private Transform trans;
    private Color mycolor;
    public GameObject pocketChoice;
    public GameObject pocketSnack;
    public GameObject pocketToy;
    public GameObject pocketImportant;
    public GameObject pockeSquare;
    public GameObject pocketDescription;
    public GameObject pocketSquare;

    public int chooseStage = 0;
    public int equip = 0;
    public int tagg = 0;
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
    public bool Move = true;
    public bool inPocket = false;
    public static bool inAction = false;


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

    void Actions (){
        switch(chooseStage){
            case 0:
                if(Input.GetKeyDown(KeyCode.Space) && first == 0){
                    tagg = 1;
                    first = 1;
                    trans.position = new Vector3(-6.34f, -1.97f);
                }
                if(Input.GetKeyDown(KeyCode.X)) {
                    tagg = 0;
                    first = 0;
                    trans.position = new Vector3 (-5.17f, 3.72f); 
                }
                if(tagg == 0){
                    trans.position = new Vector3 (-8.4f, 3.72f);   
                }
                //else if(equip == 1){
                    //trans.position = new Vector3(-6, -2);
               // }
                if(Input.GetKeyDown(KeyCode.D)) {
                    if(first < 4 && tagg == 1){
                        first = first + 1;
                    }
                }   
                if(Input.GetKeyDown(KeyCode.A)) {
                    if(first > 1 && tagg == 1){
                        first = first - 1;
                    }
                } 
                
                switch(first){
                    case 1:
                        trans.position = new Vector3(-6.34f, -1.97f);
                        break;

                    case 2:
                        trans.position = new Vector3(-2.16f,-1.97f);
                        break;
                    
                    case 3:
                        trans.position = new Vector3 (2.11f, -1.97f);
                        break;
                    
                    case 4:
                        trans.position = new Vector3 (6.41f, -1.97f);
                        break;
                }
                break;

            case 1:
                if(Input.GetKeyDown(KeyCode.Space) && second == 0){
                    equip = 1;
                    second = 1;
                    trans.position = new Vector3(-6.34f, -1.97f);
                }
                if(Input.GetKeyDown(KeyCode.X)) {
                    equip = 0;
                    second = 0;
                    trans.position = new Vector3 (-5.17f, 3.72f); 
                }
                if(equip == 0){
                    trans.position = new Vector3 (-5.17f, 3.72f);   
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
                        trans.position = new Vector3(-6.34f, -1.97f);
                        break;

                    case 2:
                        trans.position = new Vector3(-2.16f,-1.97f);
                        break;
                    
                    case 3:
                        trans.position = new Vector3 (2.11f, -1.97f);
                        break;
                    
                    case 4:
                        trans.position = new Vector3 (6.41f, -1.97f);
                        break;
                }



                break;

            case 2:
                 if(Input.GetKeyDown(KeyCode.Space) && third == 0){
                    pokect = 1;
                    third = 1;
                    pocketChoice.SetActive(true);
                    trans.position = new Vector3(-6.34f, -1.97f);
                }
                if(Input.GetKeyDown(KeyCode.X) && inPocket == false) {
                    pokect = 0;
                    third = 0;
                    pocketChoice.SetActive(false);
                    trans.position = new Vector3 (-5.17f, 3.72f); 
                }
                if(pokect == 0){
                    trans.position = new Vector3 (-1.25f, 3.72f);   
                }
                //else if(equip == 1){
                    //trans.position = new Vector3(-6, -2);
               // }
                if(Input.GetKeyDown(KeyCode.D)) {
                    if(third < 3 && pokect == 1 && Move == true){
                        third = third + 1;
                    }
                }   
                if(Input.GetKeyDown(KeyCode.A)) {
                    if(third > 1 && pokect == 1 && Move == true){
                        third = third - 1;
                    }
                } 
                
                switch(third){
                    case 1:
                        //inPocket = true;
                        if(snack == 0){
                            trans.position = new Vector3(-8, 2);
                            if(Input.GetKeyDown(KeyCode.C)){
                                snack = 1;
                                inPocket = true;
                                pocketSnack.SetActive(true);
                                pockeSquare.SetActive(true);
                                pocketDescription.SetActive(true);
                            }
                        }
                        if (snack == 1){
                            Move = false;
                           // pocketSnack.SetActive(true);
                            trans.position = new Vector3(-8, 2);
                            //pockeSquare.SetActive(true);
                            if(Input.GetKeyDown(KeyCode.X) && inPocket == true && inAction == false){
                                Move = true;
                                snack = 0;
                                pocketSnack.SetActive(false);
                                pockeSquare.SetActive(false);
                                pocketDescription.SetActive(false);
                                pockeSquare.GetComponent<PocketSelection>().chooseItem = 0;
                            }
                        }
                       
                        break;

                    case 2:
                        if(toy == 0){
                            trans.position = new Vector3(-5,2);
                            if(Input.GetKeyDown(KeyCode.C)){
                                toy = 1;
                                pocketToy.SetActive(true);
                                pockeSquare.SetActive(true);
                                pocketDescription.SetActive(true);
                            }
                        }
                        if (toy == 1){
                            Move = false;
                           // pocketSnack.SetActive(true);
                            trans.position = new Vector3(-5, 2);
                            //pockeSquare.SetActive(true);
                            if(Input.GetKeyDown(KeyCode.X) && inPocket == true && inAction == false){
                                Move = true;
                                toy = 0;
                                pocketToy.SetActive(false);
                                pockeSquare.SetActive(false);
                                pocketDescription.SetActive(false);
                                pockeSquare.GetComponent<PocketSelection>().chooseItem = 0;
                            }
                        }
                        break;
                    
                    case 3:
                        if(important == 0){
                            trans.position = new Vector3 (-3, 2);
                            if(Input.GetKeyDown(KeyCode.C)){
                                important = 1;
                                pocketImportant.SetActive(true);
                                pockeSquare.SetActive(true);
                                pocketDescription.SetActive(true);
                            }
                            
                        }
                        if (important == 1){
                            Move = false;

                           // pocketSnack.SetActive(true);
                            trans.position = new Vector3(-3, 2);
                            //pockeSquare.SetActive(true);
                            if(Input.GetKeyDown(KeyCode.X) && inPocket == true && inAction == false){
                                Move = true;
                                important = 0;
                                pocketImportant.SetActive(false);
                                pockeSquare.SetActive(false);
                                pocketDescription.SetActive(false);
                                pockeSquare.GetComponent<PocketSelection>().chooseItem = 0;
                            }
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
