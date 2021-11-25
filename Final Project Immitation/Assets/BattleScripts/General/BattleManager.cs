using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class BattleManager : MonoBehaviour
{
    public List<BattleCharacter> friends = new List<BattleCharacter>();
    public List<BattleCharacter> foes = new List<BattleCharacter>();
    public List<BattleCharacter> toast = new List<BattleCharacter>();
    List<BattleCharacter> SpeedQueue = new List<BattleCharacter>();

    public TMP_Text battleLog;
    public TMP_Text energyLog;

    public int energy = 3;
    bool battleContinue = true;

    void Awake()
    {
        StartCoroutine(NewRound());
    }

    public void AddEnergy()
    {
        energy++;
        if (energy > 10)
            energy = 10;
    }

    public void AddText(string x, bool reset)
    {
        if (reset)
            battleLog.text = x;
        else
        {
            battleLog.text += "\n";
            battleLog.text += x;
        }
    }

    public void AddText(string x)
    {
        AddText(x, false);
    }

    IEnumerator NewRound()
    {
        friends = friends.OrderBy(o => o.order).ToList();
        energyLog.text = "Energy: " + energy;

        for (int i = 0; i < friends.Count; i++)
        {
            SpeedQueue.Add(friends[i]);
            yield return friends[i].ChooseSkill();
        }
        for (int i = 0; i < foes.Count; i++)
        {
            SpeedQueue.Add(foes[i]);
            foes[i].ChooseRandomSkill();
        }
        StartCoroutine(PlayRound());
    }

    IEnumerator PlayRound()
    {
        while (battleContinue && SpeedQueue.Count > 0)
        {
            SpeedQueue = SpeedQueue.OrderByDescending(o => o.currSpeed).ToList();
            BattleCharacter nextInLine = SpeedQueue[0];
            SpeedQueue.Remove(nextInLine);

            if (!nextInLine.toast)
            {
                if (nextInLine.weapon != null)
                    yield return nextInLine.weapon.StartOfTurn();
                yield return nextInLine.UseMove();

                if (energy >= 3 && nextInLine.friend && nextInLine.currMove == BattleCharacter.Move.ATTACK)
                    yield return FollowUp(nextInLine);
                else
                    yield return new WaitForSeconds(1.5f);
            }

            energyLog.text = "Energy: " + energy;
            battleContinue = (friends.Count > 0 && foes.Count > 0);
        }

        if (battleContinue)
            StartCoroutine(NewRound());
        else
        {
            StopAllCoroutines();
            AddText("The battle is over.", true);
        }
    }

    IEnumerator FollowUp(BattleCharacter user)
    {
        yield return new WaitForSeconds(1.5f);
        bool decision = true;
        Skills userSkills = user.userSkills;

        AddText("Should " + user.name + " use a follow up?", true);
        AddText("1: Skip");

        bool skillOne = (energy >= userSkills.energyCost[0] && !userSkills.followUpRequire[0].toast);
        if (skillOne)
            AddText("2: " + userSkills.skillNames[5] + " - " + userSkills.energyCost[0] + " energy");
        else
            AddText("Cannot " + userSkills.skillNames[5]);

        bool skillTwo = (energy >= userSkills.energyCost[1] && !userSkills.followUpRequire[1].toast);
        if (skillTwo)
            AddText("3: " + userSkills.skillNames[6] + " - " + userSkills.energyCost[1] + " energy");
        else
            AddText("Cannot " + userSkills.skillNames[6]);

        bool skillThree = (energy >= userSkills.energyCost[2] &&
        (friends.Count == 4 || !userSkills.followUpRequire[2].toast));
        if (skillThree)
            AddText("4: " + userSkills.skillNames[7] + " - " + userSkills.energyCost[0] + " energy");
        else
            AddText("Cannot " + userSkills.skillNames[7]);

        while (decision)
        {
            if (Input.GetKeyDown(KeyCode.Alpha2) && skillOne)
            {
                yield return user.userSkills.FollowUpOne();
                decision = false;
                yield return new WaitForSeconds(1.5f);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) && skillTwo)
            {
                yield return user.userSkills.FollowUpTwo();
                decision = false;
                yield return new WaitForSeconds(1.5f);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4) && skillThree)
            {
                yield return user.userSkills.FollowUpThree();
                decision = false;
                yield return new WaitForSeconds(1.5f);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                decision = false;
            }
            yield return null;
        }
    }

    public void ReturnToList(BattleCharacter target)
    {
        if (toast.Contains(target))
            toast.Remove(target);
        if (target.friend)
            friends.Add(target);
        else
            foes.Add(target);
    }

    public void RemoveFromList(BattleCharacter target)
    {
        if (target.friend)
        {
            friends.Remove(target);
            toast.Add(target);
        }
        else
        {
            foes.Remove(target);
        }
    }

    public List<BattleCharacter> GetAllTargets()
    {
        List<BattleCharacter> allTargets = new List<BattleCharacter>();

        for (int i = 0; i < friends.Count; i++)
            allTargets.Add(friends[i]);
        for (int i = 0; i < foes.Count; i++)
            allTargets.Add(foes[i]);

        return allTargets;
    }
}