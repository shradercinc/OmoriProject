using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCharacter : MonoBehaviour
{
    public int startingHealth;
    public int startingJuice;
    public int startingAttack;
    public int startingDefense;
    public int startingSpeed;
    public int startingLuck;
    public float startingAccuracy;

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

    enum Move { NONE, ATTACK, SKILL1, SKILL2, SKILL3, SKILL4 };
    Move currMove = Move.NONE;
    BattleCharacter nextTarget;

    public enum Emotion { NEUTRAL, HAPPY, ECSTATIC, ANGRY, ENRAGED, SAD, DEPRESSED };
    public Emotion currEmote = Emotion.NEUTRAL;

    public bool toast = false;
    public bool friend;

    Skills userSkills;
    BattleManager manager;
    public Weapon weapon;

    private void Awake()
    {
        manager = FindObjectOfType<BattleManager>().GetComponent<BattleManager>();

        currEmote = Emotion.NEUTRAL;
        userSkills = gameObject.GetComponent<Skills>();
        userSkills.SetStartingStats();

        weapon = gameObject.GetComponent<Weapon>();
        if (weapon != null)
            weapon.AffectUser();

        currHealth = startingHealth;
        currJuice = startingJuice;
        ResetStats();
    }

    public IEnumerator ChooseSkill()
    {
        currMove = Move.NONE;
        int n = 0;

        if (!toast)
        {
            manager.AddText("What will " + gameObject.name + " do this turn?");

            while (currMove == Move.NONE)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    currMove = Move.ATTACK;
                    n = 0;
                }
                else if (Input.GetKeyDown(KeyCode.W) && currJuice >= userSkills.juiceCost[1])
                {
                    currMove = Move.SKILL1;
                    n = 1;
                }
                else if (Input.GetKeyDown(KeyCode.E) && currJuice >= userSkills.juiceCost[2])
                {
                    currMove = Move.SKILL2;
                    n = 2;
                }
                else if (Input.GetKeyDown(KeyCode.R) && currJuice >= userSkills.juiceCost[3])
                {
                    currMove = Move.SKILL3;
                    n = 3;
                }
                else if (Input.GetKeyDown(KeyCode.T) && currJuice >= userSkills.juiceCost[4])
                {
                    currMove = Move.SKILL4;
                    n = 4;
                }

                yield return null;
            }

            switch (userSkills.skillTargets[n])
            {
                case Skills.Target.FRIEND:
                    nextTarget = manager.friends[Random.Range(0, manager.friends.Count)];
                    break;
                case Skills.Target.FOE:
                    nextTarget = manager.foes[Random.Range(0, manager.foes.Count)];
                    break;
                case Skills.Target.ANYONE:
                    List<BattleCharacter> allTargets = manager.GetAllTargets();
                    nextTarget = allTargets[Random.Range(0, allTargets.Count)];
                    break;
                default:
                    nextTarget = null;
                    break;
            }
        }
    }

    public void ChooseRandomSkill()
    {
        currMove = Move.NONE;
        if (!toast)
        {
            int n = Random.Range(0, 5);

            if (n == 0)
                currMove = Move.ATTACK;
            else if (n == 1)
                currMove = Move.SKILL1;
            else if (n == 2)
                currMove = Move.SKILL2;
            else if (n == 3)
                currMove = Move.SKILL3;
            else if (n == 4)
                currMove = Move.SKILL4;

            switch (userSkills.skillTargets[n])
            {
                case Skills.Target.FRIEND:
                    nextTarget = manager.friends[Random.Range(0, manager.friends.Count)];
                    break;
                case Skills.Target.FOE:
                    nextTarget = manager.foes[Random.Range(0, manager.foes.Count)];
                    break;
                case Skills.Target.ANYONE:
                    List<BattleCharacter> allTargets = manager.GetAllTargets();
                    nextTarget = allTargets[Random.Range(0, allTargets.Count)];
                    break;
                default:
                    nextTarget = null;
                    break;
            }
        }
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
                    userSkills.UseSkillOne(nextTarget);
                    break;
                }
                case Move.SKILL2:
                {
                    userSkills.UseSkillTwo(nextTarget);
                    break;
                }
                case Move.SKILL3:
                {
                    userSkills.UseSkillThree(nextTarget);
                    break;
                }
                case Move.SKILL4:
                {
                    userSkills.UseSkillThree(nextTarget);
                    break;
                }
                case Move.NONE:
                {
                    break;
                }
            }
    }

    public void TakeDamage(int damage)
    {
        if (damage > 0)
        {
            currHealth -= damage;
            manager.AddText(gameObject.name + " takes " + damage + " damage.");
        }
        else
        {
            manager.AddText(gameObject.name + " didn't take any damage.");
        }
        ResetStats();
    }

    public void TakeHealing(int health, int juice)
    {
        if (health > 0)
        {
            currHealth += health;
            manager.AddText(gameObject.name + " recovers " + health + " health.");
        }
        if (juice > 0)
        {
            currJuice += juice;
            manager.AddText(gameObject.name + " recovers " + juice + " juice.");
        }
        ResetStats();
    }

    void nowToast()
    {
        currHealth = 0;
        toast = true;
        manager.RemoveFromList(this);
        manager.AddText(gameObject.name + " is now toast.");

        currEmote = Emotion.NEUTRAL;
        attackStat = 1;
        defenseStat = 1;
        speedStat = 1;
        luckStat = 1;
        accuracyStat = 1;
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

        switch (currEmote)
        {
            case Emotion.HAPPY:
                manager.AddText(gameObject.name + " became Happy.");
                break;
            case Emotion.ECSTATIC:
                manager.AddText(gameObject.name + " became Ecstatic.");
                break;
            case Emotion.ANGRY:
                manager.AddText(gameObject.name + " became Angry.");
                break;
            case Emotion.ENRAGED:
                manager.AddText(gameObject.name + " became Enraged.");
                break;
            case Emotion.SAD:
                manager.AddText(gameObject.name + " became Sad.");
                break;
            case Emotion.DEPRESSED:
                manager.AddText(gameObject.name + " became Depressed.");
                break;
        }

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
        currAccuracy = startingAccuracy * accuracyStat;

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

        if (currHealth <= 0)
            nowToast();
        else if (currHealth > startingHealth)
            currHealth = startingHealth;
        if (currJuice < 0)
            currJuice = 0;
        else if (currJuice > startingJuice)
            currJuice = startingJuice;

        if (currAttack < 0)
            currAttack = 0;
        if (currDefense < 0)
            currDefense = 0;
        if (currSpeed < 0)
            currSpeed = 0;
        if (currLuck < 0)
            currLuck = 0;
        if (currAccuracy < 0)
            currAccuracy = 0;
    }
}