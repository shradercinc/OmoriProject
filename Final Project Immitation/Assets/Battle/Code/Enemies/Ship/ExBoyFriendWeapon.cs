using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExBoyFriendWeapon : Weapon
{
    //At 75% health, become Ecstatic.
    //At 50% health, become Enraged.
    //At 25% health, become Depressed. 

    public override void AffectUser()
    {
    }
    public override IEnumerator StartOfTurn()
    {
        if (user.currHealth <= (user.startingHealth * 0.25f))
        {
            manager.AddText("Spaceboy lose their confidence.", true);
            yield return user.NewEmotion(BattleCharacter.Emotion.DEPRESSED);
        }

        else if (user.currHealth <= (user.startingHealth * 0.5f))
        {
            manager.AddText("Spaceboy get frustrated.", true);
            yield return user.NewEmotion(BattleCharacter.Emotion.ENRAGED);
        }

        else if (user.currHealth <= (user.startingHealth * 0.75f))
        {
            manager.AddText("Spaceboy get overconfident.", true);
            yield return user.NewEmotion(BattleCharacter.Emotion.ECSTATIC);
        }
    }
}
