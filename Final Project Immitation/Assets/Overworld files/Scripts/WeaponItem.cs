using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : MonoBehaviour
{
    public int itemNumber;
    InfoCarry info;

    private void Awake()
    {
        info = FindObjectOfType<InfoCarry>().GetComponent<InfoCarry>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            info.unlockedWeapons[itemNumber] = true;
            info.delete.Add(gameObject);
            gameObject.SetActive(false);
        }
    }
}
