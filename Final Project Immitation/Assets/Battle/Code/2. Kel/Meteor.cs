using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : Weapon
{
    public override void AffectUser()
    {
        user = FindObjectOfType<KelSkills>().GetComponent<BattleCharacter>();
        description = "Kel starts with less Health and more Attack. When Kel becomes Toast, deal damage to all Foes.";
        user.startingHealth -= 10;
        user.attackStat += 0.4f;
    }

    public override IEnumerator OnToast()
    {
        List<BattleCharacter> allTargets = manager.foes;
        Skills userSkills = user.GetComponent<Skills>();

        for (int i = 0; i < allTargets.Count; i++)
        {
            manager.AddText("Kel crashes the Meteor into the Earth.", true);
            BattleCharacter target = allTargets[i];

            int critical = userSkills.RollCritical(user.currLuck);
            int damage = (int)(critical * userSkills.IsEffective(target) * (2 * user.currAttack - target.currDefense));
            yield return target.TakeDamage(damage);
            yield return new WaitForSeconds(1);
        }

    }
}
