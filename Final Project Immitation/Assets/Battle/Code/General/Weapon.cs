using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public BattleCharacter user;
    public BattleManager manager;
    public string description;

    private void Awake()
    {
        manager = FindObjectOfType<BattleManager>().GetComponent<BattleManager>();
    }

    public virtual void AffectUser()
    {
    }
    public virtual IEnumerator StartOfTurn()
    {
        yield return null;
    }
    public virtual IEnumerator OnToast()
    {
        yield return null;
    }
}