using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillow : Weapon
{
    int unalteredAttack;
    int unalteredSpeed;

    public override void AffectUser()
    {
        user = FindObjectOfType<AubreySkills>().GetComponent<BattleCharacter>();
        description = "If Aubrey has more than half health, she has higher Attack and Speed. Otherwise, she has lower Attack and Speed.";
        unalteredAttack = user.startingAttack;
        unalteredSpeed = user.startingSpeed;
    }
    public override IEnumerator StartOfTurn()
    {
        if ((float)user.currHealth / user.startingHealth > 0.5f)
        {
            manager.AddText("Aubrey is too energetic to sleep.", true);
            yield return new WaitForSeconds(0.5f);
            manager.AddText("Aubrey's Attack and Speed increases.");

            user.startingAttack = unalteredAttack + 4;
            user.startingSpeed = unalteredSpeed + 4;
            yield return user.ResetStats();
            yield return new WaitForSeconds(0.5f);
        }
        else
        {
            manager.AddText("Aubrey is exhausted and feels like sleeping.", true);
            yield return new WaitForSeconds(0.5f);
            manager.AddText("Aubrey's Attack and Speed decreases.");

            user.startingAttack = unalteredAttack - 4;
            user.startingSpeed = unalteredSpeed - 4;
            yield return user.ResetStats();
            yield return new WaitForSeconds(0.5f);
        }
    }
}
