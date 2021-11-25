using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleCharacter : MonoBehaviour
{
    public int startingHealth;
    public int startingJuice;
    public int startingAttack;
    public int startingDefense;
    public int startingSpeed;
    public float startingLuck;
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

    public enum Move { NONE, ATTACK, SKILL1, SKILL2, SKILL3, SKILL4 };
    public Move currMove = Move.NONE;
    public BattleCharacter nextTarget;

    public enum Emotion { NEUTRAL, HAPPY, ECSTATIC, ANGRY, ENRAGED, SAD, DEPRESSED };
    public Emotion currEmote = Emotion.NEUTRAL;

    GameObject centerObject;
    TMP_Text originalTextBox;
    TMP_Text uiText;
    TMP_Text emoteText;

    public GameObject healthObject;
    public GameObject juiceObject;

    Slider healthSlider;
    TMP_Text healthText;

    Slider juiceSlider;
    TMP_Text juiceText;

    public bool toast = false;
    public bool friend;
    public int order;

    public Skills userSkills;
    BattleManager manager;
    public Weapon weapon;

    private void Awake()
    {
        manager = FindObjectOfType<BattleManager>().GetComponent<BattleManager>();
        centerObject = GameObject.Find("EnemyUI");

        currEmote = Emotion.NEUTRAL;
        userSkills = gameObject.GetComponent<Skills>();
        userSkills.SetStartingStats();
        manager.ReturnToList(this);

        if (!friend)
        {
            originalTextBox = GameObject.Find("Original Text Box").GetComponent<TextMeshProUGUI>();
            emoteText = Instantiate(originalTextBox, centerObject.transform);
            gameObject.transform.SetParent(centerObject.transform);
            uiText = Instantiate(originalTextBox, centerObject.transform);
        }
        else
        {
            emoteText = gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
            uiText = gameObject.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();

            healthObject = gameObject.transform.GetChild(2).gameObject;
            healthSlider = healthObject.GetComponent<Slider>();
            healthText = healthObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>();

            juiceObject = gameObject.transform.GetChild(3).gameObject;
            juiceSlider = juiceObject.GetComponent<Slider>();
            juiceText = juiceObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        }

        uiText.text = gameObject.name;
        weapon = gameObject.GetComponent<Weapon>();
        if (weapon != null)
            weapon.AffectUser();

        currHealth = startingHealth;
        currJuice = startingJuice;
        StartCoroutine(ResetStats());
    }

    public IEnumerator ChooseSkill()
    {
        currMove = Move.NONE;
        int n = 0;

        if (!toast)
        {
            manager.AddText("What will " + gameObject.name + " do this turn?", true);
            manager.AddText("1: Basic Attack");

            if (currJuice >= userSkills.juiceCost[1])
                manager.AddText("2: " + userSkills.skillNames[1] + " - " + userSkills.juiceCost[1] + " juice");
            else
                manager.AddText("2: " + userSkills.skillNames[1] + " - Not Enough Juice", false);

            if (currJuice >= userSkills.juiceCost[2])
                manager.AddText("3: " + userSkills.skillNames[2] + " - " + userSkills.juiceCost[2] + " juice");
            else
                manager.AddText("3: " + userSkills.skillNames[2] + " - Not Enough Juice", false);

            if (currJuice >= userSkills.juiceCost[3])
                manager.AddText("4: " + userSkills.skillNames[3] + " - " + userSkills.juiceCost[3] + " juice");
            else
                manager.AddText("4: " + userSkills.skillNames[3] + " - Not Enough Juice");

            if (currJuice >= userSkills.juiceCost[4])
                manager.AddText("5: " + userSkills.skillNames[4] + " - " + userSkills.juiceCost[4] + " juice");
            else
                manager.AddText("5: " + userSkills.skillNames[4] + " - Not Enough Juice");

            while (currMove == Move.NONE)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    currMove = Move.ATTACK;
                    n = 0;
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2) && currJuice >= userSkills.juiceCost[1])
                {
                    currMove = Move.SKILL1;
                    n = 1;
                }
                else if (Input.GetKeyDown(KeyCode.Alpha3) && currJuice >= userSkills.juiceCost[2])
                {
                    currMove = Move.SKILL2;
                    n = 2;
                }
                else if (Input.GetKeyDown(KeyCode.Alpha4) && currJuice >= userSkills.juiceCost[3])
                {
                    currMove = Move.SKILL3;
                    n = 3;
                }
                else if (Input.GetKeyDown(KeyCode.Alpha5) && currJuice >= userSkills.juiceCost[4])
                {
                    currMove = Move.SKILL4;
                    n = 4;
                }

                yield return null;
            }

            switch (userSkills.skillTargets[n])
            {
                case Skills.Target.FRIEND:
                    yield return ChooseTarget(manager.friends);
                    break;
                case Skills.Target.FOE:
                    yield return ChooseTarget(manager.foes);
                    break;
                case Skills.Target.ANYONE:
                    yield return ChooseTarget(manager.GetAllTargets());
                    break;
                default:
                    nextTarget = null;
                    break;
            }
        }
    }

    private IEnumerator ChooseTarget(List<BattleCharacter> possibleTargets)
    {
        manager.AddText("Who will " + gameObject.name + " target?", true);
        nextTarget = null;

        for (int i = 0; i<possibleTargets.Count; i++)
            manager.AddText((i+1).ToString() + ": " + possibleTargets[i].name);

        while (nextTarget == null)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && possibleTargets.Count >= 0)
                nextTarget = possibleTargets[0];
            else if (Input.GetKeyDown(KeyCode.Alpha2) && possibleTargets.Count >= 1)
                nextTarget = possibleTargets[1];
            else if (Input.GetKeyDown(KeyCode.Alpha3) && possibleTargets.Count >= 2)
                nextTarget = possibleTargets[2];
            else if (Input.GetKeyDown(KeyCode.Alpha4) && possibleTargets.Count >= 3)
                nextTarget = possibleTargets[3];
            else if (Input.GetKeyDown(KeyCode.Alpha5) && possibleTargets.Count >= 4)
                nextTarget = possibleTargets[4];
            else if (Input.GetKeyDown(KeyCode.Alpha6) && possibleTargets.Count >= 5)
                nextTarget = possibleTargets[5];
            else if (Input.GetKeyDown(KeyCode.Alpha7) && possibleTargets.Count >= 6)
                nextTarget = possibleTargets[6];
            else if (Input.GetKeyDown(KeyCode.Alpha8) && possibleTargets.Count >= 7)
                nextTarget = possibleTargets[7];
            else if (Input.GetKeyDown(KeyCode.Alpha9) && possibleTargets.Count >= 8)
                nextTarget = possibleTargets[8];

            yield return null;
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

    public IEnumerator UseMove()
    {
        if (!toast)
        {
            switch (currMove)
            {
                case Move.ATTACK:
                    {
                        yield return userSkills.BasicAttack(nextTarget);
                        break;
                    }
                case Move.SKILL1:
                    {
                        yield return userSkills.UseSkillOne(nextTarget);
                        break;
                    }
                case Move.SKILL2:
                    {
                        yield return userSkills.UseSkillTwo(nextTarget);
                        break;
                    }
                case Move.SKILL3:
                    {
                        yield return userSkills.UseSkillThree(nextTarget);
                        break;
                    }
                case Move.SKILL4:
                    {
                        yield return userSkills.UseSkillFour(nextTarget);
                        break;
                    }
                case Move.NONE:
                    {
                        break;
                    }
            }
        }

        ResetStats();
    }

    public IEnumerator TakeDamage(int damage)
    {
        damage = (int)(damage * Random.Range(0.8f, 1.2f));

        if (damage > 0)
        {
            currHealth -= damage;
            manager.AddText(gameObject.name + " takes " + damage + " damage.");

            if (friend)
                manager.AddEnergy();
        }
        else if (!toast)
        {
            manager.AddText(gameObject.name + " didn't take any damage.");
        }

        yield return ResetStats();
    }

    public bool DrainJuice(int juice)
    {
        if (currJuice >= juice)
        {
            currJuice -= juice;
            return true;
        }
        else
        {
            manager.AddText(gameObject.name + " doesn't have enough juice.");
            return false;
        }
    }

    public IEnumerator TakeHealing(int health, int juice)
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
        yield return ResetStats();
    }

    public IEnumerator NewEmotion(Emotion newEmote)
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
            case Emotion.NEUTRAL:
                manager.AddText(gameObject.name + " became Neutral.");
                break;
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

        yield return ResetStats();
    }

    public IEnumerator ResetStats()
    {
        if (currHealth <= 0)
        {
            toast = true;
            currHealth = 0;

            manager.RemoveFromList(this);
            manager.AddText(gameObject.name + " is now toast.");

            emoteText.text = "TOAST";
            currEmote = Emotion.NEUTRAL;

            attackStat = 1;
            defenseStat = 1;
            speedStat = 1;
            luckStat = 1;
            accuracyStat = 1;

            if (weapon != null)
                yield return weapon.OnToast();

            if (!friend)
            {
                yield return new WaitForSeconds(1.5f);
                Destroy(emoteText.gameObject);
                Destroy(uiText.gameObject);
                Destroy(healthObject.gameObject);
                Destroy(gameObject);
            }
        }
    
        else
        {
            toast = false;
            if (attackStat < 0.5f)
                attackStat = 0.5f;
            else if (attackStat > 1.5f)
                attackStat = 1.5f;
            if (defenseStat < 0.5f)
                defenseStat = 0.5f;
            else if (defenseStat > 1.5f)
                defenseStat = 1.5f;
            if (speedStat < 0.5f)
                speedStat = 0.5f;
            else if (speedStat > 1.5f)
                speedStat = 1.5f;
            if (luckStat < 0.5f)
                luckStat = 0.5f;
            else if (luckStat > 1.5f)
                luckStat = 1.5f;
            if (accuracyStat < 0.5f)
                accuracyStat = 0.5f;
            else if (accuracyStat > 1.5f)
                accuracyStat = 1.5f;

            currAttack = startingAttack * attackStat;
            currDefense = startingDefense * defenseStat;
            currSpeed = startingSpeed * speedStat;
            currLuck = startingLuck * luckStat;
            currAccuracy = startingAccuracy * accuracyStat;

            switch (currEmote)
            {
                case (Emotion.NEUTRAL):
                    emoteText.text = "NEUTRAL";
                    break;
                case (Emotion.HAPPY):
                    currSpeed = startingSpeed * 2 * speedStat;
                    currLuck = startingLuck * 2 * luckStat;
                    currAccuracy = startingAccuracy * 0.85f * accuracyStat;
                    emoteText.text = "HAPPY";
                    break;
                case (Emotion.ECSTATIC):
                    currSpeed = startingSpeed * 3 * speedStat;
                    currLuck = startingLuck * 3 * luckStat;
                    currAccuracy = startingAccuracy * 0.7f * accuracyStat;
                    emoteText.text = "ECSTATIC";
                    break;
                case (Emotion.ANGRY):
                    currAttack = startingAttack * 1.25f * attackStat;
                    currDefense = startingDefense * 0.75f * defenseStat;
                    emoteText.text = "ANGRY";
                    break;
                case (Emotion.ENRAGED):
                    currAttack = startingAttack * 1.5f * attackStat;
                    currDefense = startingDefense * 0.5f * defenseStat;
                    emoteText.text = "ENRAGED";
                    break;
                case (Emotion.SAD):
                    currDefense = startingDefense * 1.25f * defenseStat;
                    currSpeed = startingSpeed * 0.75f * speedStat;
                    emoteText.text = "SAD";
                    break;
                case (Emotion.DEPRESSED):
                    currDefense = startingDefense * 1.5f * defenseStat;
                    currSpeed = startingSpeed * 0.5f * speedStat;
                    emoteText.text = "DEPRESSED";
                    break;
            }

            if (currHealth > startingHealth)
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

        if (friend)
        {
        healthSlider.value = (float)currHealth / startingHealth;
        healthText.text = $"{currHealth} / {startingHealth}";
            juiceSlider.value = (float)currJuice / startingJuice;
            juiceText.text = $"{currJuice} / {startingJuice}";
        }
    }
}