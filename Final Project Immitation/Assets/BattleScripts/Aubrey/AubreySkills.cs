using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AubreySkills : Skills
{
    //Skill 1: Positive Spirit: If the targetted Foe is Angry or Enraged, lower their attack. Then deal damage to them.
    //Skill 2: Home Run: If Aubrey is Happy or Ecstatic, raise their accuracy. Then deal damage to a foe.
    //Skill 3: Pep Talk: Makes anyone Happy. If they're already Happy, they become Ecstatic.
    //Skill 4: Sacrifice: Deal a lot of damage to a foe. Aubrey becomes toats.

    public override void SetStartingStats()
    {
        juiceCost.Add(15);
        juiceCost.Add(15);
        juiceCost.Add(5);
        juiceCost.Add(25);

        skillTargets.Add(Target.FOE);
        skillTargets.Add(Target.FOE);
        skillTargets.Add(Target.ANYONE);
        skillTargets.Add(Target.FOE);

        user.friend = true;
        user.startingHealth = 69;
        user.startingJuice = 25;
        user.startingAttack = 16;
        user.startingDefense = 6;
        user.startingSpeed = 8;
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
    }
    public override void UseSkillOne(BattleCharacter target)
    {
        if (target.currEmote == BattleCharacter.Emotion.ANGRY || target.currEmote == BattleCharacter.Emotion.ENRAGED)
        {
            target.attackStat -= 0.15f;
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
        if (user.currEmote == BattleCharacter.Emotion.HAPPY || target.currEmote == BattleCharacter.Emotion.ECSTATIC)
        {
            user.accuracyStat += 0.15f;
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
        target.NewEmotion(BattleCharacter.Emotion.HAPPY);
    }
    public override void UseSkillFour(BattleCharacter target)
    {
        if (RollDice(user.currAccuracy))
        {
            int critical = RollDice(user.currLuck) ? 2 : 1;
            int damage = (int)(critical * IsEffective(target) * (4 * user.currHealth - target.currDefense));
            target.TakeDamage(-damage);
            user.TakeDamage(-100);
        }
    }
}
