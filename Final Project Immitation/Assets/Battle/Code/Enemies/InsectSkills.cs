using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsectSkills : Skills
{
    //Skill 1: Infest: Create another Insect.

    public BattleCharacter insect;

    public override void SetStartingStats()
    {
        //Attack:
        skillTargets.Add(Target.FRIEND);
        //Skill 1:
        skillTargets.Add(Target.FRIEND);

        user = gameObject.GetComponent<BattleCharacter>();
        user.friend = false;
        user.startingHealth = 80;
        user.startingAttack = 20;
        user.startingDefense = 20;
        user.startingSpeed = 20;
        user.startingLuck = 0;
        user.startingAccuracy = 1;
    }

    public override IEnumerator UseSkillOne(BattleCharacter target)
    {
        if (manager.foes.Count >= 5)
        {
            yield return BasicAttack(target);
        }
        else
        {
            manager.AddText("The Insect buzzing attracts another Insect.");
            yield return new WaitForSeconds(1);
            manager.CreateFoe(insect, "Insect");
        }
    }
}
