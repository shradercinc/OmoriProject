using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeachBall : Weapon
{
    public override void AffectUser()
    {
        user = FindObjectOfType<KelSkills>().GetComponent<BattleCharacter>();
        description = "Increases Kel's Attack and Defense. Each turn, there is a chance that Kel will miss their turn.";
        user.attackStat += 0.4f;
        user.defenseStat += 0.4f;
    }
    public override IEnumerator StartOfTurn()
    {
        if (!user.paralyze && (float)(Random.Range(0.0f, 1f)) < (0.75 + user.currLuck))
        {
            manager.AddText("Kel gets distracted by their Beach Ball.", true);
            user.paralyze = true;
            yield return new WaitForSeconds(0.5f);
        }
    }
}