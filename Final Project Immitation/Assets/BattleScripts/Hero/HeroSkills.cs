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
        skillNames.Add("Attack");
        juiceCost.Add(0);
        skillTargets.Add(Target.FOE);

        //Skill 1:
        skillNames.Add("Refreshments");
        juiceCost.Add(15);
        skillTargets.Add(Target.FRIEND);

        //Skill 2:
        skillNames.Add("Cook");
        juiceCost.Add(10);
        skillTargets.Add(Target.FRIEND);

        //Skill 3:
        skillNames.Add("Homemade Jam");
        juiceCost.Add(20);
        skillTargets.Add(Target.NONE);

        //Skill 4:
        skillNames.Add("Snack Time");
        juiceCost.Add(20);
        skillTargets.Add(Target.ALLFRIENDS);

        //Follow Up 1:
        skillNames.Add("Call Omori");
        energyCost.Add(3);
        followUpRequire.Add(GameObject.Find("Omori").GetComponent<BattleCharacter>());

        //Follow Up 2:
        skillNames.Add("Call Aubrey");
        energyCost.Add(3);
        followUpRequire.Add(GameObject.Find("Aubrey").GetComponent<BattleCharacter>());

        //Follow Up 3:
        skillNames.Add("Call Kel");
        energyCost.Add(3);
        followUpRequire.Add(GameObject.Find("Kel").GetComponent<BattleCharacter>());

        user.friend = true;
        user.order = 3;

        user.startingHealth = 56;
        user.startingJuice = 35;
        user.startingAttack = 10;
        user.startingDefense = 8;
        user.startingSpeed = 6;
        user.startingLuck = 0.03f;
        user.startingAccuracy = 1;
    }

    public override IEnumerator UseSkillOne(BattleCharacter target)
    {
        if (user.DrainJuice(juiceCost[1]))
        {
            target = RedirectTarget(target, 1);
            manager.AddText("Hero makes some refreshments for " + target.name + ".", true);

            int juice = (int)(target.startingJuice / 2);
            target.TakeHealing(0, juice);
        }
        yield return null;
    }
    public override IEnumerator UseSkillTwo(BattleCharacter target)
    {
        if (user.DrainJuice(juiceCost[2]))
        {
            target = RedirectTarget(target, 2);
            manager.AddText("Hero prepares a meal for " + target.name + ".", true);

            int health = (int)(target.startingHealth * 0.7);
            target.TakeHealing(health, 0);
        }
        yield return null;
    }
    public override IEnumerator UseSkillThree(BattleCharacter target)
    {
        if (user.DrainJuice(juiceCost[3]))
        {
            if (manager.toast.Count > 0)
            {
                target = manager.toast[Random.Range(0, manager.toast.Count - 1)];
                manager.AddText("Hero brings back " + target.name + ".", true);

                target.currHealth = (int)(target.startingHealth * 0.4);
                target.ResetStats();
                manager.ReturnToList(target);
            }
        }
        yield return null;
    }
    public override IEnumerator UseSkillFour(BattleCharacter target)
    {
        if (user.DrainJuice(juiceCost[4]))
        {
            manager.AddText("Hero brings snacks for everyone.", true);

            for (int i = 0; i < manager.friends.Count; i++)
            {
                int recover = (int)(manager.friends[i].startingHealth * 0.4);
                manager.friends[i].TakeHealing(recover, 0);
            }
        }
        yield return null;
    }
    public override IEnumerator FollowUpOne()
    {
        manager.energy -= energyCost[0];
        BattleCharacter omori = followUpRequire[0];
        manager.AddText("Hero calls out to Omori.", true);

        int healing = (int)(omori.startingHealth * 0.4f);
        int juicing = (int)(omori.startingJuice * 0.25f);

        omori.TakeHealing(healing, juicing);
        omori.userSkills.BasicAttack(omori.nextTarget);
        yield return null;
    }
    public override IEnumerator FollowUpTwo()
    {
        manager.energy -= energyCost[1];
        BattleCharacter aubrey = followUpRequire[1];
        manager.AddText("Hero calls out to Aubrey.", true);

        int healing = (int)(aubrey.startingHealth * 0.4f);
        int juicing = (int)(aubrey.startingJuice * 0.25f);

        aubrey.TakeHealing(healing, juicing);
        aubrey.userSkills.BasicAttack(aubrey.nextTarget);
        yield return null;
    }
    public override IEnumerator FollowUpThree()
    {
        manager.energy -= energyCost[2];
        BattleCharacter kel = followUpRequire[2];
        manager.AddText("Hero calls out to Kel.", true);

        int healing = (int)(kel.startingHealth * 0.4f);
        int juicing = (int)(kel.startingJuice * 0.25f);

        kel.TakeHealing(healing, juicing);
        kel.userSkills.BasicAttack(kel.nextTarget);
        yield return null;
    }
}
