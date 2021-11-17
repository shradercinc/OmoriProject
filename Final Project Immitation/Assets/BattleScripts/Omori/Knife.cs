using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : Weapon
{
    public override void AffectUser()
    {
        user = gameObject.GetComponent<BattleCharacter>();
        user.startingAttack += 12;
        user.startingAccuracy += 12;
    }
    public override void StartOfTurn()
    {
        user.startingAttack -= 2;
        user.startingAccuracy -= 2;
        user.ResetStats();
    }
}
