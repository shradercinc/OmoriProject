using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OmoriSkills : Skills
{
    //Skill 1: Exploit: If the targetted Foe is Happy or Ecstatic, lower their speed. Then deal damage to them.
    //Skill 2: Stab: If Omori is Sad or Depressed, raise their attack. Then deal damage to a foe.
    //Skill 3: Sad Poem: Makes anyone Sad. If they're already Sad, they become Depressed.
    //Skill 4: Stare: Reduce the stats of a foe.

    public override void SetStartingStats()
    {
        juiceCost.Add(0);
        juiceCost.Add(15);
        juiceCost.Add(15);
        juiceCost.Add(5);
        juiceCost.Add(25);

        skillTargets.Add(Target.FOE);
        skillTargets.Add(Target.FOE);
        skillTargets.Add(Target.FOE);
        skillTargets.Add(Target.ANYONE);
        skillTargets.Add(Target.FOE);

        user.friend = true;
        user.startingHealth = 52;
        user.startingJuice = 35;
        user.startingAttack = 13;
        user.startingDefense = 8;
        user.startingSpeed = 12;
        user.startingLuck = 5;
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
    }
    public override void UseSkillOne(BattleCharacter target)
    {
        target = RedirectTarget(target, 1);

        if (target.currEmote == BattleCharacter.Emotion.HAPPY || target.currEmote == BattleCharacter.Emotion.ECSTATIC)
        {
            target.speedStat -= 0.15f;
            target.ResetStats();
        }
        if (RollDice(user.currAccuracy))
        {
            int critical = RollDice(user.currLuck) ? 2 : 1;
            int damage = (int)(critical * IsEffective(target) * (2 * user.currAttack - target.currDefense));
            target.TakeDamage(-damage);
        }
    }
    public override void UseSkillTwo(BattleCharacter target)
    {
        target = RedirectTarget(target, 2);

        if (user.currEmote == BattleCharacter.Emotion.SAD || target.currEmote == BattleCharacter.Emotion.DEPRESSED)
        {
            user.attackStat += 0.15f;
            user.ResetStats();
        }
        if (RollDice(user.currAccuracy))
        {
            int critical = RollDice(user.currLuck) ? 2 : 1;
            int damage = (int)(critical * IsEffective(target) * (2 * user.currAttack - target.currDefense));
            target.TakeDamage(-damage);
        }
    }
    public override void UseSkillThree(BattleCharacter target)
    {
        target = RedirectTarget(target, 3);
        target.NewEmotion(BattleCharacter.Emotion.SAD);
    }
    public override void UseSkillFour(BattleCharacter target)
    {
        target = RedirectTarget(target, 4);

        target.attackStat -= 0.1f;
        target.defenseStat -= 0.1f;
        target.speedStat -= 0.1f;
        target.luckStat -= 0.1f;
        target.accuracyStat -= 0.1f;
        target.ResetStats();
    }
}
