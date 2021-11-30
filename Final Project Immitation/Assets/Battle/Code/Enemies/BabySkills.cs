using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabySkills : Skills
{
    //Skill 1: Vomit: Deal damage to all Friends.

    public override void SetStartingStats()
    {
        //Attack:
        skillTargets.Add(Target.FRIEND);
        //Skill 1:
        skillTargets.Add(Target.ALLFRIENDS);
        //Skill 2:
        skillTargets.Add(Target.ALLFRIENDS);
        //Skill 3:
        skillTargets.Add(Target.ALLFRIENDS);
        //Skill 4:
        skillTargets.Add(Target.ALLFRIENDS);

        user = gameObject.GetComponent<BattleCharacter>();
        user.friend = false;
        user.startingHealth = 50;
        user.startingAttack = 20;
        user.startingDefense = 10;
        user.startingSpeed = 0;
        user.startingLuck = 0.05f;
        user.startingAccuracy = 1;
    }

    public override IEnumerator UseSkillOne(BattleCharacter target)
    {
        List<BattleCharacter> allFriends = manager.friends;

        for (int i = 0; i < allFriends.Count; i++)
        {
            target = allFriends[i];
            manager.AddText("Juice Baby vomits juice everywhere.", true);

            if (RollAccuracy(user.currAccuracy))
            {
                int critical = RollCritical(user.currLuck);
                int damage = (int)(critical * IsEffective(target) * (user.currAttack - target.currDefense));
                yield return target.TakeDamage(damage);
            }
        }
    }
}
