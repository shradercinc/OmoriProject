using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatWeapon : Weapon
{
    public override void AffectUser()
    {
        user = FindObjectOfType<BombSkills>().GetComponent<BattleCharacter>();
    }

    public override IEnumerator StartOfTurn()
    {
        if (user.invisible)
        {
            user.invisible = false;
            manager.AddText("Lab Rat's invisibility wears off.", true);
            yield return new WaitForSeconds(0.5f);
        }

        for (int i = 0; i<manager.friends.Count; i++)
        {
            manager.AddText("Lab Rat collects some juice.", true);
            BattleCharacter target = manager.friends[i];
            yield return target.DrainJuice(target.currJuice / 6);
        }
    }
}
