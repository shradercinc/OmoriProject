using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeachBall : Weapon
{
    //Kel starts Happy. Increases Attack and Luck. 

    public override void AffectUser()
    {
        user = FindObjectOfType<KelSkills>().GetComponent<BattleCharacter>();
        description "Kel starts Happy. Increases Attack and Luck.";
        user.startingAttack += 4;
        user.startingLuck += 0.4f;
        user.currEmote = BattleCharacter.Emotion.HAPPY;
    }
    public override IEnumerator StartOfTurn()
    {
        yield return null;
    }
}
