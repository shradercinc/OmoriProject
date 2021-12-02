using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : Weapon
{
    public override void AffectUser()
    {
        user = FindObjectOfType<AubreySkills>().GetComponent<BattleCharacter>();
        description = "Aubrey gets Angry every turn. Increases Attack and Accuracy.";
        user.attackStat += 0.25f;
        user.accuracyStat += 0.25f;
    }

    public override IEnumerator StartOfTurn()
    {
        manager.AddText("Aubrey feels the urge to swing her Hammer.", true);
        yield return user.NewEmotion(BattleCharacter.Emotion.ANGRY);
    }
}
