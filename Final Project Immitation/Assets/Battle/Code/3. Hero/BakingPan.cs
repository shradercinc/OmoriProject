using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BakingPan : Weapon
{
    //Hero's stats increase with extra Energy.

    int unalteredAttack;
    int unalteredDefense;
    int unalteredSpeed;

    public override void AffectUser()
    {
        user = FindObjectOfType<HeroSkills>().GetComponent<BattleCharacter>();
        description = "Hero's stats scale with the team's Energy.";
        unalteredAttack = user.startingAttack;
        unalteredDefense = user.startingDefense;
        unalteredSpeed = user.startingSpeed;
    }
    public override IEnumerator StartOfTurn()
    {
        user.startingAttack = unalteredAttack + (int)(0.03f * manager.energy);
        user.startingDefense = unalteredDefense + (int)(0.03f * manager.energy);
        user.startingSpeed = unalteredSpeed + (int)(0.03f * manager.energy);

        yield return user.ResetStats();
    }
}
