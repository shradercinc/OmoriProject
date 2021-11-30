using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearSkills : Skills
{
    public override void SetStartingStats()
    {
        //Attack:
        skillTargets.Add(Target.FRIEND);
        //Skill 1:
        skillTargets.Add(Target.NONE);
        //Skill 2:
        skillTargets.Add(Target.NONE);
        //Skill 3:
        skillTargets.Add(Target.FRIEND);
        //Skill 4:
        skillTargets.Add(Target.FRIEND);

        user = gameObject.GetComponent<BattleCharacter>();
        user.friend = false;
        user.startingHealth = 200;
        user.startingAttack = 25;
        user.startingDefense = 5;
        user.startingSpeed = 10;
        user.startingLuck = 0.0f;
        user.startingAccuracy = 0.8f;
    }

    public override IEnumerator UseSkillOne(BattleCharacter target)
    {
        manager.AddText("Bear lets out a huge roar.");
        target.attackStat += 0.1f;
        target.defenseStat += 0.1f;
        target.speedStat += 0.1f;
        target.luckStat += 0.1f;
        target.accuracyStat += 0.1f;
        yield return user.NewEmotion(BattleCharacter.Emotion.ANGRY);
        manager.AddText("All of Bear's stats increase.");
    }

    public override IEnumerator UseSkillThree(BattleCharacter target)
    {
        yield return BasicAttack(target);
    }
    public override IEnumerator UseSkillFour(BattleCharacter target)
    {
        yield return BasicAttack(target);
    }
}
