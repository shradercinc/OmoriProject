using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSkills : Skills
{
    //Skill 1: Refreshments: Restore 50% of a friend's juice.
    //Skill 2: Cook: Restore 70% of a friend's health.
    //Skill 3: Homemade Jam: Bring back a friend who's toast. Restore 40% of their health.
    //Skill 4: Snack Time: Restore 40% of all friends' health.

    //Skill 1: Call Omori: Omori restores 40% of his health, 25% of his juice, and attacks again.
    //Skill 2: Call Aubrey: Aubreu restores 40% of his health, 25% of his juice, and attacks again.
    //Skill 3: Call Kel: Kel restores 40% of her health, 25% of her juice, and attacks again.

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
        skillTargets.Add(Target.NONE);

        //Skill 4:
        juiceCost.Add(20);
        skillTargets.Add(Target.ALLFRIENDS);

        //Follow Up 1:
        energyCost.Add(3);
        followUpRequire.Add(GameObject.Find("Omori").GetComponent<BattleCharacter>());

        //Follow Up 2:
        energyCost.Add(3);
        followUpRequire.Add(GameObject.Find("Aubrey").GetComponent<BattleCharacter>());

        //Follow Up 3:
        energyCost.Add(3);
        followUpRequire.Add(GameObject.Find("Kel").GetComponent<BattleCharacter>());

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

        if (manager.toast.Count > 0)
        {
            target = manager.toast[Random.Range(0, manager.toast.Count - 1)];
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
    public override void FollowUpOne()
    {
        manager.energy -= energyCost[0];
        BattleCharacter omori = followUpRequire[0];
        manager.AddText("Hero calls out to Omori.");

        int healing = (int)(omori.startingHealth * 0.4f);
        int juicing = (int)(omori.startingJuice * 0.25f);

        omori.TakeHealing(healing, juicing);
        omori.userSkills.BasicAttack(omori.nextTarget);
    }
    public override void FollowUpTwo()
    {
        manager.energy -= energyCost[1];
        BattleCharacter aubrey = followUpRequire[1];
        manager.AddText("Hero calls out to Aubrey.");

        int healing = (int)(aubrey.startingHealth * 0.4f);
        int juicing = (int)(aubrey.startingJuice * 0.25f);

        aubrey.TakeHealing(healing, juicing);
        aubrey.userSkills.BasicAttack(aubrey.nextTarget);
    }
    public override void FollowUpThree()
    {
        manager.energy -= energyCost[2];
        BattleCharacter kel = followUpRequire[2];
        manager.AddText("Hero calls out to Kel.");

        int healing = (int)(kel.startingHealth * 0.4f);
        int juicing = (int)(kel.startingJuice * 0.25f);

        kel.TakeHealing(healing, juicing);
        kel.userSkills.BasicAttack(kel.nextTarget);
    }
}
