using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public BattleCharacter user;

    public virtual void AffectUser()
    {
    }
    public virtual void StartOfTurn()
    {
    }
}
