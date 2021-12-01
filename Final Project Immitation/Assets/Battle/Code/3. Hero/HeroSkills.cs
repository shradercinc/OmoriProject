using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSkills : Skills
{
    //Skill 1: Call Omori: Omori restores 40% of his health, 25% of his juice, loses their emotion, and attacks again.
    //Skill 2: Call Aubrey: Aubreu restores 40% of his health, 25% of her juice, loses their emotion, and attacks again.
    //Skill 3: Call Kel: Kel restores 40% of her health, 25% of his juice, loses their emotion, and attacks again.

    public override void SetStartingStats()
    {
        //Attack:
        skillNames.Add("Attack");
        juiceCost.Add(0);
        skillTargets.Add(Target.FOE);
        skillDescription.Add("");

        //Skill 1:
        skillNames.Add("Refreshments");
        juiceCost.Add(10);
        skillTargets.Add(Target.FRIEND);
        skillDescription.Add("A Friend regains all their Juice.");

        //Skill 2:
        skillNames.Add("Cook");
        juiceCost.Add(10);
        skillTargets.Add(Target.FRIEND);
        skillDescription.Add("A Friend regains all their Health.");

        //Skill 3:
        skillNames.Add("Homemade Jam");
        juiceCost.Add(20);
        skillTargets.Add(Target.NONE);
        skillDescription.Add("Bring back a Friend who's Toast.");

        //Skill 4:
        skillNames.Add("Snack Time");
        juiceCost.Add(20);
        skillTargets.Add(Target.ALLFRIENDS);
        skillDescription.Add("All Friends regain a bit of Health.");

        //Follow Up 1:
        skillNames.Add("Call Omori");
        energyCost.Add(3);
        followUpRequire.Add(GameObject.Find("Omori").GetComponent<BattleCharacter>());
        skillDescription.Add("Omori regains some Health and Juice, returns to Neutral, and attacks.");

        //Follow Up 2:
        skillNames.Add("Call Aubrey");
        energyCost.Add(3);
        followUpRequire.Add(GameObject.Find("Aubrey").GetComponent<BattleCharacter>());
        skillDescription.Add("Aubrey regains some Health and Juice, returns to Neutral, and attacks.");

        //Follow Up 3:
        skillNames.Add("Call Kel");
        energyCost.Add(3);
        followUpRequire.Add(GameObject.Find("Kel").GetComponent<BattleCharacter>());
        skillDescription.Add("Kel regains some Health and Juice, returns to Neutral, and attacks.");

        user = gameObject.GetComponent<BattleCharacter>();
        user.friend = true;
        user.order = 3;

        user.startingHealth = 59;
        user.startingJuice = 50;
        user.startingAttack = 15;
        user.startingDefense = 10;
        user.startingSpeed = 9;
        user.startingLuck = 0.03f;
        user.startingAccuracy = 1;
    }

    public override IEnumerator UseSkillOne(BattleCharacter target)
    {
        bool check = juiceCost[1] <= user.currJuice;
        yield return user.DrainJuice(juiceCost[1]);

        if (check)
        {
            target = RedirectTarget(target, 1);
            manager.AddText("Hero makes some refreshments for " + target.name + ".", true);

            int juice = (int)(target.startingJuice);
            yield return target.TakeHealing(0, juice);
        }
    }
    public override IEnumerator UseSkillTwo(BattleCharacter target)
    {
        bool check = juiceCost[2] <= user.currJuice;
        yield return user.DrainJuice(juiceCost[2]);

        if (check)
        {
            target = RedirectTarget(target, 2);
            manager.AddText("Hero prepares a meal for " + target.name + ".", true);

            int health = (int)(target.startingHealth);
            yield return target.TakeHealing(health, 0);
        }
    }
    public override IEnumerator UseSkillThree(BattleCharacter target)
    {
        bool check = juiceCost[3] <= user.currJuice;
        yield return user.DrainJuice(juiceCost[3]);

        if (check)
        {
            if (manager.toast.Count > 0)
            {
                target = manager.toast[Random.Range(0, manager.toast.Count - 1)];
                manager.AddText("Hero brings back " + target.name + ".", true);

                target.currHealth = (int)(target.startingHealth * 0.5);
                yield return target.ResetStats();
                manager.ReturnToList(target);
            }
        }
    }
    public override IEnumerator UseSkillFour(BattleCharacter target)
    {
        bool check = juiceCost[4] <= user.currJuice;
        yield return user.DrainJuice(juiceCost[4]);

        if (check)
        {
            manager.AddText("Hero brings snacks for everyone.", true);

            for (int i = 0; i < manager.friends.Count; i++)
            {
                int recover = (int)(manager.friends[i].startingHealth * 0.5);
                yield return manager.friends[i].TakeHealing(recover, 0);
            }
        }
    }
    public override IEnumerator FollowUpOne()
    {
        yield return manager.AddEnergy(-energyCost[0]);
        BattleCharacter omori = followUpRequire[0];
        manager.AddText("Hero calls out to Omori.", true);

        int healing = (int)(omori.startingHealth * 0.75f);
        int juicing = (int)(omori.startingJuice * 0.4f);

        yield return omori.NewEmotion(BattleCharacter.Emotion.NEUTRAL);
        yield return omori.TakeHealing(healing, juicing);
        yield return omori.userSkills.BasicAttack(omori.nextTarget);
    }
    public override IEnumerator FollowUpTwo()
    {
        yield return manager.AddEnergy(-energyCost[1]);
        BattleCharacter aubrey = followUpRequire[1];
        manager.AddText("Hero calls out to Aubrey.", true);

        int healing = (int)(aubrey.startingHealth * 0.75f);
        int juicing = (int)(aubrey.startingJuice * 0.4f);

        yield return aubrey.NewEmotion(BattleCharacter.Emotion.NEUTRAL);
        yield return aubrey.TakeHealing(healing, juicing);
        yield return aubrey.userSkills.BasicAttack(aubrey.nextTarget);
    }
    public override IEnumerator FollowUpThree()
    {
        yield return manager.AddEnergy(-energyCost[2]);
        BattleCharacter kel = followUpRequire[2];
        manager.AddText("Hero calls out to Kel.", true);

        int healing = (int)(kel.startingHealth * 0.75f);
        int juicing = (int)(kel.startingJuice * 0.4f);

        yield return kel.NewEmotion(BattleCharacter.Emotion.NEUTRAL);
        yield return kel.TakeHealing(healing, juicing);
        yield return kel.userSkills.BasicAttack(kel.nextTarget);
    }
}
