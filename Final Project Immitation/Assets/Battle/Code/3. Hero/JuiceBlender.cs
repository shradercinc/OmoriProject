using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuiceBlender : Weapon
{
    public override void AffectUser()
    {
        user = FindObjectOfType<HeroSkills>().GetComponent<BattleCharacter>();
        description = "Hero starts with much less Health and Juice, but everyone regains 10 Juice each turn.";
        user.startingHealth -= 20;
        user.startingJuice -= 30;
    }

    public override IEnumerator StartOfTurn()
    {
        manager.AddText("The Juice Blender has finished blending.");
        for (int i = 0; i < manager.friends.Count; i++)
        {
            yield return manager.friends[i].TakeHealing(0, 10);
        }
    }
}
