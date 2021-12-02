using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heirloom : Weapon
{
    public override void AffectUser()
    {
        user = FindObjectOfType<OmoriSkills>().GetComponent<BattleCharacter>();
        description = "Omori gets Sad every turn. Increases Attack and Luck.";
        user.attackStat += 0.25f;
        user.luckStat += 0.25f;
    }

    public override IEnumerator StartOfTurn()
    {
        manager.AddText("Omori's Heirloom reminds him of bitter memories.", true);
        yield return user.NewEmotion(BattleCharacter.Emotion.SAD);
    }
}
