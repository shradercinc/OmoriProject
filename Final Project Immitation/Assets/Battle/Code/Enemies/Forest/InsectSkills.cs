using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InsectSkills : Skills
{
    //Skill 1: Infest: Create another Insect.
    //Skill 2: Sting: Deal damage to a Friend and paralyze them.
    //Skill 3: Steal Juice: Drain 25% of a Friend's Juice. They become Angry.
    //Skill 4: Powder: Reduce all Friends' Defense.

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
        //Skill 4:
        skillTargets.Add(Target.ALLFRIENDS);

        user = gameObject.GetComponent<BattleCharacter>();
        user.friend = false;
        user.startingHealth = 75;
        user.startingAttack = 25;
        user.startingDefense = 15;
        user.startingSpeed = 20;
        user.startingLuck = 0;
        user.startingAccuracy = 1;
    }

    //targets the friend with the most juice
    public override BattleCharacter ChooseTarget(int n)
    {
        List<BattleCharacter> friends = manager.friends;
        friends = friends.OrderByDescending(o => o.currJuice).ToList();
        return friends[0];
    }

    public override IEnumerator UseSkillOne(BattleCharacter target)
    {
        if (manager.foes.Count >= 5)
        {
            yield return BasicAttack(target);
        }
        else
        {
            manager.AddText("The buzzing of the Wasp attracts another Wasp.", true);
            yield return new WaitForSeconds(0.5f);
            manager.CreateFoe(insect, "Wasp");
        }
    }

    public override IEnumerator UseSkillTwo(BattleCharacter target)
    {
        target = RedirectTarget(target, 3);
        manager.AddText("Wasp stings " + target.name + ".", true);

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
        manager.AddText("Wasp steals some juice from " + target.name + ".", true);

        int juice = target.currJuice / 4;
        yield return target.DrainJuice(juice);
        manager.AddText(target.name + $" loses {juice} Juice.");
        yield return target.NewEmotion(BattleCharacter.Emotion.ANGRY);
    }
    public override IEnumerator UseSkillFour(BattleCharacter target)
    {
        for (int i = 0; i<manager.friends.Count; i++)
        {
            manager.AddText("Wasp shoots out poison powder.", true);
            target = manager.friends[i];
            target.defenseStat -= 0.15f;
            manager.AddText(target.name + "'s Defense decreases.");
            yield return new WaitForSeconds(0.5f);
        }
    }
}
