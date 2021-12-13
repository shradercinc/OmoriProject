using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatSkills : Skills
{
    //Skill 1: Invisibility Potion: Become Invisible. (It takes no damage until its next turn.)
    //Skill 2: Failed Invention: Deal damage to all Friends. Become Sad.

    public override void SetStartingStats()
    {
        //Attack:
        skillTargets.Add(Target.FRIEND);
        //Skill 1:
        skillTargets.Add(Target.NONE);
        //Skill 2:
        skillTargets.Add(Target.ALLFRIENDS);

        user = gameObject.GetComponent<BattleCharacter>();
        user.friend = false;
        user.startingHealth = 300;
        user.startingAttack = 30;
        user.startingDefense = 10;
        user.startingSpeed = 5;
        user.startingLuck = 0.05f;
        user.startingAccuracy = 1;
    }

    public override IEnumerator UseSkillOne(BattleCharacter target)
    {
        manager.AddText("Lab Rat drinks a potion of invisibility.", true);
        user.invisible = true;
        yield return new WaitForSeconds(0.5f);
    }
    public override IEnumerator UseSkillTwo(BattleCharacter target)
    {
        manager.AddText("Lab Rat's invention falls apart and explodes.", true);
        yield return user.NewEmotion(BattleCharacter.Emotion.SAD);

        List<BattleCharacter> allTargets = manager.friends;

        for (int i = 0; i < allTargets.Count; i++)
        {
            manager.AddText("Lab Rat's invention falls apart and explodes.", true);
            target = allTargets[i];

            int critical = RollCritical(user.currLuck);
            int damage = (int)(critical * IsEffective(target) * (1.5 * user.currAttack - target.currDefense));
            yield return target.TakeDamage(damage);
        }
    }
}
