using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCharacter : MonoBehaviour
{
    public enum Emotion { NEUTRAL, HAPPY, ECSTATIC, ANGRY, ENRAGED, SAD, DEPRESSED };
    public Emotion currEmote = Emotion.NEUTRAL;
    public bool toast = false;
    public bool friend;

    public enum Move { NONE, ATTACK, SKILL1, SKILL2, SKILl3, SKILL4 };
    public Move currMove = Move.NONE;
    private Skills userSkills;

    public int startingHealth;
    public int startingJuice;
    public int startingAttack;
    public int startingDefense;
    public int startingSpeed;
    public int startingLuck;

    public float attackStat = 1;
    public float defenseStat = 1;
    public float speedStat = 1;
    public float luckStat = 1;
    public float accuracyStat = 1;

    int currHealth;
    int currJuice;
    float currAttack;
    float currDefense;
    public float currSpeed;
    float currLuck;
    float currAccuracy;

    private void Start()
    {
        userSkills = gameObject.GetComponent<Skills>();
        currEmote = Emotion.NEUTRAL;
        userSkills.SetStartingStats();

        currHealth = startingHealth;
        currJuice = startingJuice;
        ResetStats();
    }

    public void TakeDamage(int n)
    {
        currHealth -= n;
        if (currHealth > startingHealth)
            currHealth = startingHealth;
        else if (currHealth <= 0)
            toast = true;
    }

    void ResetStats()
    {
        currAttack = startingAttack * attackStat;
        currDefense = startingDefense * defenseStat;
        currSpeed = startingSpeed * speedStat;
        currLuck = startingLuck * luckStat;
        currAccuracy = 1.0f * accuracyStat;
    }

    public void NewEmotion(Emotion newEmote)
    {
        if (newEmote == Emotion.HAPPY && (currEmote == Emotion.HAPPY || currEmote == Emotion.ECSTATIC))
            currEmote = Emotion.ECSTATIC;

        else if (newEmote == Emotion.ANGRY && (currEmote == Emotion.ANGRY || currEmote == Emotion.ENRAGED))
            currEmote = Emotion.ENRAGED;

        else if (newEmote == Emotion.SAD && (currEmote == Emotion.SAD || currEmote == Emotion.DEPRESSED))
            currEmote = Emotion.DEPRESSED;

        else
            currEmote = newEmote;

        ResetStats();

        switch (currEmote)
        {
            case (Emotion.NEUTRAL):
                break;
            case (Emotion.HAPPY):
                currSpeed = startingSpeed * 1.5f * speedStat;
                currLuck = startingLuck * 1.5f * luckStat;
                currAccuracy = 0.8f * accuracyStat;
                break;
            case (Emotion.ECSTATIC):
                currSpeed = startingSpeed * 2 * speedStat;
                currLuck = startingLuck * 2 * luckStat;
                currAccuracy = 0.6f * accuracyStat;
                break;
            case (Emotion.ANGRY):
                currAttack = startingAttack * 1.25f * attackStat;
                currDefense = startingDefense * 0.75f * defenseStat;
                break;
            case (Emotion.ENRAGED):
                currAttack = startingAttack * 1.5f * attackStat;
                currDefense = startingDefense * 0.5f * defenseStat;
                break;
            case (Emotion.SAD):
                currDefense = startingDefense * 1.5f * defenseStat;
                currSpeed = startingSpeed * 0.75f * speedStat;
                break;
            case (Emotion.DEPRESSED):
                currDefense = startingDefense * 2f * defenseStat;
                currSpeed = startingSpeed * 0.5f * speedStat;
                break;
        }

    }
}