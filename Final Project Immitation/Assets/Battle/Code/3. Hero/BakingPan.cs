using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BakingPan : Weapon
{
    int unalteredDefense;
    int unalteredSpeed;

    public override void AffectUser()
    {
        user = FindObjectOfType<HeroSkills>().GetComponent<BattleCharacter>();
        description = "Hero's Defense and Speed scales with the team's Energy.";
        unalteredDefense = user.startingDefense;
        unalteredSpeed = user.startingSpeed;
    }
    public override IEnumerator StartOfTurn()
    {
        user.startingDefense = (int)(unalteredDefense + (1.5f * manager.energy));
        user.startingSpeed = (int)(unalteredSpeed + (1.5f * manager.energy));
        yield return user.ResetStats();
    }
}
