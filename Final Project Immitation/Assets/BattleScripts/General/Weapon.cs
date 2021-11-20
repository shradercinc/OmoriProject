using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public BattleCharacter user;
    public BattleManager manager;

    private void Awake()
    {
        manager = FindObjectOfType<BattleManager>().GetComponent<BattleManager>();
    }

    public virtual void AffectUser()
    {
    }
    public virtual void StartOfTurn()
    {
    }
}
