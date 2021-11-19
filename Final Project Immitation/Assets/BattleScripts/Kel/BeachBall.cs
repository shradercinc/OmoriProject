using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeachBall : Weapon
{
    public override void AffectUser()
    {
        user = gameObject.GetComponent<BattleCharacter>();
        user.startingLuck += 0.1f;
        user.currEmote = BattleCharacter.Emotion.HAPPY;
    }
    public override void StartOfTurn()
    {
    }
}
