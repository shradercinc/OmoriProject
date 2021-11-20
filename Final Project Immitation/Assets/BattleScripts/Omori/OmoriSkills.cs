using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OmoriSkills : Skills
{
    //Skill 1: Exploit: If the targetted Foe is Happy or Ecstatic, lower their speed. Then deal damage to them.
    //Skill 2: Stab: If Omori is Sad or Depressed, raise their attack. Then deal damage to a foe.
    //Skill 3: Sad Poem: Makes anyone Sad. If they're already Sad, they become Depressed.
    //Skill 4: Glare: Reduce the stats of a foe.

    //Skill 1: Chain Attack: Omori attacks 2 more times.
    //Skill 2: Trip: Deal damage to a foe. Lower their speed, and they become Sad.
    //Skill 3: Release Energy: Deal a lot of damage to all enemies.

    public override void SetStartingStats()
    {
        //Attack:
        juiceCost.Add(0);
        skillTargets.Add(Target.FOE);

        //Skill 1:
        juiceCost.Add(15);
        skillTargets.Add(Target.FOE);

        //Skill 2:
        juiceCost.Add(15);
        skillTargets.Add(Target.FOE);

        //Skill 3:
        juiceCost.Add(5);
        skillTargets.Add(Target.ANYONE);

        //Skill 4:
        juiceCost.Add(25);
        skillTargets.Add(Target.FOE);

        //Follow Up 1:
        energyCost.Add(3);
        followUpRequire.Add(GameObject.Find("Omori").GetComponent<BattleCharacter>());

        //Follow Up 2:
        energyCost.Add(3);
        followUpRequire.Add(GameObject.Find("Omori").GetComponent<BattleCharacter>());

        //Follow Up 3:
        energyCost.Add(10);

        user.friend = true;
        user.startingHealth = 52;
        user.startingJuice = 35;
        user.startingAttack = 13;
        user.startingDefense = 8;
        user.startingSpeed = 12;
        user.startingLuck = 0.05f;
        user.startingAccuracy = 1;
    }

    public override void UseSkillOne(BattleCharacter target)
    {
        user.currJuice -= juiceCost[1];
        target = RedirectTarget(target, 1);
        manager.AddText("Omori exploits " + target.name + "'s emotions.");

        if (target.currEmote == BattleCharacter.Emotion.HAPPY || target.currEmote == BattleCharacter.Emotion.ECSTATIC)
        {
            target.speedStat -= 0.15f;
            target.ResetStats();
            manager.AddText(target.name + "'s speed decreases.");
        }
        if (RollAccuracy(user.currAccuracy))
        {
            int critical = RollCritical(user.currLuck);
            int damage = (int)(critical * IsEffective(target) * (2 * user.currAttack - target.currDefense));
            target.TakeDamage(damage);
        }
    }
    public override void UseSkillTwo(BattleCharacter target)
    {
        user.currJuice -= juiceCost[2];
        target = RedirectTarget(target, 2);
        manager.AddText("Omori stabs " + target.name + ".");

        if (user.currEmote == BattleCharacter.Emotion.SAD || target.currEmote == BattleCharacter.Emotion.DEPRESSED)
        {
            user.attackStat += 0.15f;
            user.ResetStats();
            manager.AddText(user.name + "'s attack increases.");
        }
        if (RollAccuracy(user.currAccuracy))
        {
            int critical = RollCritical(user.currLuck);
            int damage = (int)(critical * IsEffective(target) * (2 * user.currAttack - target.currDefense));
            target.TakeDamage(damage);
        }
    }
    public override void UseSkillThree(BattleCharacter target)
    {
        user.currJuice -= juiceCost[3];
        target = RedirectTarget(target, 3);
        manager.AddText("Omori reads " + target.name + " a poem.");
        target.NewEmotion(BattleCharacter.Emotion.SAD);
    }
    public override void UseSkillFour(BattleCharacter target)
    {
        user.currJuice -= juiceCost[4];
        target = RedirectTarget(target, 4);
        manager.AddText("Omori glares at " + target.name + ".");

        target.attackStat -= 0.1f;
        target.defenseStat -= 0.1f;
        target.speedStat -= 0.1f;
        target.luckStat -= 0.1f;
        target.accuracyStat -= 0.1f;
        manager.AddText("All of " + target.name + "'s stats decrease.");
        target.ResetStats();
    }
    public override void FollowUpOne()
    {
        manager.energy -= energyCost[0];
        manager.AddText("Omori attacks two more times.");

        user.userSkills.BasicAttack(user.nextTarget);
        user.userSkills.BasicAttack(user.nextTarget);
    }
    public override void FollowUpTwo()
    {
        manager.energy -= energyCost[1];
        BattleCharacter target = manager.foes[Random.Range(0, manager.foes.Count - 1)];
        manager.AddText("Omori makes " + target.name + " trip and fall over.");

        target.speedStat -= 0.15f;
        manager.AddText(target.name + "'s speed decreases.");
        target.NewEmotion(BattleCharacter.Emotion.SAD);

        int critical = RollCritical(user.currLuck);
        int damage = (int)(critical * IsEffective(target) * (user.currAttack + user.currLuck - target.currDefense));
        target.TakeDamage(damage);
    }
    public override void FollowUpThree()
    {
        manager.energy -= energyCost[2];
        manager.AddText("Omori and friends come together and use their ultimate attack.");

        for (int i = 0; i < manager.foes.Count; i++)
        {
            BattleCharacter target = manager.foes[i];
            int critical = RollCritical(user.currLuck);
            int damage = (int)(critical * IsEffective(target) * (4.5 * user.currAttack));
            target.TakeDamage(damage);
        }
    }
}
