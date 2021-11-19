using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class BattleManager : MonoBehaviour
{
    public List<BattleCharacter> friends = new List<BattleCharacter>();
    public List<BattleCharacter> foes = new List<BattleCharacter>();
    public TMP_Text battleLog;
    bool battleContinue = true;

    List<BattleCharacter> SpeedQueue = new List<BattleCharacter>();
    BattleCharacter[] characterArray;

    void Start()
    {
        characterArray = FindObjectsOfType(typeof(BattleCharacter)) as BattleCharacter[];

        for (int i = characterArray.Length-1; i>=0; i--)
        {
            if (characterArray[i].friend)
                friends.Add(characterArray[i]);
            else
                foes.Add(characterArray[i]);
        }

        StartCoroutine(NewRound());
    }

    public void AddText(string x)
    {
        battleLog.text += "\n";
        battleLog.text += x;
    }

    IEnumerator NewRound()
    {
        for (int i = 0; i < friends.Count; i++)
        {
            battleLog.text = "";
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
            battleLog.text = "";
            SpeedQueue = SpeedQueue.OrderByDescending(o => o.currSpeed).ToList();
            BattleCharacter nextInLine = SpeedQueue[0];
            SpeedQueue.Remove(nextInLine);

            if (!nextInLine.toast)
            {
                if (nextInLine.weapon != null)
                    nextInLine.weapon.StartOfTurn();

                nextInLine.UseMove();
                yield return new WaitForSeconds(1.5f);
            }

            battleContinue = (friends.Count > 0 && foes.Count > 0);
        }

        if (battleContinue)
            StartCoroutine(NewRound());
        else
            AddText("The battle is over.");
    }

    public void ReturnToList(BattleCharacter target)
    {
        if (target.name == "Omori")
            friends.Insert(0, target);
        if (target.name == "Aubrey")
            friends.Insert(1, target);
        if (target.name == "Kel")
            friends.Insert(2, target);
        if (target.name == "Hero")
            friends.Insert(3, target);
    }

    public void RemoveFromList(BattleCharacter target)
    {
        if (target.friend)
            friends.Remove(target);
        else
            foes.Remove(target);
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
