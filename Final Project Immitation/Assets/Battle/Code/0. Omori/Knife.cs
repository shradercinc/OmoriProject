using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : Weapon
{
    public override void AffectUser()
    {
        user = FindObjectOfType<OmoriSkills>().GetComponent<BattleCharacter>();
        description = "Greatly increases Attack and Accuracy. Each turn, Omori's Attack and Accuracy decreases.";
        user.startingAttack += 12;
        user.startingAccuracy += 0.12f;
    }
    public override IEnumerator StartOfTurn()
    {
        user.startingAttack -= 2;
        user.startingAccuracy -= 0.2f;
        yield return user.ResetStats();
    }
}
