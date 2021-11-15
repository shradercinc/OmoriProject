using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KelSkills : Skills
{
    //SKILL 1: Snowball Fight: If the targetted foe is Sad or Depressed, lower their defense. Then deal damage to them.
    //SKILL 2: Headbutt: If Kel is Angry or Enraged, increase their luck. Then deal damage to a foe.
    //SKILL 3: Annoy: Makes anyone Angry. If they're already Angry, they become Enraged.
    //SKILL 4: Run N' Gun; Deal damage to a foe, based on your Speed instead of your Attack.

    public override void SetStartingStats()
    {
        juiceCost.Add(15);
        juiceCost.Add(15);
        juiceCost.Add(5);
        juiceCost.Add(10);

        skillTargets.Add(Target.FOE);
        skillTargets.Add(Target.FOE);
        skillTargets.Add(Target.ANYONE);
        skillTargets.Add(Target.FOE);

        user.friend = true;
        user.startingHealth = 48;
        user.startingJuice = 41;
        user.startingAttack = 13;
        user.startingDefense = 6;
        user.startingSpeed = 15;
        user.startingLuck = 7;
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

        if (target.currEmote == BattleCharacter.Emotion.SAD || target.currEmote == BattleCharacter.Emotion.DEPRESSED)
        {
            target.defenseStat -= 0.15f;
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

        if (user.currEmote == BattleCharacter.Emotion.ANGRY || target.currEmote == BattleCharacter.Emotion.ENRAGED)
        {
            user.luckStat += 0.15f;
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
        target.NewEmotion(BattleCharacter.Emotion.ANGRY);
    }
    public override void UseSkillFour(BattleCharacter target)
    {
        target = RedirectTarget(target, 4);

        if (RollDice(user.currAccuracy))
        {
            int critical = RollDice(user.currLuck) ? 2 : 1;
            int damage = (int)(critical * IsEffective(target) * (1.5*user.currSpeed - target.currDefense));
            target.TakeDamage(-damage);
        }
    }
}
