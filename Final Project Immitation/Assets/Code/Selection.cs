using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour
{
    private Transform trans;
    public int chooseStage = 0;

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

    public void PostitionChange(){
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

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.D)) {
            Next();
       }   
       if(Input.GetKeyDown(KeyCode.A)) {
           Back();
       }     

       PostitionChange();
        
    }
}
