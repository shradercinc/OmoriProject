using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KelSkills : Skills
{
    //SKILL 1: Snowball Fight: If the targetted foe is Sad or Depressed, lower their defense. Then deal damage to them.
    //SKILL 2: Headbutt: If Kel is Angry or Enraged, increase their luck. Then deal damage to a foe.
    //SKILL 3: Annoy: Makes anyone Angry. If they're already Angry, they become Enraged.
    //SKILL 4: Run N' Gun; Deal damage to a foe, based on your Speed instead of your Attack.

    //Follow Up 1: Pass to Omori: Omori becomes happy and deals damage to a foe.
    //Follow Up 2: Pass to Aubrey: Aubrey deals extra damage to a foe.
    //Follow Up 3: Pass to Hero: Kel deals damage to all foes.

    public override void SetStartingStats()
    {
        //Attack:
        skillNames.Add("Attack");
        juiceCost.Add(0);
        skillTargets.Add(Target.FOE);

        //Skill 1:
        skillNames.Add("Snowball Fight");
        juiceCost.Add(15);
        skillTargets.Add(Target.FOE);

        //Skill 2:
        skillNames.Add("Headbutt");
        juiceCost.Add(15);
        skillTargets.Add(Target.FOE);

        //Skill 3:
        skillNames.Add("Annoy");
        juiceCost.Add(5);
        skillTargets.Add(Target.ANYONE);

        //Skill 4:
        skillNames.Add("Run N' Gun");
        juiceCost.Add(10);
        skillTargets.Add(Target.FOE);

        //Follow Up 1:
        skillNames.Add("Pass to Omori");
        energyCost.Add(3);
        followUpRequire.Add(GameObject.Find("Omori").GetComponent<BattleCharacter>());

        //Follow Up 2:
        skillNames.Add("Pass to Aubrey");
        energyCost.Add(3);
        followUpRequire.Add(GameObject.Find("Aubrey").GetComponent<BattleCharacter>());

        //Follow Up 3:
        skillNames.Add("Pass to Hero");
        energyCost.Add(3);
        followUpRequire.Add(GameObject.Find("Hero").GetComponent<BattleCharacter>());

        user.friend = true;
        user.order = 2;

        user.startingHealth = 48;
        user.startingJuice = 41;
        user.startingAttack = 13;
        user.startingDefense = 6;
        user.startingSpeed = 15;
        user.startingLuck = 0.07f;
        user.startingAccuracy = 1;
    }

    public override void UseSkillOne(BattleCharacter target)
    {
        user.currJuice -= juiceCost[1];
        target = RedirectTarget(target, 1);
        manager.AddText("Kel starts a snowball fight against " + target.name + ".");

        if (target.currEmote == BattleCharacter.Emotion.SAD || target.currEmote == BattleCharacter.Emotion.DEPRESSED)
        {
            target.defenseStat -= 0.15f;
            target.ResetStats();
            manager.AddText(target.name + "'s defense decreases.");
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
        manager.AddText("Kel headbutts " + target.name + ".");

        if (user.currEmote == BattleCharacter.Emotion.ANGRY || target.currEmote == BattleCharacter.Emotion.ENRAGED)
        {
            user.luckStat += 0.15f;
            user.ResetStats();
            manager.AddText(user.name + "'s luck increases.");
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
        manager.AddText("Kel annoys " + target.name + ".");
        target.NewEmotion(BattleCharacter.Emotion.ANGRY);
    }
    public override void UseSkillFour(BattleCharacter target)
    {
        user.currJuice -= juiceCost[4];
        target = RedirectTarget(target, 4);
        manager.AddText("Kel throws a quick shot at " + target.name + ".");

        if (RollAccuracy(user.currAccuracy))
        {
            int critical = RollCritical(user.currLuck);
            int damage = (int)(critical * IsEffective(target) * (1.5*user.currSpeed - target.currDefense));
            target.TakeDamage(damage);
        }
    }
    public override void FollowUpOne()
    {
        BattleCharacter omori = followUpRequire[0];
        manager.AddText("Kel passes the ball to Omori, who then throws it.");
        omori.NewEmotion(BattleCharacter.Emotion.HAPPY);

        BattleCharacter target = manager.foes[Random.Range(0, manager.foes.Count - 1)];
        int critical = RollCritical(omori.currLuck);
        int damage = (int)(critical * IsEffective(target) * (1.5 * user.currAttack + 1.5 * omori.currAttack - target.currDefense));
        target.TakeDamage(damage);
    }
    public override void FollowUpTwo()
    {
        BattleCharacter aubrey = followUpRequire[1];
        BattleCharacter target = manager.foes[Random.Range(0, manager.foes.Count - 1)];
        manager.AddText("Kel passes the ball to Aubrey, who knocks it out of the park.");

        int critical = RollCritical(aubrey.currLuck);
        int damage = (int)(critical * IsEffective(target) * (2 * user.currAttack + 2 * aubrey.currAttack - target.currDefense));
        target.TakeDamage(damage);
    }
    public override void FollowUpThree()
    {
        BattleCharacter hero = followUpRequire[2];
        manager.AddText("Kel passes the ball to Hero, who throws it high up to let Kel do a slam dunk.");

        for (int i = 0; i < manager.foes.Count; i++)
        {
            BattleCharacter target = manager.foes[i];
            int critical = RollCritical(hero.currLuck);
            int damage = (int)(critical * IsEffective(target) * (user.currAttack + hero.currAttack - target.currDefense));
            target.TakeDamage(damage);
        }
    }
}
