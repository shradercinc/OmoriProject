using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuiceBlobWeapon : Weapon
{
    //Each turn, all Friends lose their Juice.

    public override void AffectUser()
    {
        user = FindObjectOfType<JuiceBlobSkills>().GetComponent<BattleCharacter>();
    }
    public override IEnumerator StartOfTurn()
    {
        for (int i = 0; i<manager.friends.Count; i++)
        {
            manager.AddText("Juice Blob drains all friends' juice.", true);
            BattleCharacter target = manager.friends[i];

            yield return target.DrainJuice(target.currJuice);
        }
    }
}
