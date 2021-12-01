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
        manager.AddText("Bear lets out a huge roar.", true);
        target.attackStat += 0.1f;
        target.defenseStat += 0.1f;
        target.speedStat += 0.1f;
        target.luckStat += 0.1f;
        target.accuracyStat += 0.1f;
        yield return user.NewEmotion(BattleCharacter.Emotion.ANGRY);
        manager.AddText("All of Bear's stats increase.");
    }

}
