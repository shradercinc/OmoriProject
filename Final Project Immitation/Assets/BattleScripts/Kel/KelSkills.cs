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
        juiceCost.Add(10);
        skillTargets.Add(Target.FOE);

        user.friend = true;
        user.startingHealth = 48;
        user.startingJuice = 41;
        user.startingAttack = 13;
        user.startingDefense = 6;
        user.startingSpeed = 15;
        user.startingLuck = 7;
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
}
