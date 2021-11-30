using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillow : Weapon
{
    //If Aubrey has more than half health, their Attack and Speed increase. Otherwise, their Attack and Speed decrease.

    int unalteredAttack;
    int unalteredSpeed;

    public override void AffectUser()
    {
        user = FindObjectOfType<AubreySkills>().GetComponent<BattleCharacter>();
        unalteredAttack = user.startingAttack;
        unalteredSpeed = user.startingSpeed;
    }
    public override IEnumerator StartOfTurn()
    {
        if ((float)user.currHealth / user.startingHealth > 0.5f)
        {
            user.startingAttack = unalteredAttack + 7;
            user.startingSpeed = unalteredSpeed + 7;
            yield return user.ResetStats();
        }
        else
        {
            user.startingAttack = unalteredAttack - 7;
            user.startingSpeed = unalteredSpeed - 7;
            yield return user.ResetStats();
        }
    }
}
