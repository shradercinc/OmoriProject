using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : Weapon
{
    public override void AffectUser()
    {
        user.startingAttack += 5;
        user.startingSpeed += 2;
    }
}
