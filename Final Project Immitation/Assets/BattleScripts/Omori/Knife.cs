using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : Weapon
{
    public override void AffectUser()
    {
        user = gameObject.GetComponent<BattleCharacter>();
        user.startingAttack += 5;
        user.startingSpeed += 2;
    }
}
