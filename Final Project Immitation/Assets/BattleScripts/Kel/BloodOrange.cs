using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodOrange : Weapon
{
    public override void AffectUser()
    {
        user.startingJuice += 30;
        user.startingAttack += 6;
    }
}
