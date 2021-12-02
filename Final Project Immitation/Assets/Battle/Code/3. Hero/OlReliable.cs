using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OlReliable : Weapon
{
    public override void AffectUser()
    {
        user = FindObjectOfType<HeroSkills>().GetComponent<BattleCharacter>();
        description = "Hero survives his first lethal attack, but has less Defense.";
        user.lastHit = true;
        user.defenseStat -= 0.4f;
    }
}
