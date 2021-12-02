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
        user.startingHealth = 400;
        user.startingAttack = 50;
        user.startingDefense = 10;
        user.startingSpeed = 10;
        user.startingLuck = 0.0f;
        user.startingAccuracy = 0.8f;
    }

    public override IEnumerator UseSkillOne(BattleCharacter target)
    {
        manager.AddText("Bear lets out a huge roar.", true);
        user.attackStat += 0.15f;
        user.defenseStat += 0.15f;
        user.speedStat += 0.15f;
        user.luckStat += 0.15f;
        user.accuracyStat += 0.15f;
        yield return user.NewEmotion(BattleCharacter.Emotion.ANGRY);
        manager.AddText("All of Bear's stats increase.");
    }

}
