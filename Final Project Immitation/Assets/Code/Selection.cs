using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selection : MonoBehaviour
{
    /*
    private Transform trans;
    private Color mycolor;
    public GameObject choiceSquare;
    public GameObject pocketChoice;
    public GameObject pocketSnack;
    public GameObject pocketToy;
    public GameObject pocketImportant;
    public GameObject pockeSquare;
    public GameObject pocketDescription;
    public GameObject pocketSquare;
    public GameObject OmoriSquare;
    public GameObject AuberySquare;
    public GameObject HeroSquare;
    public GameObject KeiSquare;
    public GameObject Omori;
    public Transform Omoritrans;
    public GameObject Aubery;
    public Transform Auberytrans;
    public GameObject Hero;
    public Transform Herotrans;
    public GameObject Kei;
    public Transform Keitrans;

    public int chooseStage = 0;
    public int equip = 0;
    public int tagg = 0;
    public int pokect = 0;
    public int skill = 0;
    public int option = 0;
    public int first = 0;
    public int second = 0;
    public int inCharacter = 0;
    public int characterOne = 0;
    public int characterTwo = 0;
    public int characterThree = 0;
    public int characterFour = 0;
    public int third = 0;
    public int snack = 0;
    public int toy = 0;
    public int important = 0;
    public int forth = 0;
    public int five = 0;
    public bool Move = true;
    public bool inPocket = false;
    public static bool inAction = false;
    public static bool Canexit = true;


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
                Canexit = true;
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
                        Canexit = false;
                        trans.position = new Vector3(-6.53f, -0.51f);
                        break;

                    case 2:
                        trans.position = new Vector3(-2.02f,-0.51f);
                        break;
                    
                    case 3:
                        trans.position = new Vector3 (2.48f, -0.51f);
                        break;
                    
                    case 4:
                        trans.position = new Vector3 (6.67f, -0.51f);
                        break;
                }
                break;

            case 1:
                Canexit = true;
                if(Input.GetKeyDown(KeyCode.Space) && second == 0){
                    equip = 1;
                    second = 1;
                    trans.position = new Vector3(-6.53f, -0.51f);
                }
                if(Input.GetKeyDown(KeyCode.X) && inCharacter == 0) {
                    equip = 0;
                    second = 0;
                    //characterOne = 0;
                   // characterTwo = 0;
                    //characterThree = 0;
                    //characterFour = 0;
                    trans.position = new Vector3 (-5.17f, 3.72f); 
                }
                if(equip == 0){
                    trans.position = new Vector3 (-5.17f, 3.72f);   
                }
                //else if(equip == 1){
                    //trans.position = new Vector3(-6, -2);
               // }
                if(Input.GetKeyDown(KeyCode.D) && inCharacter == 0) {
                    if(second < 4 && equip == 1){
                        second = second + 1;
                    }
                }   
                if(Input.GetKeyDown(KeyCode.A) && inCharacter == 0) {
                    if(second > 1 && equip == 1){
                        second = second - 1;
                    }
                } 
                
                switch(second){
                    case 1:
                        Canexit = false;
                        trans.position = new Vector3(-6.53f, -0.51f);
                        characterTwo = 0;
                        characterThree = 0;
                        characterFour = 0;
                        if(Input.GetKeyDown(KeyCode.C) && characterOne == 1){
                            Aubery.SetActive(false);
                            Hero.SetActive(false);
                            Kei.SetActive(false);
                            inCharacter = 1;
                            Omoritrans.position = new Vector3 (-6.75f, 0.3f);
                            choiceSquare.SetActive(false);
                            OmoriSquare.SetActive(true);

                        }
                        characterOne = 1;
                        if (Input.GetKeyDown(KeyCode.X) && inCharacter == 1){
                            Aubery.SetActive(true);
                            Hero.SetActive(true);
                            Kei.SetActive(true);
                            characterOne = 0;
                            inCharacter = 0;
                            Omoritrans.position = new Vector3 (-6.75f, -2.4f);
                        }
                        
                        break;

                    case 2:
                        trans.position = new Vector3(-2.02f,-0.51f);
                        characterOne = 0;
                        characterThree = 0;
                        characterFour = 0;
                        if(Input.GetKeyDown(KeyCode.C) && characterTwo == 1){
                            Omori.SetActive(false);
                            Hero.SetActive(false);
                            Kei.SetActive(false);
                            inCharacter= 1;
                            Auberytrans.position = new Vector3 (-6.75f, 0.3f);
                            choiceSquare.SetActive(false);
                            AuberySquare.SetActive(true);
                        }
                        characterTwo = 1;
                        if (Input.GetKeyDown(KeyCode.X) && inCharacter == 1){
                            Omori.SetActive(true);
                            Hero.SetActive(true);
                            Kei.SetActive(true);
                            characterTwo = 0;
                            inCharacter = 0;
                            Auberytrans.position = new Vector3 (-2.05f, -2.4f);
                        }
                        break;
                    
                    case 3:
                        trans.position = new Vector3 (2.48f, -0.51f);
                        characterOne = 0;
                        characterTwo = 0;
                        characterFour = 0;
                        if(Input.GetKeyDown(KeyCode.C) && characterThree == 1){
                            Omori.SetActive(false);
                            Aubery.SetActive(false);
                            Kei.SetActive(false);
                            inCharacter = 1;
                            Herotrans.position = new Vector3 (-6.75f, 0.3f);
                            choiceSquare.SetActive(false);
                            HeroSquare.SetActive(true);
                        }
                        characterThree = 1;
                        if (Input.GetKeyDown(KeyCode.X) && inCharacter == 1){
                            Omori.SetActive(true);
                            Aubery.SetActive(true);
                            Kei.SetActive(true);
                            characterThree = 0;
                            inCharacter = 0;
                            Herotrans.position = new Vector3 (2.53f, -2.4f);
                        }
                        break;
                    
                    case 4:
                        trans.position = new Vector3 (6.67f, -0.51f);
                        characterOne = 0;
                        characterTwo = 0;
                        characterThree = 0;
                        if(Input.GetKeyDown(KeyCode.C) && characterFour == 1){
                            Omori.SetActive(false);
                            Aubery.SetActive(false);
                            Hero.SetActive(false);
                            inCharacter = 1;
                            Keitrans.position = new Vector3 (-6.75f, 0.3f);
                            choiceSquare.SetActive(false);
                            KeiSquare.SetActive(true);
                        }
                        characterFour = 1;
                        if (Input.GetKeyDown(KeyCode.X) && inCharacter == 1){
                            Omori.SetActive(true);
                            Aubery.SetActive(true);
                            Hero.SetActive(true);
                            characterFour = 0;
                            inCharacter = 0;
                            Keitrans.position = new Vector3 (6.76f, -2.4f);
                        }
                        break;
                }

                break;

            case 2:
                Canexit = true;
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
                        Canexit = false;
                        //inPocket = true;
                        if(snack == 0){
                            trans.position = new Vector3(-8.37f, 2.22f);
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
                            trans.position = new Vector3(-8.37f, 2.22f);
                            //pockeSquare.SetActive(true);
                            if(Input.GetKeyDown(KeyCode.X) && inPocket == true && inAction == false){
                                Move = true;
                                inPocket = false;
                                snack = 0;
                                pocketSnack.SetActive(false);
                                pockeSquare.SetActive(false);
                                pocketDescription.SetActive(false);
                                //pockeSquare.GetComponent<PocketSelection>().chooseItem = 0;
                            }
                        }
                       
                        break;

                    case 2:
                        if(toy == 0){
                            trans.position = new Vector3(-5.54f,2.22f);
                            if(Input.GetKeyDown(KeyCode.C)){
                                toy = 1;
                                inPocket = true;
                                pocketToy.SetActive(true);
                                pockeSquare.SetActive(true);
                                pocketDescription.SetActive(true);
                            }
                        }
                        if (toy == 1){
                            Move = false;
                           // pocketSnack.SetActive(true);
                            trans.position = new Vector3(-5.54f, 2.22f);
                            //pockeSquare.SetActive(true);
                            if(Input.GetKeyDown(KeyCode.X) && inPocket == true && inAction == false){
                                Move = true;
                                inPocket = false;
                                toy = 0;
                                pocketToy.SetActive(false);
                                pockeSquare.SetActive(false);
                                pocketDescription.SetActive(false);
                                //pockeSquare.GetComponent<PocketSelection>().chooseItem = 0;
                            }
                        }
                        break;
                    
                    case 3:
                        if(important == 0){
                            trans.position = new Vector3 (-2.51f, 2.22f);
                            if(Input.GetKeyDown(KeyCode.C)){
                                important = 1;
                                inPocket = true;
                                pocketImportant.SetActive(true);
                                pockeSquare.SetActive(true);
                                pocketDescription.SetActive(true);
                            }
                            
                        }
                        if (important == 1){
                            Move = false;

                           // pocketSnack.SetActive(true);
                            trans.position = new Vector3(-2.51f, 2.22f);
                            //pockeSquare.SetActive(true);
                            if(Input.GetKeyDown(KeyCode.X) && inPocket == true && inAction == false){
                                Move = true;
                                inPocket = false;
                                important = 0;
                                pocketImportant.SetActive(false);
                                pockeSquare.SetActive(false);
                                pocketDescription.SetActive(false);
                                //pockeSquare.GetComponent<PocketSelection>().chooseItem = 0;
                            }
                        }
                        break;
                    
                }
                break;

            case 3:
                Canexit = true;
                 if(Input.GetKeyDown(KeyCode.Space) && forth == 0){
                    skill = 1;
                    forth = 1;
                    trans.position = new Vector3(-6.34f, -1.97f);
                }
                if(Input.GetKeyDown(KeyCode.X)) {
                    skill = 0;
                    forth = 0;
                }
                if(skill == 0){
                    trans.position = new Vector3 (2.88f, 3.66f);   
                }
                //else if(equip == 1){
                    //trans.position = new Vector3(-6, -2);
               // }
                if(Input.GetKeyDown(KeyCode.D)) {
                    if(forth < 4 && skill == 1){
                        forth = forth + 1;
                    }
                }   
                if(Input.GetKeyDown(KeyCode.A)) {
                    if(forth > 1 && skill == 1){
                        forth = forth - 1;
                    }
                } 
                
                switch(forth){
                    case 1:
                        Canexit = false;
                        trans.position = new Vector3(-6.53f, -0.51f);
                        break;

                    case 2:
                        trans.position = new Vector3(-2.02f,-0.51f);
                        break;
                    
                    case 3:
                        trans.position = new Vector3 (2.48f, -0.51f);
                        break;
                    
                    case 4:
                        trans.position = new Vector3 (6.67f, -0.51f);
                        break;
                }
                break;

            break;

            case 4:
                trans.position = new Vector3 (5.8f, 3.66f);
                break;


        }
    }
    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.D) && second == 0 && first == 0 && third == 0 && forth == 0) {
            Next();
       }   
       if(Input.GetKeyDown(KeyCode.A) && second == 0 && first == 0 && third == 0 && forth == 0) {
           Back();
       }     

       Actions();
        
    }
    */
}
