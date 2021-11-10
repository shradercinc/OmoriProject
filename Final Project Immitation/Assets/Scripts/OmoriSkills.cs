using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OmoriSkills : Skills
{
    public int[] juiceCost;

    private void Start()
    {
        juiceCost[0] = 5;
        juiceCost[1] = 5;
        juiceCost[2] = 5;
        juiceCost[3] = 5;
    }

    void UseSkillOne(BattleCharacter user, BattleCharacter target)
    {
    }
    void UseSkillTwo(BattleCharacter user, BattleCharacter target)
    {
    }
    void UseSkillThree(BattleCharacter user, BattleCharacter target)
    {

    }
    void UseSkillFour(BattleCharacter user, BattleCharacter target)
    {
    }
}
