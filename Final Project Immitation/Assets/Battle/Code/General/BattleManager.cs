using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    public List<BattleCharacter> friends = new List<BattleCharacter>();
    public List<BattleCharacter> foes = new List<BattleCharacter>();
    public List<BattleCharacter> toast = new List<BattleCharacter>();
    public List<BattleCharacter> SpeedQueue = new List<BattleCharacter>();

    BattleCharacter omori;
    InfoCarry info;
    string loadScene;

    TMP_Text battleLog;
    TMP_Text descriptionLog;
    TMP_Text energyText;
    GameObject energySlider;

    public double energy = 0.0;
    public bool undo = false;

    void Awake()
    {
        battleLog = GameObject.Find("Battle Log").GetComponent<TextMeshProUGUI>();
        descriptionLog = GameObject.Find("Description Log").GetComponent<TextMeshProUGUI>();
        info = FindObjectOfType<InfoCarry>().GetComponent<InfoCarry>();

        energySlider = GameObject.Find("Energy Slider");
        energyText = energySlider.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        omori = GameObject.Find("Omori").GetComponent<BattleCharacter>();

        for (int i = 0; i < info.enemies.Count; i++)
        {
            CreateFoe(info.enemies[i], info.enemies[i].name);
        }

        StartCoroutine(AddEnergy(3));
        StartCoroutine(NewRound());
    }

    public BattleCharacter CreateFoe(BattleCharacter prefab, string name)
    {
        if (foes.Count < 5)
        {
            BattleCharacter nextFoe = Instantiate(prefab);
            nextFoe.uiText.text = name;
            nextFoe.name = name;
            return nextFoe;
        }
        return null;
    }

    public IEnumerator AddEnergy(int n)
    {
        double counter = 0;
        if (n > 0)
        {
            while (counter < n && energy < 10)
            {
                energy += 0.1f;
                counter += 0.1f;

                energySlider.GetComponent<Slider>().value = (float)(energy / 10);
                energyText.text = $"Energy: {(int)energy}";

                yield return new WaitForSeconds(0.01f);
            }
        }
        else
        {
            while (counter > n && energy > 0)
            {
                energy -= 0.1f;
                counter -= 0.1f;

                energySlider.GetComponent<Slider>().value = (float)(energy / 10);
                energyText.text = $"Energy: {(int)energy}";

                yield return new WaitForSeconds(0.01f);
            }
        }
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

    public void AddDescription(string x, bool reset, bool disable)
    {
        if (disable)
        {
            descriptionLog.gameObject.transform.parent.gameObject.SetActive(false);
        }
        else if (reset)
        {
            descriptionLog.gameObject.transform.parent.gameObject.SetActive(true);
            descriptionLog.text = x;
        }
        else
        {
            descriptionLog.text += "\n";
            descriptionLog.text += x;
        }
    }

    public void AddDescription(string x)
    {
        AddDescription(x, false, false);
    }

    IEnumerator NewRound()
    {
        yield return ChooseYourSkills();

        for (int i = 0; i < foes.Count; i++)
        {
            SpeedQueue.Add(foes[i]);
            foes[i].ChooseRandomSkill();
        }

        StartCoroutine(PlayRound());
    }

    IEnumerator ChooseYourSkills()
    {
        friends = friends.OrderBy(o => o.order).ToList();
        for (int i = 0; i < friends.Count; i++)
        {
            if (i == 0)
                SpeedQueue.Clear();

            undo = false;
            yield return friends[i].ChooseSkill();
            SpeedQueue.Add(friends[i]);

            if (undo)
                i = -1;
        }
    }

    IEnumerator PlayRound()
    {
        List<BattleCharacter> allTargets = GetAllTargets();
        for (int i = 0; i < allTargets.Count; i++)
        {
            if (allTargets[i].weapon != null)
            {
                yield return allTargets[i].weapon.StartOfTurn();
                yield return new WaitForSeconds(0.25f);
            }
        }
        while (BattleContinue() && SpeedQueue.Count > 0)
        {
            AddDescription("", false, true);
            SpeedQueue = SpeedQueue.OrderByDescending(o => o.currSpeed).ToList();
            BattleCharacter nextInLine = SpeedQueue[0];
            SpeedQueue.Remove(nextInLine);

            if (!nextInLine.toast)
            {
                yield return nextInLine.UseMove();

                if (BattleContinue() && energy >= 3 && !nextInLine.toast && nextInLine.friend && nextInLine.currMove == BattleCharacter.Move.ATTACK)
                    yield return FollowUp(nextInLine);
                else
                    yield return new WaitForSeconds(0.5f);
            }
        }

        if (!BattleContinue())
        {
            AddDescription("", false, true);
            AddText("The battle has ended.", true);

            if (omori.toast)
            {
                AddText("GAME OVER.");
                Debug.Log("Restarting battle...");
                yield return new WaitForSeconds(1.5f);
                SceneManager.LoadScene("Omori Battle");
            }
            else if (foes.Count == 0)
            {
                AddText("VICTORY!");
                yield return new WaitForSeconds(1.5f);
                SceneManager.LoadScene(info.sceneName);
            }
        }
        else
        {
            StartCoroutine(NewRound());
        }
    }

    IEnumerator FollowUp(BattleCharacter user)
    {
        yield return new WaitForSeconds(0.5f);
        bool decision = true;
        Skills userSkills = user.userSkills;

        AddText("Should " + user.name + " use a Follow Up?", true);
        AddText("1: Skip");

        bool skillOne = (energy >= userSkills.energyCost[0] && !userSkills.followUpRequire[0].toast);
        AddDescription(userSkills.skillNames[5] + ": " + userSkills.skillDescription[5], true, false);
        if (skillOne)
            AddText("2: " + userSkills.skillNames[5] + " - " + userSkills.energyCost[0] + " energy");
        else
            AddText("Cannot " + userSkills.skillNames[5]);

        bool skillTwo = (energy >= userSkills.energyCost[1] && !userSkills.followUpRequire[1].toast);
        AddDescription(userSkills.skillNames[6] + ": " + userSkills.skillDescription[6]);
        if (skillTwo)
            AddText("3: " + userSkills.skillNames[6] + " - " + userSkills.energyCost[1] + " energy");
        else
            AddText("Cannot " + userSkills.skillNames[6]);

        bool skillThree;
        if (user.name == "Omori")
            skillThree = (energy >= userSkills.energyCost[2] && friends.Count == 4);
        else
            skillThree = (energy >= userSkills.energyCost[2] && !userSkills.followUpRequire[2].toast);

        AddDescription(userSkills.skillNames[7] + ": " + userSkills.skillDescription[7]);
        if (skillThree)
            AddText("4: " + userSkills.skillNames[7] + " - " + userSkills.energyCost[0] + " energy");
        else
            AddText("Cannot " + userSkills.skillNames[7]);

        while (decision)
        {
            if (Input.GetKeyDown(KeyCode.Alpha2) && skillOne)
            {
                AddDescription("", false, true);
                yield return user.userSkills.FollowUpOne();
                decision = false;
                yield return new WaitForSeconds(0.5f);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) && skillTwo)
            {
                AddDescription("", false, true);
                yield return user.userSkills.FollowUpTwo();
                decision = false;
                yield return new WaitForSeconds(0.5f);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4) && skillThree)
            {
                AddDescription("", false, true);
                yield return user.userSkills.FollowUpThree();
                decision = false;
                yield return new WaitForSeconds(0.5f);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                AddDescription("", false, true);
                decision = false;
            }
            yield return null;
        }
    }

    bool BattleContinue()
    {
        return (!omori.toast && foes.Count > 0);
    }

    public void ReturnToList(BattleCharacter target)
    {
        if (toast.Contains(target))
            toast.Remove(target);
        if (target.friend)
        {
            friends.Add(target);
            friends = friends.OrderBy(o => o.order).ToList();
        }
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

        allTargets = allTargets.OrderByDescending(o => o.currSpeed).ToList();
        return allTargets;
    }
}