using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public BattleCharacter user;

    // Start is called before the first frame update
    void Awake()
    {
        user = gameObject.GetComponent<BattleCharacter>();
    }

    public virtual void AffectUser()
    {
    }
}
