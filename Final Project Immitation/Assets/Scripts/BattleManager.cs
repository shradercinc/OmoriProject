using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BattleManager : MonoBehaviour
{
    public List<BattleCharacter> friends = new List<BattleCharacter>();
    public List<BattleCharacter> foes = new List<BattleCharacter>();
    List<BattleCharacter> SpeedQueue = new List<BattleCharacter>();
    BattleCharacter[] characterArray;

    void Start()
    {
        characterArray = FindObjectsOfType(typeof(BattleCharacter)) as BattleCharacter[];

        for (int i = 0; i<characterArray.Length; i++)
        {
            if (characterArray[i].friend)
                friends.Add(characterArray[i]);
            else
                foes.Add(characterArray[i]);
        }

        NewRound();
    }

    void SortSpeed()
    {
        SpeedQueue = SpeedQueue.OrderByDescending(o => o.currSpeed).ToList();
    }

    void NewRound()
    {
        for (int i = 0; i < friends.Count; i++)
        {
            SpeedQueue.Add(friends[i]);
            friends[i].currMove = BattleCharacter.Move.NONE;
        }
        for (int i = 0; i < foes.Count; i++)
        {
            SpeedQueue.Add(foes[i]);
            foes[i].currMove = BattleCharacter.Move.NONE;
        }
    }

    void PlayRound()
    {
        while (SpeedQueue.Count > 0)
        {
            SortSpeed();
            SpeedQueue[0].UseMove();
            SpeedQueue.Remove(SpeedQueue[0]);
        }
        NewRound();
    }

    public bool AllFoesDead()
    {
        for (int i = 0; i < foes.Count; i++)
            if (!foes[i].toast)
                return false;
        return true;
    }
}
