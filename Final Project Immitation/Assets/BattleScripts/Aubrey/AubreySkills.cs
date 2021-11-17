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

        user.friend = true;
        user.startingHealth = 69;
        user.startingJuice = 25;
        user.startingAttack = 16;
        user.startingDefense = 6;
        user.startingSpeed = 8;
        user.startingLuck = 3;
        user.startingAccuracy = 1;
    }

    public override void UseSkillOne(BattleCharacter target)
    {
        user.currJuice -= juiceCost[1];
        if (target.currEmote == BattleCharacter.Emotion.ANGRY || target.currEmote == BattleCharacter.Emotion.ENRAGED)
        {
            target.attackStat -= 0.15f;
            target.ResetStats();
        }
        if (RollDice(user.currAccuracy))
        {
            int critical = RollDice(user.currLuck) ? 2 : 1;
            int damage = (int)(critical * IsEffective(target) * (2 * user.currAttack - target.currDefense));
            target.TakeDamage(damage);
        }
    }
    public override void UseSkillTwo(BattleCharacter target)
    {
        user.currJuice -= juiceCost[2];
        if (user.currEmote == BattleCharacter.Emotion.HAPPY || target.currEmote == BattleCharacter.Emotion.ECSTATIC)
        {
            user.accuracyStat += 0.15f;
            user.ResetStats();
        }
        if (RollDice(user.currAccuracy))
        {
            int critical = RollDice(user.currLuck) ? 2 : 1;
            int damage = (int)(critical * IsEffective(target) * (2 * user.currAttack - target.currDefense));
            target.TakeDamage(damage);
        }
    }
    public override void UseSkillThree(BattleCharacter target)
    {
        user.currJuice -= juiceCost[3];
        target.NewEmotion(BattleCharacter.Emotion.HAPPY);
    }
    public override void UseSkillFour(BattleCharacter target)
    {
        user.currJuice -= juiceCost[4];
        if (RollDice(user.currAccuracy))
        {
            int critical = RollDice(user.currLuck) ? 2 : 1;
            int damage = (int)(critical * IsEffective(target) * (4 * user.currHealth - target.currDefense));
            target.TakeDamage(damage);
            user.TakeDamage(100);
        }
    }
}
