using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OmoriSkills : Skills
{
    public override void SetStartingStats()
    {
        //Attack:
        skillNames.Add("Attack");
        juiceCost.Add(0);
        skillTargets.Add(Target.FOE);
        skillDescription.Add("");

        //Skill 1:
        skillNames.Add("Mock");
        juiceCost.Add(15);
        skillTargets.Add(Target.FOE);
        skillDescription.Add("Make a Foe Angry and reduce their Attack. Then deal damage to them.");

        //Skill 2:
        skillNames.Add("Stab");
        juiceCost.Add(15);
        skillTargets.Add(Target.FOE);
        skillDescription.Add("Deals damage to a Foe. If Omori is Sad or Depressed, he first raises his Attack.");

        //Skill 3:
        skillNames.Add("Sad Poem");
        juiceCost.Add(5);
        skillTargets.Add(Target.FRIEND);
        skillDescription.Add("Both Omori and another Friend become Sad.");

        //Skill 4:
        skillNames.Add("Stare");
        juiceCost.Add(25);
        skillTargets.Add(Target.ALLFOES);
        skillDescription.Add("Reduce the stats of all Foes.");

        //Follow Up 1:
        skillNames.Add("Blind Rage");
        energyCost.Add(3);
        followUpRequire.Add(GameObject.Find("Omori").GetComponent<BattleCharacter>());
        skillDescription.Add("Omori becomes Angry. Then deal damage to a Foe.");

        //Follow Up 2:
        skillNames.Add("Trip Foe");
        energyCost.Add(3);
        followUpRequire.Add(GameObject.Find("Omori").GetComponent<BattleCharacter>());
        skillDescription.Add("A Foe becomes Sad and loses Speed. Then deal damage to them.");

        //Follow Up 3:
        skillNames.Add("Release Energy");
        energyCost.Add(10);
        skillDescription.Add("Deals a lot of damage to all Foes. All Friends regain their Juice.");

        user = gameObject.GetComponent<BattleCharacter>();
        user.friend = true;
        user.order = 0;

        user = gameObject.GetComponent<BattleCharacter>();
        user.startingHealth = 110;
        user.startingJuice = 50;
        user.startingAttack = 40;
        user.startingDefense = 16;
        user.startingSpeed = 15;
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

            yield return target.NewEmotion(BattleCharacter.Emotion.ANGRY);
            target.attackStat -= 0.2f;
            yield return target.ResetStats();
            manager.AddText(target.name + "'s Attack decreases.");
            
            if (RollAccuracy(user.currAccuracy))
            {
                int critical = RollCritical(user.currLuck);
                int damage = (int)(critical * IsEffective(target) * (1.5 * user.currAttack - target.currDefense));
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
                manager.AddText("Omori's Attack increases.");
            }
            if (RollAccuracy(user.currAccuracy))
            {
                int critical = RollCritical(user.currLuck);
                int damage = (int)(critical * IsEffective(target) * (1.5 * user.currAttack - target.currDefense));
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
            List<BattleCharacter> allEnemies = manager.foes;
            for (int i = 0; i < allEnemies.Count; i++)
            {
                target = allEnemies[i];
                manager.AddText("Omori glares at " + target.name + ".", true);

                target.attackStat -= 0.15f;
                target.defenseStat -= 0.15f;
                target.speedStat -= 0.15f;
                target.luckStat -= 0.15f;
                target.accuracyStat -= 0.15f;

                manager.AddText("All of " + target.name + "'s stats decrease.");
                yield return target.ResetStats();
                yield return new WaitForSeconds(1);
            }
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
        manager.AddText(target.name + "'s Speed decreases.");
        yield return target.NewEmotion(BattleCharacter.Emotion.SAD);

        int critical = RollCritical(user.currLuck);
        int damage = (int)(critical * IsEffective(target) * (user.currAttack + user.currLuck - target.currDefense));
        yield return target.TakeDamage(damage);
    }
    public override IEnumerator FollowUpThree()
    {
        yield return manager.AddEnergy(-energyCost[2]);
        for (int i = 0; i < manager.friends.Count; i++)
        {
            manager.AddText("Omori and friends regain their Juice.", true);
            BattleCharacter target = manager.friends[i];

            yield return target.TakeHealing(0, target.startingJuice);
            yield return new WaitForSeconds(1);
        }
        for (int i = 0; i < manager.foes.Count; i++)
        {
            manager.AddText("Omori and friends then use their ultimate attack.", true);
            BattleCharacter target = manager.foes[i];

            int critical = RollCritical(user.currLuck);
            int damage = (int)(critical * IsEffective(target) * (3.5 * user.currAttack));
            yield return target.TakeDamage(damage);
            yield return new WaitForSeconds(1);
        }
    }
}
