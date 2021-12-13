using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweetheartSkills : Skills
{
    public BattleCharacter nextTarget;

    //Each turn, pick a friend, and they must change their emotion to match Mutantheart, or they die.

    public override void SetStartingStats()
    {
        //Attack:
        skillTargets.Add(Target.FRIEND);

        user = gameObject.GetComponent<BattleCharacter>();
        user.friend = false;
        user.startingHealth = 750;
        user.startingDefense = 25;
        user.startingSpeed = -500;
    }

    public override IEnumerator BasicAttack(BattleCharacter target)
    {
        if (nextTarget != null && !nextTarget.toast && nextTarget.currEmote != user.currEmote)
        {
            manager.AddText(target.name + " has the wrong emotion.", true);
            yield return target.TakeDamage(200);
        }

        int n = Random.Range(0, 3);
        switch (n)
        {
            case 0:
                yield return UseSkillOne(target);
                break;
            case 1:
                yield return UseSkillTwo(target);
                break;
            case 2:
                yield return UseSkillThree(target);
                break;
        }
    }

    public override IEnumerator UseSkillOne(BattleCharacter target)
    {
        nextTarget = RedirectTarget(target, 1);
        manager.AddText("Sweetheart wants " + nextTarget.name + " to act like her.", true);
        yield return user.NewEmotion(BattleCharacter.Emotion.HAPPY);
        yield return new WaitForSeconds(1);
    }
    public override IEnumerator UseSkillTwo(BattleCharacter target)
    {
        nextTarget = RedirectTarget(target, 2);
        manager.AddText("Sweetheart wants " + nextTarget.name + " to act like her.", true);
        yield return user.NewEmotion(BattleCharacter.Emotion.SAD);
        yield return new WaitForSeconds(1);
    }
    public override IEnumerator UseSkillThree(BattleCharacter target)
    {
        nextTarget = RedirectTarget(target, 3);
        manager.AddText("Sweetheart wants " + nextTarget.name + " to act like her.", true);
        yield return user.NewEmotion(BattleCharacter.Emotion.ANGRY);
        yield return new WaitForSeconds(1);
    }
}
