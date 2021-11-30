using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantHeartSkills : Skills
{
    public BattleCharacter nextTarget;

    //Each turn, pick a friend, and they must change their emotion.

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
        skillTargets.Add(Target.FRIEND);

        user = gameObject.GetComponent<BattleCharacter>();
        user.friend = false;
        user.startingHealth = 400;
        user.startingAttack = 10;
        user.startingDefense = 3;
        user.startingSpeed = -500;
        user.startingLuck = 0.15f;
        user.startingAccuracy = 1;
    }

    public override IEnumerator BasicAttack(BattleCharacter target)
    {
        yield return UseSkillFour(target);
    }

    public override IEnumerator UseSkillOne(BattleCharacter target)
    {
        nextTarget = RedirectTarget(target, 1);
        manager.AddText("Mutantheart wants " + nextTarget.name + " to act like her.", true);
        yield return user.NewEmotion(BattleCharacter.Emotion.HAPPY);
    }
    public override IEnumerator UseSkillTwo(BattleCharacter target)
    {
        nextTarget = RedirectTarget(target, 2);
        manager.AddText("Mutantheart wants " + nextTarget.name + " to act like her.", true);
        yield return user.NewEmotion(BattleCharacter.Emotion.SAD);
    }
    public override IEnumerator UseSkillThree(BattleCharacter target)
    {
        nextTarget = RedirectTarget(target, 3);
        manager.AddText("Mutantheart wants " + nextTarget.name + " to act like her.", true);
        yield return user.NewEmotion(BattleCharacter.Emotion.ANGRY);
    }
    public override IEnumerator UseSkillFour(BattleCharacter target)
    {
        nextTarget = RedirectTarget(target, 4);
        manager.AddText("Mutantheart wants " + nextTarget.name + " to act like her.", true);
        yield return user.NewEmotion(BattleCharacter.Emotion.NEUTRAL);
    }
}
