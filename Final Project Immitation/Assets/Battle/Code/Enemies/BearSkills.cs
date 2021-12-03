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

        user = gameObject.GetComponent<BattleCharacter>();
        user.friend = false;
        user.startingHealth = 350;
        user.startingAttack = 50;
        user.startingDefense = 10;
        user.startingSpeed = 10;
        user.startingLuck = 0.05f;
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
    public override IEnumerator UseSkillTwo(BattleCharacter target)
    {
        manager.AddText("Bear eats some berries.", true);
        yield return user.TakeHealing(user.startingHealth / 5, 0);
        yield return user.NewEmotion(BattleCharacter.Emotion.HAPPY);
    }
}
