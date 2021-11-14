using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCharacter : MonoBehaviour
{
    public enum Emotion { NEUTRAL, HAPPY, ECSTATIC, ANGRY, ENRAGED, SAD, DEPRESSED };
    public Emotion currEmote = Emotion.NEUTRAL;
    public bool toast = false;
    public bool friend;

    Skills userSkills;
    BattleCharacter nextTarget;
    BattleManager manager;

    public enum Move { NONE, ATTACK, SKILL1, SKILL2, SKILL3, SKILL4 };
    public Move currMove = Move.NONE;

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

    public void ResetStats()
    {
        if (attackStat < 0.7f)
            attackStat = 0.7f;
        else if (attackStat > 1.3f)
            attackStat = 1.3f;
        if (defenseStat < 0.7f)
            defenseStat = 0.7f;
        else if (defenseStat > 1.3f)
            defenseStat = 1.3f;
        if (speedStat < 0.7f)
            speedStat = 0.7f;
        else if (speedStat > 1.3f)
            speedStat = 1.3f;
        if (luckStat < 0.7f)
            luckStat = 0.7f;
        else if (luckStat > 1.3f)
            luckStat = 1.3f;
        if (accuracyStat < 0.7f)
            accuracyStat = 0.7f;
        else if (accuracyStat > 1.3f)
            accuracyStat = 1.3f;

        currAttack = startingAttack * attackStat;
        currDefense = startingDefense * defenseStat;
        currSpeed = startingSpeed * speedStat;
        currLuck = startingLuck * luckStat;
        currAccuracy = 1.0f * accuracyStat;
    }

    private bool CheckIfNeedTarget(int n)
    {
        return (userSkills.skillTargets[n] == Skills.Target.FRIEND ||
            userSkills.skillTargets[n] == Skills.Target.FOE ||
            userSkills.skillTargets[n] == Skills.Target.ANYONE);
    }

    public void UseMove()
    {
        if (toast)
            return;
        switch (currMove)
            {
                case Move.ATTACK:
                {
                    userSkills.BasicAttack(nextTarget);
                    break;
                }
                case Move.SKILL1:
                {
                    currJuice -= userSkills.juiceCost[0];
                    if (nextTarget != null)
                        userSkills.UseSkillOne(nextTarget);
                    else
                        userSkills.UseSkillOne();
                    break;
                }
                case Move.SKILL2:
                {
                    currJuice -= userSkills.juiceCost[1];
                    if (nextTarget != null)
                        userSkills.UseSkillTwo(nextTarget);
                    else
                        userSkills.UseSkillTwo();
                    break;
                }
                case Move.SKILL3:
                {
                    currJuice -= userSkills.juiceCost[2];
                    if (nextTarget != null)
                        userSkills.UseSkillThree(nextTarget);
                    else
                        userSkills.UseSkillThree();
                    break;
                }
                case Move.SKILL4:
                {
                    currJuice -= userSkills.juiceCost[3];
                    if (nextTarget != null)
                        userSkills.UseSkillThree(nextTarget);
                    else
                        userSkills.UseSkillThree();
                    break;
                }
                case Move.NONE:
                {
                    break;
                }
            }
    }

    public void TakeDamage(int n)
    {
        currHealth += n;
        if (currHealth > startingHealth)
            currHealth = startingHealth;
        else if (currHealth <= 0)
        {
            toast = true;
            manager.RemoveFromList(this);
        }
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