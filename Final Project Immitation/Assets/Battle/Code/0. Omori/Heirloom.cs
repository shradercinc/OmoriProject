using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heirloom : Weapon
{
    public override void AffectUser()
    {
        user = FindObjectOfType<OmoriSkills>().GetComponent<BattleCharacter>();
        description = "Omori gets Sad every turn. Increases Attack and Luck.";
        user.startingAttack += 4;
        user.startingLuck += 4;
    }

    public override IEnumerator StartOfTurn()
    {
        manager.AddText("Omori's Heirloom reminds him of bitter memories.", true);
        yield return user.NewEmotion(BattleCharacter.Emotion.SAD);
    }
}
