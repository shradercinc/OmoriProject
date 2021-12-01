using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : Weapon
{
    public override void AffectUser()
    {
        user = FindObjectOfType<OmoriSkills>().GetComponent<BattleCharacter>();
        description = "Omori starts with more Attack and Accuracy. Each turn, Omori's Attack and Accuracy decreases.";
        user.startingAttack += 10;
        user.startingAccuracy += 0.12f;
    }
    public override IEnumerator StartOfTurn()
    {
        manager.AddText("Omori's Knife begins to rust.", true);
        user.startingAttack -= 2;
        user.startingAccuracy -= 0.2f;

        yield return new WaitForSeconds(1);
        manager.AddText("Omori's Attack and Accuracy decreases.");
        yield return user.ResetStats();
    }
}
