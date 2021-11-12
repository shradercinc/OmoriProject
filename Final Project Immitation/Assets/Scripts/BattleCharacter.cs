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

    public int currHealth;
    public int currJuice;
    public float currAttack;
    public float currDefense;
    public float currSpeed;
    public float currLuck;
    public float currAccuracy;

    private void Awake()
    {
        userSkills = gameObject.GetComponent<Skills>();
        currEmote = Emotion.NEUTRAL;
        userSkills.SetStartingStats();

        currHealth = startingHealth;
        currJuice = startingJuice;
        ResetStats();
    }

    void ResetStats()
    {
        currAttack = startingAttack * attackStat;
        currDefense = startingDefense * defenseStat;
        currSpeed = startingSpeed * speedStat;
        currLuck = startingLuck * luckStat;
        currAccuracy = 1.0f * accuracyStat;
    }

    public void UseMove()
    {
        if (!toast)
        {
            switch (currMove)
            {
                case Move.ATTACK:
                    //userSkills.BasicAttack();
                    break;
                case Move.SKILL1:
                    currJuice -= userSkills.juiceCost[0];
                    userSkills.UseSkillOne();
                    break;
                case Move.SKILL2:
                    currJuice -= userSkills.juiceCost[1];
                    userSkills.UseSkillTwo();
                    break;
                case Move.SKILl3:
                    currJuice -= userSkills.juiceCost[2];
                    userSkills.UseSkillThree();
                    break;
                case Move.SKILL4:
                    currJuice -= userSkills.juiceCost[3];
                    userSkills.UseSkillFour();
                    break;
                case Move.NONE:
                    break;
            }
        }
    }

    public void TakeDamage(int n)
    {
        currHealth -= n;
        if (currHealth > startingHealth)
            currHealth = startingHealth;
        else if (currHealth <= 0)
            toast = true;
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