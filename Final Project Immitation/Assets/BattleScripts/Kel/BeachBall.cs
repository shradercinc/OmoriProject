using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeachBall : Weapon
{
    public override void AffectUser()
    {
        user = gameObject.GetComponent<BattleCharacter>();
        user.startingAccuracy += 0.2f;
        user.currEmote = BattleCharacter.Emotion.HAPPY;
    }
    public override IEnumerator StartOfTurn()
    {
        yield return null;
    }
}
