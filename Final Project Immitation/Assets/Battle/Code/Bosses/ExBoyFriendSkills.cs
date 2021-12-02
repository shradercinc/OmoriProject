using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExBoyFriendSkills : Skills
{
    //Skill 1: Strange Bottle: All friends get affected by a random emotion.
    //Skill 2: Gun: Deal damage to a friend 3 times.
    //Skill 3: Electric Ray: Paralyze a Friend. (They lose their next turn.)
    //Skill 4: Intimidate: Reduce all friends' stats.

    public override void SetStartingStats()
    {
        //Attack:
        skillTargets.Add(Target.FRIEND);

        //Skill 1:
        skillTargets.Add(Target.ALLFRIENDS);

        //Skill 2:
        skillTargets.Add(Target.FRIEND);

        //Skill 3:
        skillTargets.Add(Target.FRIEND);

        //Skill 4:
        skillTargets.Add(Target.ALLFRIENDS);

        user = gameObject.GetComponent<BattleCharacter>();
        user.friend = false;
        user.startingHealth = 750;
        user.startingAttack = 50;
        user.startingDefense = 25;
        user.startingSpeed = 13;
        user.startingLuck = 0.15f;
        user.startingAccuracy = 1;
    }

    public override IEnumerator UseSkillOne(BattleCharacter target)
    {
        List<BattleCharacter> allFriends = manager.friends;
        manager.AddText("Spaceboy throws a bottle that releases a strange gas.", true);

        for (int i = 0; i < allFriends.Count; i++)
        {
            int k = Random.Range(0, 3);
            target = allFriends[i];

            switch (k)
            {
                case 0:
                    yield return target.NewEmotion(BattleCharacter.Emotion.HAPPY);
                    break;
                case 1:
                    yield return target.NewEmotion(BattleCharacter.Emotion.ANGRY);
                    break;
                case 2:
                    yield return target.NewEmotion(BattleCharacter.Emotion.SAD);
                    break;
            }
            yield return new WaitForSeconds(1);
        }
    }
    public override IEnumerator UseSkillTwo(BattleCharacter target)
    {
        target = RedirectTarget(target, 2);
        manager.AddText("Spaceboy pulls out his gun.", true);

        for (int i = 0; i < 2; i++)
        {
            if (RollAccuracy(user.currAccuracy))
            {
                int critical = RollCritical(user.currLuck);
                int damage = (int)(critical * IsEffective(target) * (1.5f * user.currAttack - target.currDefense));
                yield return target.TakeDamage(damage);
            }
        }
    }
    public override IEnumerator UseSkillThree(BattleCharacter target)
    {
        target = RedirectTarget(target, 3);
        target.paralyze = true;
        manager.AddText("Spaceboy charges an electric shot.", true);

        if (RollAccuracy(user.currAccuracy))
        {
            int critical = RollCritical(user.currLuck);
            int damage = (int)(critical * IsEffective(target) * (user.currAttack - target.currDefense));
            yield return target.TakeDamage(damage);
        }
    }
    public override IEnumerator UseSkillFour(BattleCharacter target)
    {
        List<BattleCharacter> allFriends = manager.friends;

        for (int i = 0; i < allFriends.Count; i++)
        {
            target = allFriends[i];
            manager.AddText("Spaceboy give an intimidating glare.", true);

            target.attackStat -= 0.1f;
            target.defenseStat -= 0.1f;
            target.speedStat -= 0.1f;
            target.luckStat -= 0.1f;
            target.accuracyStat -= 0.1f;

            manager.AddText("All of " + target.name + "'s stats decrease.");
            yield return target.ResetStats();
            yield return new WaitForSeconds(1);
        }
    }
}
