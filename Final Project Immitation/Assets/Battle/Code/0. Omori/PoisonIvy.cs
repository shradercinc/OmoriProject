using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonIvy : Weapon
{
    public override void AffectUser()
    {
        user = FindObjectOfType<OmoriSkills>().GetComponent<BattleCharacter>();
        description = "Greatly increases Defense. Each turn, Omori loses 1/4 of their health.";
        user.defenseStat += 0.8f;
    }
    public override IEnumerator StartOfTurn()
    {
        manager.AddText("Omori gets poisoned by the Ivy.", true);
        yield return user.TakeDamage(user.startingHealth / 3);
    }
}
