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
    List<BattleCharacter> SpeedQueue = new List<BattleCharacter>();

    TMP_Text battleLog;
    TMP_Text energyText;
    GameObject energySlider;
    public double energy = 3;

    void Awake()
    {
        battleLog = GameObject.Find("Battle Log").GetComponent<TextMeshProUGUI>();
        energySlider = GameObject.Find("Energy Slider");
        energyText = energySlider.transform.GetChild(2).GetComponent<TextMeshProUGUI>();

        energySlider.GetComponent<Slider>().value = (float)(energy / 10);
        energyText.text = $"Energy: {energy}";
        StartCoroutine(NewRound());
    }

    public IEnumerator AddEnergy(int n)
    {
        double counter = 0;
        if (n > 0)
        {
            while (counter < n && energy < 10)
            {
                energy+=0.1f;
                counter+=0.1f;

                energySlider.GetComponent<Slider>().value = (float)(energy/10);
                energyText.text = $"Energy: {(int)energy}";

                yield return new WaitForSeconds(0.01f);
            }
        }
        else
        {
            while (counter > n && energy > 0)
            {
                energy-=0.1f;
                counter-=0.1f;

                energySlider.GetComponent<Slider>().value = (float)(energy / 10);
                energyText.text = $"Energy: {energy}";

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

    IEnumerator NewRound()
    {
        friends = friends.OrderBy(o => o.order).ToList();

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

    bool BattleEnd()
    {
        return (friends.Count > 0 && foes.Count > 0);
    }

    IEnumerator ReloadScene()
    {
        bool decision = true;
        while (decision)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            else
                yield return null;
        }
    }

    IEnumerator PlayRound()
    {
        while (BattleEnd() && SpeedQueue.Count > 0)
        {
            SpeedQueue = SpeedQueue.OrderByDescending(o => o.currSpeed).ToList();
            BattleCharacter nextInLine = SpeedQueue[0];
            SpeedQueue.Remove(nextInLine);

            if (!nextInLine.toast)
            {
                if (nextInLine.weapon != null)
                    yield return nextInLine.weapon.StartOfTurn();
                yield return nextInLine.UseMove();

                if (BattleEnd() && energy >= 3 && nextInLine.friend && nextInLine.currMove == BattleCharacter.Move.ATTACK)
                    yield return FollowUp(nextInLine);
                else
                    yield return new WaitForSeconds(1.5f);
            }
        }

        if (BattleEnd())
        {
            StopAllCoroutines();
            AddText("The battle is over.", true);

            if (friends.Count == 0)
            {
                AddText("GAME OVER.");
                AddText("Press space to retry.");
                yield return ReloadScene();
            }
            if (foes.Count == 0)
            {
                AddText("Omori and friends celebrate their victory!");
            }
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(NewRound());
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