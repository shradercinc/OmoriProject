using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeachBall : Weapon
{
    public override void AffectUser()
    {
        user = FindObjectOfType<KelSkills>().GetComponent<BattleCharacter>();
        description = "Kel starts Happy. Increases Attack and Luck.";
        user.attackStat += 0.25f;
        user.luckStat += 0.25f;
    }
    public override IEnumerator StartOfTurn()
    {
        manager.AddText("Kel is excited to play with his Beach Ball.", true);
        yield return user.NewEmotion(BattleCharacter.Emotion.HAPPY);
    }
}
