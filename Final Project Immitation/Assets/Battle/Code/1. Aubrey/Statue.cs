using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statue : Weapon
{
    public override void AffectUser()
    {
        user = FindObjectOfType<AubreySkills>().GetComponent<BattleCharacter>();
        description = "Aubrey starts with more Juice, Attack, and Luck, but loses most of her Health.";
        user.attackStat += 0.5f;
        user.luckStat += 0.5f;
        user.startingJuice += 25;
        user.startingHealth -= 50;
    }
}
