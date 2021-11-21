using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AubreySkills : Skills
{
    //Skill 1: Positive Spirit: If the targetted foe is Sad or Depressed, lower their defense.Then deal damage to them.
    //Skill 2: Home Run: If Aubrey is Happy or Ecstatic, raise their accuracy. Then deal damage to a foe.
    //Skill 3: Cheer: Makes anyone Happy. If they're already Happy, they become Ecstatic.
    //Skill 4: Sacrifice: Deal a lot of damage to a Foe. Aubrey becomes toats.

    //Follow Up 1: Look at Omori: Deal a lot of damage to a foe.
    //Follow Up 2: Look at Kel: Kel and Aubrey become Angry. Raise their attacks.
    //Follow Up 3: Look at Hero: Aubrey restores 50% of her health, increases her defense, and becomes Happy.

    public override void SetStartingStats()
    {
        //Attack:
        skillNames.Add("Attack");
        juiceCost.Add(0);
        skillTargets.Add(Target.FOE);

        //Skill 1:
        skillNames.Add("Positive Spirit");
        juiceCost.Add(15);
        skillTargets.Add(Target.FOE);

        //Skill 2:
        skillNames.Add("Home Run");
        juiceCost.Add(15);
        skillTargets.Add(Target.FOE);

        //Skill 3:
        skillNames.Add("Cheer");
        juiceCost.Add(5);
        skillTargets.Add(Target.ANYONE);

        //Skill 4:
        skillNames.Add("Sacrifice");
        juiceCost.Add(25);
        skillTargets.Add(Target.FOE);

        //Follow Up 1:
        skillNames.Add("Look at Omori");
        energyCost.Add(3);
        followUpRequire.Add(GameObject.Find("Omori").GetComponent<BattleCharacter>());

        //Follow Up 2:
        skillNames.Add("Look at Kel");
        energyCost.Add(3);
        followUpRequire.Add(GameObject.Find("Kel").GetComponent<BattleCharacter>());

        //Follow Up 3:
        skillNames.Add("Look at Hero");
        energyCost.Add(3);
        followUpRequire.Add(GameObject.Find("Hero").GetComponent<BattleCharacter>());

        user.friend = true;
        user.order = 1;

        user.startingHealth = 69;
        user.startingJuice = 25;
        user.startingAttack = 16;
        user.startingDefense = 6;
        user.startingSpeed = 8;
        user.startingLuck = 0.03f;
        user.startingAccuracy = 1;
    }

    public override void UseSkillOne(BattleCharacter target)
    {
        user.currJuice -= juiceCost[1];
        manager.AddText("Aubrey shows off her positive spirit.");
        if (target.currEmote == BattleCharacter.Emotion.SAD || target.currEmote == BattleCharacter.Emotion.DEPRESSED)
        {
            target.defenseStat -= 0.15f;
            target.ResetStats();
            manager.AddText(target.name + "'s defense decreases.");
        }
        if (RollAccuracy(user.currAccuracy))
        {
            int critical = RollCritical(user.currLuck);
            int damage = (int)(critical * IsEffective(target) * (2 * user.currAttack - target.currDefense));
            target.TakeDamage(damage);
        }
    }
    public override void UseSkillTwo(BattleCharacter target)
    {
        user.currJuice -= juiceCost[2];
        manager.AddText("Aubrey hits a home run.");
        if (user.currEmote == BattleCharacter.Emotion.HAPPY || target.currEmote == BattleCharacter.Emotion.ECSTATIC)
        {
            user.accuracyStat += 0.15f;
            user.ResetStats();
            manager.AddText(user.name + "'s accuracy increases.");
        }
        if (RollAccuracy(user.currAccuracy))
        {
            int critical = RollCritical(user.currLuck);
            int damage = (int)(critical * IsEffective(target) * (2 * user.currAttack - target.currDefense));
            target.TakeDamage(damage);
        }
    }
    public override void UseSkillThree(BattleCharacter target)
    {
        user.currJuice -= juiceCost[3];
        manager.AddText("Aubrey encourages " + target.name + ".");
        target.NewEmotion(BattleCharacter.Emotion.HAPPY);
    }
    public override void UseSkillFour(BattleCharacter target)
    {
        user.currJuice -= juiceCost[4];
        manager.AddText("Aubrey gives it everything she's got.");
        if (RollAccuracy(user.currAccuracy))
        {
            int critical = RollCritical(user.currLuck);
            int damage = (int)(critical * IsEffective(target) * (4 * user.currHealth - target.currDefense));
            target.TakeDamage(damage);
            user.TakeDamage(100);
        }
    }
    public override void FollowUpOne()
    {
        manager.energy -= energyCost[0];
        BattleCharacter target = manager.foes[Random.Range(0, manager.foes.Count - 1)];
        manager.AddText("Omori didn't notice Aubrey, so she attacks harder.");

        int critical = RollCritical(user.currLuck);
        int damage = (int)(critical * IsEffective(target) * (3 * user.currAttack));
        target.TakeDamage(damage);
    }
    public override void FollowUpTwo()
    {
        manager.energy -= energyCost[1];
        BattleCharacter kel = followUpRequire[1];
        manager.AddText("Kel eggs on Aubrey.");

        user.attackStat += 0.15f;
        manager.AddText(user.name + "'s attack increases.");
        user.NewEmotion(BattleCharacter.Emotion.ANGRY);

        kel.attackStat += 0.15f;
        manager.AddText(kel.name + "'s attack increases.");
        kel.NewEmotion(BattleCharacter.Emotion.ANGRY);
    }
    public override void FollowUpThree()
    {
        manager.energy -= energyCost[2];
        manager.AddText("Hero cheers on Aubrey.");
        user.defenseStat += 0.15f;
        manager.AddText(user.name + "'s defense increases.");

        int healing = (int) (user.startingHealth * 0.5f);
        user.TakeHealing(healing, 0);
        user.NewEmotion(BattleCharacter.Emotion.HAPPY);
    }
}
