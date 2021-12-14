using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BattleAnimation : MonoBehaviour
{
    private Image spr;
    private float timer = 0.0f;
    public Sprite F1;
    public Sprite F2;

    private void Awake()
    {
        spr = GetComponent<Image>();
    }

    private void FixedUpdate()
    {

        timer++;
        if (timer < 0.4f * 50)
        {
            spr.sprite = F1;
        }
        if (timer > 0.4f * 50)
        {
            spr.sprite = F2;
        }
        if (timer >= 0.8f * 50)
        {
            timer = 0;
        }

    }
}
