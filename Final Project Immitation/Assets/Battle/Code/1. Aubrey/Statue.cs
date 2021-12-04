using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statue : Weapon
{
    //Increase Aubrey's Attack and Defense. There is a chance that Aubrey will miss her turn.

    public override void AffectUser()
    {
        user = FindObjectOfType<AubreySkills>().GetComponent<BattleCharacter>();
        description = "Increase Aubrey's Attack and Defense. Each turn, there is a chance that Aubrey will miss her turn.";
        user.attackStat += 0.75f;
        user.defenseStat += 0.75f;
    }
    public override IEnumerator StartOfTurn()
    {
        if (!user.paralyze && (float)(Random.Range(0.0f, 1f)) < (0.75+user.currLuck))
        {
            manager.AddText("Aubrey feels weighed down by the Statue.", true);
            user.paralyze = true;
            yield return new WaitForSeconds(1);
        }
    }
}
