using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class BattleManager : MonoBehaviour
{
    public List<BattleCharacter> friends = new List<BattleCharacter>();
    public List<BattleCharacter> foes = new List<BattleCharacter>();

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

        NewRound();
    }

    void NewRound()
    {
        for (int i = 0; i < friends.Count; i++)
        {
            SpeedQueue.Add(friends[i]);
            friends[i].ChooseSkill();
        }
        for (int i = 0; i < foes.Count; i++)
        {
            SpeedQueue.Add(foes[i]);
            foes[i].ChooseRandomSkill();
        }
    }

    void PlayRound()
    {
        while (SpeedQueue.Count > 0)
        {
            SpeedQueue = SpeedQueue.OrderByDescending(o => o.currSpeed).ToList();
            SpeedQueue[0].UseMove();
            SpeedQueue.Remove(SpeedQueue[0]);
        }
    }

    public void ReturnToList(BattleCharacter target)
    {
        if (target.friend)
            friends.Add(target);
        else
            foes.Add(target);
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
