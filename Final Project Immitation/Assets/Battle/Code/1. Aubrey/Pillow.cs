using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillow : Weapon
{
    int turnCount = 0;

    public override void AffectUser()
    {
        user = FindObjectOfType<AubreySkills>().GetComponent<BattleCharacter>();
        description = "Aubrey starts with more Attack and Luck, but can't do anything for 3 turns.";
        user.attackStat += 0.4f;
        user.luckStat += 0.4f;
        user.paralyze = true;
    }
    public override IEnumerator StartOfTurn()
    {
        if (turnCount == 2)
        {
            manager.AddText("Aubrey finally wakes up.", true);
            yield return new WaitForSeconds(0.5f);
            turnCount++;
        }
        else
        {
            turnCount++;
            user.paralyze = true;
        }
    }
}
