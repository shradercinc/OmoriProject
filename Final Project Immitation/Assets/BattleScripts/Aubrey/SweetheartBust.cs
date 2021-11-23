using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweetheartBust : Weapon
{
    int unalteredAttack;
    int unalteredDefense;
    int unalteredSpeed;

    public override void AffectUser()
    {
        user = gameObject.GetComponent<BattleCharacter>();
        unalteredAttack = user.startingAttack;
        unalteredDefense = user.startingDefense;
        unalteredSpeed = user.startingSpeed;
    }
    public override IEnumerator StartOfTurn()
    {
        if ((float)user.currHealth / user.startingHealth < 0.5f)
        {
            user.startingAttack = unalteredAttack + 5;
            user.startingDefense = unalteredDefense + 5;
            user.startingSpeed = unalteredSpeed + 5;

            user.ResetStats();
            yield return null;
        }
    }
}
