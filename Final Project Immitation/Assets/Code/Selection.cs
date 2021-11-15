using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour
{
    private Transform trans;
    public int chooseStage = 0;
    public int equip = 0;
    public int tag = 0;
    public int pokect = 0;
    public int skill = 0;
    public int option = 0;
    public int second = 0;

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
                trans.position = new Vector3 (-8, 4);
                break;

            case 1:
                if(Input.GetKeyDown(KeyCode.Space)){
                    equip = 1;
                    second = 1;
                    trans.position = new Vector3(-6, -2);
                }
                if(Input.GetKeyDown(KeyCode.P)) {
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
                        break;

                    case 2:
                        break;
                    
                    case 3:
                        break;
                    
                    case 4:
                        break;
                }



                break;

            case 2:
                trans.position = new Vector3 (-2, 4);
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
       if(Input.GetKeyDown(KeyCode.D) && second == 0) {
            Next();
       }   
       if(Input.GetKeyDown(KeyCode.A) && second == 0) {
           Back();
       }     

       Actions();
        
    }
}
