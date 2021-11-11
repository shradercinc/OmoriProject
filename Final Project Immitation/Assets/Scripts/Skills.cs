using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills : MonoBehaviour
{
    public BattleCharacter user;
    public List<int> juiceCost = new List<int>();
    public enum Target { NONE, FRIEND, FOE, ANY };
    public List<Target> skillTargets = new List<Target>();

    private void Start()
    {
        user = gameObject.GetComponent<BattleCharacter>();
    }

    public bool RollDice(float value)
    {
        return (Random.Range(0, 1) <= value);
    }

    public virtual void SetStartingStats()
    {
    }
    public virtual void UseSkillOne()
    {
    }
    public virtual void UseSkillTwo()
    {
    }
    public virtual void UseSkillThree()
    {
    }
    public virtual void UseSkillFour()
    {
    }
    public virtual void UseSkillOne(BattleCharacter target)
    {
    }
    public virtual void UseSkillTwo(BattleCharacter target)
    {
    }
    public virtual void UseSkillThree(BattleCharacter target)
    {
    }
    public virtual void UseSkillFour(BattleCharacter target)
    {
    }

    public float IsEffective (BattleCharacter target)
    {
        if (user.currEmote == BattleCharacter.Emotion.HAPPY)
            switch (target.currEmote)
            {
                case (BattleCharacter.Emotion.ANGRY):
                    return 1.25f;
                case (BattleCharacter.Emotion.ENRAGED):
                    return 1.25f;
                case (BattleCharacter.Emotion.SAD):
                    return 0.75f;
                case (BattleCharacter.Emotion.DEPRESSED):
                    return 0.75f;
                default:
                    return 1.0f;
            }
        else if (user.currEmote == BattleCharacter.Emotion.ECSTATIC)
            switch (target.currEmote)
            {
                case (BattleCharacter.Emotion.ANGRY):
                    return 1.5f;
                case (BattleCharacter.Emotion.ENRAGED):
                    return 1.5f;
                case (BattleCharacter.Emotion.SAD):
                    return 0.5f;
                case (BattleCharacter.Emotion.DEPRESSED):
                    return 0.5f;
                default:
                    return 1.0f;
            }
        else if (user.currEmote == BattleCharacter.Emotion.ANGRY)
            switch (target.currEmote)
            {
                case (BattleCharacter.Emotion.SAD):
                    return 1.25f;
                case (BattleCharacter.Emotion.DEPRESSED):
                    return 1.25f;
                case (BattleCharacter.Emotion.HAPPY):
                    return 0.75f;
                case (BattleCharacter.Emotion.ECSTATIC):
                    return 0.75f;
                default:
                    return 1.0f;
            }
        else if (user.currEmote == BattleCharacter.Emotion.ENRAGED)
            switch (target.currEmote)
            {
                case (BattleCharacter.Emotion.SAD):
                    return 1.5f;
                case (BattleCharacter.Emotion.DEPRESSED):
                    return 1.5f;
                case (BattleCharacter.Emotion.HAPPY):
                    return 0.5f;
                case (BattleCharacter.Emotion.ECSTATIC):
                    return 0.5f;
                default:
                    return 1.0f;
            }
        else if (user.currEmote == BattleCharacter.Emotion.SAD)
            switch (target.currEmote)
            {
                case (BattleCharacter.Emotion.HAPPY):
                    return 1.25f;
                case (BattleCharacter.Emotion.ECSTATIC):
                    return 1.25f;
                case (BattleCharacter.Emotion.ANGRY):
                    return 0.75f;
                case (BattleCharacter.Emotion.ENRAGED):
                    return 0.75f;
                default:
                    return 1.0f;
            }
        else if (user.currEmote == BattleCharacter.Emotion.DEPRESSED)
            switch (target.currEmote)
            {
                case (BattleCharacter.Emotion.HAPPY):
                    return 1.5f;
                case (BattleCharacter.Emotion.ECSTATIC):
                    return 1.5f;
                case (BattleCharacter.Emotion.ANGRY):
                    return 0.5f;
                case (BattleCharacter.Emotion.ENRAGED):
                    return 0.5f;
                default:
                    return 1.0f;
            }
        return 1.0f;
    }
}