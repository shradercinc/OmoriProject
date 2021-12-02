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
        user = FindObjectOfType<ExBoyFriendSkills>().GetComponent<BattleCharacter>();
    }
    public override IEnumerator StartOfTurn()
    {
        if (user.currHealth <= user.startingHealth * 0.25)
        {
            manager.AddText("Spaceboy lose their confidence.", true);
            yield return user.NewEmotion(BattleCharacter.Emotion.DEPRESSED);
            yield return new WaitForSeconds(1);
        }

        else if (user.currHealth <= user.startingHealth * 0.5)
        {
            manager.AddText("Spaceboy get frustrated.", true);
            yield return user.NewEmotion(BattleCharacter.Emotion.ENRAGED);
            yield return new WaitForSeconds(1);
        }

        else if (user.currHealth <= user.startingHealth * 0.75)
        {
            manager.AddText("Spaceboy get overconfident.", true);
            yield return user.NewEmotion(BattleCharacter.Emotion.ECSTATIC);
            yield return new WaitForSeconds(1);
        }
    }
}
