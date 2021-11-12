using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OmoriSkills : Skills
{
    public override void SetStartingStats()
    {
        juiceCost.Add(5);
        juiceCost.Add(10);
        juiceCost.Add(15);
        juiceCost.Add(20);

        skillTargets.Add(Target.NONE);
        skillTargets.Add(Target.FRIEND);
        skillTargets.Add(Target.FOE);
        skillTargets.Add(Target.ANYONE);

        user.friend = true;
        user.startingHealth = 52;
        user.startingJuice = 35;
        user.startingAttack = 13;
        user.startingDefense = 8;
        user.startingSpeed = 12;
        user.startingLuck = 5;
    }

    public override void UseSkillOne()
    {
    }
    public override void UseSkillTwo()
    {
    }
    public override void UseSkillThree()
    {
    }
    public override void UseSkillFour()
    {
    }
    public override void UseSkillOne(BattleCharacter target)
    {
    }
    public override void UseSkillTwo(BattleCharacter target)
    {
    }
    public override void UseSkillThree(BattleCharacter target)
    {
    }
    public override void UseSkillFour(BattleCharacter target)
    {
    }
}
