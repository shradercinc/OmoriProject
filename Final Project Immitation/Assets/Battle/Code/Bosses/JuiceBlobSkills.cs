using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class JuiceBlobSkills : Skills
{
    public BattleCharacter bomb;
    public BattleCharacter juiceBaby;

    //Skill 1: Vomit: Deal damage to all Friends.
    //Skill 2: Assemble Bomb: Create a Bomb.
    //Skill 3: Baby: If there's a Juice Baby, kill it to gain health. Otherwise, create a Juice Baby.
    //Skill 4: Hunger: Become Angry and raise its attack.

    public override void SetStartingStats()
    {
        //Attack:
        skillTargets.Add(Target.FRIEND);
        //Skill 1:
        skillTargets.Add(Target.ALLFRIENDS);
        //Skill 2:
        skillTargets.Add(Target.NONE);
        //Skill 3:
        skillTargets.Add(Target.NONE);
        //Skill 4:
        skillTargets.Add(Target.ALLFRIENDS);

        user = gameObject.GetComponent<BattleCharacter>();
        user.friend = false;
        user.startingHealth = 750;
        user.startingAttack = 50;
        user.startingDefense = 25;
        user.startingSpeed = 100;
        user.startingLuck = 0.1f;
        user.startingAccuracy = 1;
    }

    //targets the friend with the most health
    public override BattleCharacter ChooseTarget(int n)
    {
        List<BattleCharacter> friends = manager.friends;
        friends = friends.OrderBy(o => o.currHealth).ToList();
        return friends[0];
    }

    public override IEnumerator UseSkillOne(BattleCharacter target)
    {
        List<BattleCharacter> allFriends = manager.friends;

        for (int i = 0; i < allFriends.Count; i++)
        {
            target = allFriends[i];
            manager.AddText("Juice Blob vomits juice everywhere.", true);

            if (RollAccuracy(user.currAccuracy))
            {
                int critical = RollCritical(user.currLuck);
                int damage = (int)(critical * IsEffective(target) * (1.5 * user.currAttack - target.currDefense));
                yield return target.TakeDamage(damage);
            }
        }
    }
    public override IEnumerator UseSkillTwo(BattleCharacter target)
    {
        if (manager.foes.Count == 5)
            yield return UseSkillOne(target);
        else
        {
            manager.AddText("Juice Blob assembles some Bombs.", true);
            yield return new WaitForSeconds(1);
            manager.CreateFoe(bomb, "Bomb");
            manager.CreateFoe(bomb, "Bomb");
        }
    }

    public override IEnumerator UseSkillThree(BattleCharacter target)
    {
        BattleCharacter babyExists = null;

        for (int i = 0; i < manager.foes.Count; i++)
        {
            if (manager.foes[i].gameObject.GetComponent<BabySkills>() != null)
            {
                babyExists = manager.foes[i];
                break;
            }
        }

        if (babyExists != null)
        {
            manager.AddText("Juice Blob swallows up its juice.", true);
            yield return babyExists.TakeDamage(200);
            yield return user.TakeHealing(100, 0);
            yield return user.NewEmotion(BattleCharacter.Emotion.HAPPY);
        }
        else
        {
            manager.AddText("Juice Blob spits out some juice that becomes sentient.", true);
            yield return new WaitForSeconds(1);
            manager.CreateFoe(juiceBaby, "Juice Baby");
            manager.CreateFoe(juiceBaby, "Juice Baby");
        }
    }

    public override IEnumerator UseSkillFour(BattleCharacter target)
    {
        manager.AddText("Juice Blob is hungry for more juice.", true);
        user.attackStat += 0.2f;
        yield return user.ResetStats();
        manager.AddText("Juice Blob's Attack increases.");
        yield return user.NewEmotion(BattleCharacter.Emotion.ANGRY);
    }
}
