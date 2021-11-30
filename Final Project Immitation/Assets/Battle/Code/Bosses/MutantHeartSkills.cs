using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantHeartSkills : Skills
{
    public BattleCharacter nextTarget;

    //Each turn, pick a friend, and they must change their emotion to match Mutantheart, or they die.

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

    //targets a random friend who doesn't already have the required emotion
    public override BattleCharacter ChooseTarget(int n)
    {
        switch (n)
        {
            case 1:
                return Shuffle(BattleCharacter.Emotion.HAPPY);
            case 2:
                return Shuffle(BattleCharacter.Emotion.SAD);
            case 3:
                return Shuffle(BattleCharacter.Emotion.ANGRY);
            default:
                return Shuffle(BattleCharacter.Emotion.NEUTRAL);
        }
    }

    BattleCharacter Shuffle(BattleCharacter.Emotion target)
    {
        List<BattleCharacter> friends = manager.friends;
        for (int i = 0; i < friends.Count - 1; i++)
        {
            int rnd = Random.Range(i, friends.Count);
            BattleCharacter temp = friends[rnd];
            friends[rnd] = friends[i];
            friends[i] = temp;
        }
        for (int i = 0; i<friends.Count; i++)
        {
            if (friends[i].currEmote != target)
                return friends[i];
        }
        return friends[Random.Range(0, friends.Count)];
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
        yield return new WaitForSeconds(0.5f);
    }
    public override IEnumerator UseSkillTwo(BattleCharacter target)
    {
        nextTarget = RedirectTarget(target, 2);
        manager.AddText("Mutantheart wants " + nextTarget.name + " to act like her.", true);
        yield return user.NewEmotion(BattleCharacter.Emotion.SAD);
        yield return new WaitForSeconds(0.5f);
    }
    public override IEnumerator UseSkillThree(BattleCharacter target)
    {
        nextTarget = RedirectTarget(target, 3);
        manager.AddText("Mutantheart wants " + nextTarget.name + " to act like her.", true);
        yield return user.NewEmotion(BattleCharacter.Emotion.ANGRY);
        yield return new WaitForSeconds(0.5f);
    }
    public override IEnumerator UseSkillFour(BattleCharacter target)
    {
        nextTarget = RedirectTarget(target, 4);
        manager.AddText("Mutantheart wants " + nextTarget.name + " to act like her.", true);
        yield return user.NewEmotion(BattleCharacter.Emotion.NEUTRAL);
        yield return new WaitForSeconds(0.5f);
    }
}
