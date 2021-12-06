using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : Weapon
{
    public override void AffectUser()
    {
        user = FindObjectOfType<OmoriSkills>().GetComponent<BattleCharacter>();
        description = "Omori starts with more Attack and Accuracy. Each turn, Omori's Attack and Accuracy decreases.";
        user.attackStat += 0.6f;
        user.accuracyStat += 0.2f;
    }
    public override IEnumerator StartOfTurn()
    {
        manager.AddText("Omori's Knife begins to rust.", true);
        user.attackStat -= 0.15f;
        user.accuracyStat -= 0.05f;

        yield return new WaitForSeconds(0.5f);
        manager.AddText("Omori's Attack and Accuracy decreases.");
        yield return user.ResetStats();
        yield return new WaitForSeconds(0.5f);
    }
}
