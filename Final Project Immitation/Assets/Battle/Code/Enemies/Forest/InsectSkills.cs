using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsectSkills : Skills
{
    //Skill 1: Infest: Create another Insect.
    //Skill 2: Sting: Deal damage to a Friend and paralyze them.
    //Skill 3: Steal Juice: Drain 25% of a Friend's Juice. They become Angry.

    public BattleCharacter insect;

    public override void SetStartingStats()
    {
        //Attack:
        skillTargets.Add(Target.FRIEND);
        //Skill 1:
        skillTargets.Add(Target.FRIEND);
        //Skill 2:
        skillTargets.Add(Target.FRIEND);
        //Skill 3:
        skillTargets.Add(Target.FRIEND);

        user = gameObject.GetComponent<BattleCharacter>();
        user.friend = false;
        user.startingHealth = 80;
        user.startingAttack = 25;
        user.startingDefense = 15;
        user.startingSpeed = 20;
        user.startingLuck = 0;
        user.startingAccuracy = 1;
    }

    public override IEnumerator UseSkillOne(BattleCharacter target)
    {
        if (manager.foes.Count >= 5)
        {
            yield return BasicAttack(target);
        }
        else
        {
            manager.AddText("The Insect buzzing attracts another Insect.", true);
            yield return new WaitForSeconds(0.5f);
            manager.CreateFoe(insect, "Insect");
        }
    }

    public override IEnumerator UseSkillTwo(BattleCharacter target)
    {
        target = RedirectTarget(target, 3);
        manager.AddText("Insect stings " + target.name + ".", true);

        if (RollAccuracy(user.currAccuracy))
        {
            int critical = RollCritical(user.currLuck);
            int damage = (int)(critical * IsEffective(target) * (user.currAttack - target.currDefense));
            yield return target.TakeDamage(damage);

            target.paralyze = true;
            manager.AddText(target.name + " gets paralyzed.");
        }
    }
    public override IEnumerator UseSkillThree(BattleCharacter target)
    {
        target = RedirectTarget(target, 3);
        manager.AddText("Insect steals some juice from " + target.name + ".", true);

        int juice = target.currJuice / 4;
        yield return target.DrainJuice(juice);
        manager.AddText(target.name + $" loses {juice} Juice.");
        yield return target.NewEmotion(BattleCharacter.Emotion.ANGRY);
    }
}
