using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KelSkills : Skills
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
        skillTargets.Add(Target.ANY);

        user.friend = true;
        user.startingHealth = 48;
        user.startingJuice = 41;
        user.startingAttack = 13;
        user.startingDefense = 6;
        user.startingSpeed = 15;
        user.startingLuck = 7;
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
