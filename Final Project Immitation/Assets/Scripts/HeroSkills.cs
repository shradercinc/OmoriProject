using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSkills : Skills
{
    //Skill 1: Refresh: Restore 50% of a friend's juice.
    //Skill 2: Cook: Restore 70% of a friend's health.
    //Skill 3: Homemade Jam: Bring back a friend who's toast. Restore 40% of their health.
    //Skill 4: Snack Time: Resotre 40% of all friends' health.

    public override void SetStartingStats()
    {
        juiceCost.Add(15);
        juiceCost.Add(10);
        juiceCost.Add(20);
        juiceCost.Add(20);

        skillTargets.Add(Target.FRIEND);
        skillTargets.Add(Target.FRIEND);
        skillTargets.Add(Target.FRIEND);
        skillTargets.Add(Target.ALLFRIENDS);

        user.friend = true;
        user.startingHealth = 56;
        user.startingJuice = 35;
        user.startingAttack = 10;
        user.startingDefense = 8;
        user.startingSpeed = 6;
        user.startingLuck = 3;
    }

    public override void UseSkillOne()
    {
    }
    public override void UseSkillTwo()
    {
    }
    public override void UseSkillThree()
    {
    }
    public override void UseSkillFour()
    {
        for (int i = 0; i<manager.friends.Count; i++)
        {
            int recover = (int) (manager.friends[i].startingHealth * 0.4);
            manager.friends[i].TakeDamage(recover);
        }
    }
    public override void UseSkillOne(BattleCharacter target)
    {
        target = RedirectTarget(target, 1);

        target.currJuice += (int)(target.startingJuice / 2);
        target.ResetStats();
    }
    public override void UseSkillTwo(BattleCharacter target)
    {
        target = RedirectTarget(target, 2);
        int recover = (int) (target.startingHealth * 0.7);
        target.TakeDamage(recover);
    }
    public override void UseSkillThree(BattleCharacter target)
    {
        if (target.toast)
        {
            target.toast = false;
            target.currHealth = (int)(target.startingHealth * 0.4);
            manager.ReturnToList(target);
        }
    }
    public override void UseSkillFour(BattleCharacter target)
    {
    }
}
