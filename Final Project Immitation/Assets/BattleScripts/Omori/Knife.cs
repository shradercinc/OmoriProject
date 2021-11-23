using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : Weapon
{
    public override void AffectUser()
    {
        user = gameObject.GetComponent<BattleCharacter>();
        user.startingAttack += 12;
        user.startingAccuracy += 0.12f;
    }
    public override IEnumerator StartOfTurn()
    {
        user.attackStat -= 0.1f;
        user.accuracyStat -= 0.1f;

        user.ResetStats();
        yield return null;
    }
}
