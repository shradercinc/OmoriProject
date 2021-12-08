using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnySkills : Skills
{
    //Skill 1: Cute Bounce: All Friends become Happy.
    //Skill 2: Loneliness: Become Depressed. Raise its Defense.
    //Skill 3: Bounce Around: Deal damage to all Friends.
    //Skill 4: Bite: Deal damage to a Friend. Subtract 2 Energy.

    public override void SetStartingStats()
    {
        //Attack:
        skillTargets.Add(Target.FRIEND);
        //Skill 1:
        skillTargets.Add(Target.ALLFRIENDS);
        //Skill 2:
        skillTargets.Add(Target.NONE);
        //Skill 3:
        skillTargets.Add(Target.ALLFRIENDS);

        user = gameObject.GetComponent<BattleCharacter>();
        user.friend = false;
        user.startingHealth = 100;
        user.startingAttack = 30;
        user.startingDefense = 10;
        user.startingSpeed = 30;
        user.startingLuck = 0.1f;
        user.startingAccuracy = 0.9f;
    }

    public override IEnumerator UseSkillOne(BattleCharacter target)
    {
        List<BattleCharacter> allFriends = manager.friends;
        for (int i = 0; i < allFriends.Count; i++)
        {
            target = allFriends[i];
            manager.AddText("Bunny bounces around in a cute way.", true);
            yield return target.NewEmotion(BattleCharacter.Emotion.HAPPY);
        }
    }
    public override IEnumerator UseSkillTwo(BattleCharacter target)
    {
        manager.AddText("Bunny feels lonely.", true);
        yield return user.NewEmotion(BattleCharacter.Emotion.DEPRESSED);

        user.defenseStat += 0.2f;
        manager.AddText("Bunny's Defense increases.");
        yield return new WaitForSeconds(0.5f);
    }
    public override IEnumerator UseSkillThree(BattleCharacter target)
    {
        List<BattleCharacter> allTargets = manager.friends;
        for (int i = 0; i < allTargets.Count; i++)
        {
            manager.AddText("Bunny bounces into everyone.", true);
            target = allTargets[i];

            if (RollAccuracy(user.currAccuracy))
            {
                int critical = RollCritical(user.currLuck);
                int damage = (int)(critical * IsEffective(target) * (1.5 * user.currAttack - target.currDefense));
                yield return target.TakeDamage(damage);
            }
        }
    }
    public override IEnumerator UseSkillFour(BattleCharacter target)
    {
        target = RedirectTarget(target, 0);
        manager.AddText(user.name + " bites " + target.name + ".", true);

        if (RollAccuracy(user.currAccuracy))
        {
            int critical = RollCritical(user.currLuck);
            int damage = (int)(critical * IsEffective(target) * (user.currAttack - target.currDefense));
            yield return target.TakeDamage(damage);

            manager.AddText("Omori and friends lose 2 Energy.");
            yield return manager.AddEnergy(-2);

        }
    }
}
