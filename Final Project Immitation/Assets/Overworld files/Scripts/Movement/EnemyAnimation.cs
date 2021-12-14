using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private SpriteRenderer spr;
    private float timer = 0.0f;
    public Sprite F1;
    public Sprite F2;

    private void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
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
