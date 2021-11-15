using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeachBall : Weapon
{
    public override void AffectUser()
    {
        user.startingLuck += 4;
        user.currEmote = BattleCharacter.Emotion.HAPPY;
    }
}
