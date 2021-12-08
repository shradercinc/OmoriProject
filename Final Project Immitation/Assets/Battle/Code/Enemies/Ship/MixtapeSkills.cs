using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MixtapeSkills : Skills
{
    //Skill 1: Entangle: Reduce a Friend's speed. Then deal a small amount of damage to them.
    //Skill 2: Sad Tune: Deal damage to a Friend. They become Sad.
    //Skill 3: Up to 11: Deal damage to all Friends.
    //Skill 4: Energetic Tune: Heal a Foe. They become Happy.

    public override void SetStartingStats()
    {
        //Attack:
        skillTargets.Add(Target.FRIEND);
        //Skill 1:
        skillTargets.Add(Target.FRIEND);
        //Skill 2:
        skillTargets.Add(Target.FRIEND);
        //Skill 3:
        skillTargets.Add(Target.ALLFRIENDS);
        //Skill 4:
        skillTargets.Add(Target.FOE);

        user = gameObject.GetComponent<BattleCharacter>();
        user.friend = false;
        user.startingHealth = 100;
        user.startingAttack = 30;
        user.startingDefense = 10;
        user.startingSpeed = 15;
        user.startingLuck = 0.1f;
        user.startingAccuracy = 0.9f;
    }

    //targets the fastest friend
    public override BattleCharacter ChooseTarget(int n)
    {
        List<BattleCharacter> friends = manager.friends;
        friends = friends.OrderByDescending(o => o.currSpeed).ToList();
        return friends[0];
    }

    public override IEnumerator UseSkillOne(BattleCharacter target)
    {
        target = RedirectTarget(target, 1);
        manager.AddText("Mixtape entangles " + target.name + " in wires.", true);

        target.speedStat -= 0.15f;
        yield return target.ResetStats();
        manager.AddText(target.name + "'s Speed decreases.");

        if (RollAccuracy(user.currAccuracy))
        {
            int critical = RollCritical(user.currLuck);
            int damage = (int)(critical * IsEffective(target) * (0.5f * user.currAttack - target.currDefense));
            yield return target.TakeDamage(damage);
        }
    }
    public override IEnumerator UseSkillTwo(BattleCharacter target)
    {
        target = RedirectTarget(target, 2);
        manager.AddText("Mixtape plays a sad tune.", true);
        yield return target.NewEmotion(BattleCharacter.Emotion.SAD);
        
        if (RollAccuracy(user.currAccuracy))
        {
            int critical = RollCritical(user.currLuck);
            int damage = (int)(critical * IsEffective(target) * (user.currAttack - target.currDefense));
            yield return target.TakeDamage(damage);
        }
    }
    public override IEnumerator UseSkillThree(BattleCharacter target)
    {
        List<BattleCharacter> allFriends = manager.friends;
        for (int i = 0; i < allFriends.Count; i++)
        {
            target = allFriends[i];
            manager.AddText("Mixtape turns its volume up to eleven.", true);

            if (RollAccuracy(user.currAccuracy))
            {
                int critical = RollCritical(user.currLuck);
                int damage = (int)(critical * IsEffective(target) * (user.currAttack - target.currDefense));
                yield return target.TakeDamage(damage);
            }
        }
    }
    public override IEnumerator UseSkillFour(BattleCharacter target)
    {
        target = RedirectTarget(target, 4);
        manager.AddText("Mixtape plays an energetic tune.", true);
        yield return target.NewEmotion(BattleCharacter.Emotion.HAPPY);
        yield return target.TakeHealing(75, 0);
    }
}