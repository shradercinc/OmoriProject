using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coconut : Weapon
{
    public override void AffectUser()
    {
        user = FindObjectOfType<KelSkills>().GetComponent<BattleCharacter>();
        description = "Kel starts with more Juice, Attack, and Luck, but loses most of his Health.";
        user.attackStat += 0.75f;
        user.luckStat += 0.4f;
        user.startingJuice += 25;
        user.startingHealth -= 30;
    }
}