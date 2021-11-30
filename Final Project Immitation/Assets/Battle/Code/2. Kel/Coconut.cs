using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coconut : Weapon
{
    public override void AffectUser()
    {
        user = FindObjectOfType<KelSkills>().GetComponent<BattleCharacter>();
        description = "Kel has more Juice, Attack, and Luck, but loses most of their Health and Defense.";
        user.startingAttack += 10;
        user.startingLuck += 0.5f;
        user.startingJuice += 25;
        user.startingHealth -= 30;
        user.startingDefense -= 6;
    }
}