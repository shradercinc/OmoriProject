using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : Weapon
{
    //Aubrey starts Angry. Increases Attack and Accuracy.

    public override void AffectUser()
    {
        user = FindObjectOfType<AubreySkills>().GetComponent<BattleCharacter>();
        user.startingAttack += 4;
        user.startingAccuracy += 4;
        user.currEmote = BattleCharacter.Emotion.ANGRY;
    }
    public override IEnumerator StartOfTurn()
    {
        yield return null;
    }
}
