using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweetheartBust : Weapon
{
    public override void AffectUser()
    {
        user = gameObject.GetComponent<BattleCharacter>();
        user.startingAttack += 20;
        user.startingSpeed -= 10;
        user.startingAccuracy -= 0.1f;
    }
    public override void StartOfTurn()
    {
    }
}
