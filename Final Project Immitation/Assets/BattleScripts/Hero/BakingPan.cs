using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BakingPan : Weapon
{
    public override void AffectUser()
    {
        user.startingHealth += 10;
        user.startingAttack += 6;
    }
}
