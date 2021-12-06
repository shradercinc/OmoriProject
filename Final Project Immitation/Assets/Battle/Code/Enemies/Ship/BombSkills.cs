using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSkills : Skills
{
    //This does nothing until it dies, or until 3 turns pass.

    int turnCount = 3;

    public override void SetStartingStats()
    {
        //Attack:
        skillTargets.Add(Target.ALLFRIENDS);

        user = gameObject.GetComponent<BattleCharacter>();
        user.friend = false;
        user.startingHealth = 200;
        user.startingAttack = 20;
        user.startingDefense = 0;
        user.startingSpeed = -500;
        user.startingLuck = 0;
        user.startingAccuracy = 1;
    }

    public override IEnumerator BasicAttack(BattleCharacter target)
    {
        if (turnCount == 0)
        {
            yield return user.TakeDamage(user.startingHealth);
            for (int i = 0; i < allTargets.Count; i++)
            {
                manager.AddText("The Bomb explodes.", true);
                BattleCharacter target = allTargets[i];

                int critical = userSkills.RollCritical(user.currLuck);
                int damage = (int)(critical * userSkills.IsEffective(target) * (2 * user.currAttack - target.currDefense));
                yield return target.TakeDamage(damage);
            }
        }
        else
        {
            manager.AddText($"The Bomb's countdown is at {turnCount}.", true);
            yield return new WaitForSeconds(0.5f);
            turnCount--;
        }
    }
}
