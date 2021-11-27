using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OmoriSkills : Skills
{
    //Skill 1: Mock: If the targetted Foe is Angry or Enraged, lower their attack. Then deal damage to them..
    //Skill 2: Stab: If Omori is Sad or Depressed, raise their attack. Then deal damage to a foe.
    //Skill 3: Sad Poem: Omori and another target both become Sad.
    //Skill 4: Glare: Reduce the stats of a foe.

    //Skill 1: Blind Rage: Omori becomes Angry and deals damage to a foe.
    //Skill 2: Trip Foe: Deal damage to a foe. Lower their speed, and they become Sad.
    //Skill 3: Release Energy: Deal a lot of damage to all enemies.

    public override void SetStartingStats()
    {
        //Attack:
        skillNames.Add("Attack");
        juiceCost.Add(0);
        skillTargets.Add(Target.FOE);

        //Skill 1:
        skillNames.Add("Mock");
        juiceCost.Add(15);
        skillTargets.Add(Target.FOE);

        //Skill 2:
        skillNames.Add("Stab");
        juiceCost.Add(15);
        skillTargets.Add(Target.FOE);

        //Skill 3:
        skillNames.Add("Sad Poem");
        juiceCost.Add(5);
        skillTargets.Add(Target.ANYONE);

        //Skill 4:
        skillNames.Add("Stare");
        juiceCost.Add(25);
        skillTargets.Add(Target.FOE);

        //Follow Up 1:
        skillNames.Add("Blind Rage");
        energyCost.Add(3);
        followUpRequire.Add(GameObject.Find("Omori").GetComponent<BattleCharacter>());

        //Follow Up 2:
        skillNames.Add("Trip Foe");
        energyCost.Add(3);
        followUpRequire.Add(GameObject.Find("Omori").GetComponent<BattleCharacter>());

        //Follow Up 3:
        skillNames.Add("Release Energy");
        energyCost.Add(10);

        user.friend = true;
        user.order = 0;

        user.startingHealth = 52;
        user.startingJuice = 35;
        user.startingAttack = 13;
        user.startingDefense = 8;
        user.startingSpeed = 12;
        user.startingLuck = 0.05f;
        user.startingAccuracy = 1;
    }

    public override IEnumerator UseSkillOne(BattleCharacter target)
    {
        bool check = juiceCost[1] <= user.currJuice;
        yield return user.DrainJuice(juiceCost[1]);

        if (check)
        {
            target = RedirectTarget(target, 1);
            manager.AddText("Omori mocks " + target.name + ".", true);

            if (target.currEmote == BattleCharacter.Emotion.ANGRY || target.currEmote == BattleCharacter.Emotion.ENRAGED)
            {
                target.attackStat -= 0.15f;
                yield return target.ResetStats();
                manager.AddText(target.name + "'s attack decreases.");
            }
            if (RollAccuracy(user.currAccuracy))
            {
                int critical = RollCritical(user.currLuck);
                int damage = (int)(critical * IsEffective(target) * (2 * user.currAttack - target.currDefense));
                yield return target.TakeDamage(damage);
            }
        }
    }
    public override IEnumerator UseSkillTwo(BattleCharacter target)
    {
        bool check = juiceCost[2] <= user.currJuice;
        yield return user.DrainJuice(juiceCost[2]);

        if (check)
        {
            target = RedirectTarget(target, 2);
            manager.AddText("Omori stabs " + target.name + ".", true);

            if (user.currEmote == BattleCharacter.Emotion.SAD || target.currEmote == BattleCharacter.Emotion.DEPRESSED)
            {
                user.attackStat += 0.15f;
                yield return user.ResetStats();
                manager.AddText(user.name + "'s attack increases.");
            }
            if (RollAccuracy(user.currAccuracy))
            {
                int critical = RollCritical(user.currLuck);
                int damage = (int)(critical * IsEffective(target) * (2 * user.currAttack - target.currDefense));
                yield return target.TakeDamage(damage);
            }
        }
    }
    public override IEnumerator UseSkillThree(BattleCharacter target)
    {
        bool check = juiceCost[3] <= user.currJuice;
        yield return user.DrainJuice(juiceCost[3]);

        if (check)
        {
            target = RedirectTarget(target, 3);
            manager.AddText("Omori reads " + target.name + " a poem.", true);
            yield return user.NewEmotion(BattleCharacter.Emotion.SAD);
            yield return target.NewEmotion(BattleCharacter.Emotion.SAD);
        }
    }
    public override IEnumerator UseSkillFour(BattleCharacter target)
    {
        bool check = juiceCost[4] <= user.currJuice;
        yield return user.DrainJuice(juiceCost[4]);

        if (check)
        {
            target = RedirectTarget(target, 4);
            manager.AddText("Omori glares at " + target.name + ".", true);

            target.attackStat -= 0.1f;
            target.defenseStat -= 0.1f;
            target.speedStat -= 0.1f;
            target.luckStat -= 0.1f;
            target.accuracyStat -= 0.1f;

            manager.AddText("All of " + target.name + "'s stats decrease.");
            yield return target.ResetStats();
        }
    }
    public override IEnumerator FollowUpOne()
    {
        yield return manager.AddEnergy(-energyCost[0]);
        BattleCharacter target = RedirectTarget(user.nextTarget, 0);
        manager.AddText("Omori gets blinded by anger.", true);
        yield return user.NewEmotion(BattleCharacter.Emotion.ANGRY);

        int critical = RollCritical(user.currLuck);
        int damage = (int)(critical * IsEffective(target) * (user.currAttack + user.currLuck - target.currDefense));
        yield return target.TakeDamage(damage);
    }
    public override IEnumerator FollowUpTwo()
    {
        yield return manager.AddEnergy(-energyCost[1]);
        BattleCharacter target = RedirectTarget(user.nextTarget, 0);
        manager.AddText("Omori makes " + target.name + " trip and fall over.", true);

        target.speedStat -= 0.15f;
        manager.AddText(target.name + "'s speed decreases.");
        yield return target.NewEmotion(BattleCharacter.Emotion.SAD);

        int critical = RollCritical(user.currLuck);
        int damage = (int)(critical * IsEffective(target) * (user.currAttack + user.currLuck - target.currDefense));
        yield return target.TakeDamage(damage);
    }
    public override IEnumerator FollowUpThree()
    {
        yield return manager.AddEnergy(-energyCost[2]);
        for (int i = 0; i < manager.foes.Count; i++)
        {
            manager.AddText("Omori and friends come together and use their ultimate attack.", true);
            BattleCharacter target = manager.foes[i];

            int critical = RollCritical(user.currLuck);
            int damage = (int)(critical * IsEffective(target) * (4.5 * user.currAttack));
            yield return target.TakeDamage(damage);
            yield return new WaitForSeconds(1);
        }
    }
}
