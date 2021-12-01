using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeGirlsWeapon : Weapon
{
    //At 75% health, Slime Girls become Ecstatic.
    //At 50% health, Slime Girls become Enraged.
    //At 25% health, Slime Girls become Depressed. 

    public override void AffectUser()
    {
        user = FindObjectOfType<SlimeGirlsSkills>().GetComponent<BattleCharacter>();
    }
    public override IEnumerator StartOfTurn()
    {
        if (user.currHealth <= user.startingHealth * 0.25)
        {
            manager.AddText("The Slime Girls lose their confidence.", true);
            yield return user.NewEmotion(BattleCharacter.Emotion.DEPRESSED);
            yield return new WaitForSeconds(1);
        }

        else if (user.currHealth <= user.startingHealth * 0.5)
        {
            manager.AddText("The Slime Girls get frustrated.", true);
            yield return user.NewEmotion(BattleCharacter.Emotion.ENRAGED);
            yield return new WaitForSeconds(1);
        }

        else if (user.currHealth <= user.startingHealth * 0.75)
        {
            manager.AddText("The Slime Girls get overconfident.", true);
            yield return user.NewEmotion(BattleCharacter.Emotion.ECSTATIC);
            yield return new WaitForSeconds(1);
        }
    }
}
