using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSkills : Skills
{
    //Skill 1: Refreshments: Restore 50% of a friend's juice.
    //Skill 2: Cook: Restore 70% of a friend's health.
    //Skill 3: Homemade Jam: Bring back a friend who's toast. Restore 40% of their health.
    //Skill 4: Snack Time: Resotre 40% of all friends' health.

    public override void SetStartingStats()
    {
        //Attack:
        juiceCost.Add(0);
        skillTargets.Add(Target.FOE);

        //Skill 1:
        juiceCost.Add(15);
        skillTargets.Add(Target.FRIEND);

        //Skill 2:
        juiceCost.Add(10);
        skillTargets.Add(Target.FRIEND);

        //Skill 3:
        juiceCost.Add(20);
        skillTargets.Add(Target.FRIEND);

        //Skill 4:
        juiceCost.Add(20);
        skillTargets.Add(Target.ALLFRIENDS);

        user.friend = true;
        user.startingHealth = 56;
        user.startingJuice = 35;
        user.startingAttack = 10;
        user.startingDefense = 8;
        user.startingSpeed = 6;
        user.startingLuck = 0.03f;
        user.startingAccuracy = 1;
    }

    public override void UseSkillOne(BattleCharacter target)
    {
        user.currJuice -= juiceCost[1];
        target = RedirectTarget(target, 1);
        int juice = (int)(target.startingJuice / 2);
        manager.AddText("Hero makes some refreshments for " + target.name + ".");
        target.TakeHealing(0, juice);
    }
    public override void UseSkillTwo(BattleCharacter target)
    {
        user.currJuice -= juiceCost[2];
        target = RedirectTarget(target, 2);
        int health = (int) (target.startingHealth * 0.7);
        manager.AddText("Hero prepares a meal for " + target.name + ".");
        target.TakeHealing(health, 0);
    }
    public override void UseSkillThree(BattleCharacter target)
    {
        user.currJuice -= juiceCost[3];
        if (target.toast)
        {
            target.currHealth = (int)(target.startingHealth * 0.4);
            target.ResetStats();
            manager.AddText("Hero brings back " + target.name + ".");
            manager.ReturnToList(target);
        }
    }
    public override void UseSkillFour(BattleCharacter target)
    {
        user.currJuice -= juiceCost[4];
        manager.AddText("Hero brings snacks for everyone.");

        for (int i = 0; i < manager.friends.Count; i++)
        {
            int recover = (int)(manager.friends[i].startingHealth * 0.4);
            manager.friends[i].TakeHealing(recover, 0);
        }
    }
}
