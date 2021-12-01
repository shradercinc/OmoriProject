using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombWeapon : Weapon
{
    //When this becomes Toast, deal damage to everyone else.
    //If at the end of the turn, this is the only Foe left, it turns itself off.

    public override void AffectUser()
    {
        user = FindObjectOfType<BombSkills>().GetComponent<BattleCharacter>();
    }
    public override IEnumerator StartOfTurn()
    {
        if (manager.foes.Count == 1)
        {
            manager.AddText("The Bomb automatically deactivates.", true);
            manager.RemoveFromList(user);
            user.currMove = BattleCharacter.Move.NONE;
            yield return new WaitForSeconds(1.5f);
        }
    }
    public override IEnumerator OnToast()
    {
        List<BattleCharacter> allTargets = manager.GetAllTargets();
        Skills userSkills = user.GetComponent<Skills>();

        for (int i = 0; i < allTargets.Count; i++)
        {
            manager.AddText("The Bomb explodes.", true);
            BattleCharacter target = allTargets[i];

            int critical = userSkills.RollCritical(user.currLuck);
            int damage = (int)(critical * userSkills.IsEffective(target) * (3 * user.currAttack - target.currDefense));
            yield return target.TakeDamage(damage);
            yield return new WaitForSeconds(1);
        }
    }
}
