using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BakingPan : Weapon
{
    public override void AffectUser()
    {
        user = gameObject.GetComponent<BattleCharacter>();
    }
    public override void StartOfTurn()
    {
        user.attackStat += (0.05f * manager.energy);
        user.defenseStat += (0.05f * manager.energy);
        user.speedStat += (0.05f * manager.energy);
        user.luckStat += (0.05f * manager.energy);
        user.accuracyStat += (0.05f * manager.energy);
        user.ResetStats();
    }
}
