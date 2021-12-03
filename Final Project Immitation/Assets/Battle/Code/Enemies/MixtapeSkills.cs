using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MixtapeSkills : Skills
{
    //Skill 1: Entangle: Reduce a Friend's speed. Then deal a small amount of damage to them.

    public override void SetStartingStats()
    {
        //Attack:
        skillTargets.Add(Target.FRIEND);
        //Skill 1:
        skillTargets.Add(Target.FRIEND);

        user = gameObject.GetComponent<BattleCharacter>();
        user.friend = false;
        user.startingHealth = 150;
        user.startingAttack = 30;
        user.startingDefense = 10;
        user.startingSpeed = 15;
        user.startingLuck = 0.1f;
        user.startingAccuracy = 0.9f;
    }

    //targets the fastest friend
    public override BattleCharacter ChooseTarget(int n)
    {
        List<BattleCharacter> friends = manager.friends;
        friends = friends.OrderByDescending(o => o.currSpeed).ToList();
        return friends[0];
    }

    public override IEnumerator UseSkillOne(BattleCharacter target)
    {
        target = RedirectTarget(target, 1);
        manager.AddText("Mixtape entangles " + target.name + " in wires.", true);

        target.speedStat -= 0.15f;
        yield return target.ResetStats();
        manager.AddText(target.name + "'s speed decreases.");

        if (RollAccuracy(user.currAccuracy))
        {
            int critical = RollCritical(user.currLuck);
            int damage = (int)(critical * IsEffective(target) * (0.5f * user.currAttack - target.currDefense));
            yield return target.TakeDamage(damage);
        }
    }
}
