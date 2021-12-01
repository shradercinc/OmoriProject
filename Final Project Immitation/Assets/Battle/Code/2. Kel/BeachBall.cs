using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeachBall : Weapon
{
    public override void AffectUser()
    {
        user = FindObjectOfType<KelSkills>().GetComponent<BattleCharacter>();
        description = "Kel starts Happy. Increases Attack and Luck.";
        user.startingAttack += 4;
        user.startingLuck += 0.4f;
    }
    public override IEnumerator StartOfTurn()
    {
        manager.AddText("Kel is excited to play with his Beach Ball.", true);
        yield return user.NewEmotion(BattleCharacter.Emotion.HAPPY);
    }

}
