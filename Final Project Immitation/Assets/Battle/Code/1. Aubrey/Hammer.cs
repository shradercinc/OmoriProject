using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : Weapon
{
    public override void AffectUser()
    {
        user = FindObjectOfType<AubreySkills>().GetComponent<BattleCharacter>();
        description = "Aubrey gets Angry every turn. Increases Attack and Accuracy.";
        user.startingAttack += 4;
        user.startingAccuracy += 4;
    }

    public override IEnumerator StartOfTurn()
    {
        manager.AddText("Aubrey feels the urge to hit something.", true);
        yield return user.NewEmotion(BattleCharacter.Emotion.ANGRY);
    }
}
