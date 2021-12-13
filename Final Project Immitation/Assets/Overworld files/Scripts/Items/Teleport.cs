using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    public string nextScene;
    public Vector2 startingPosition;
    InfoCarry info;
    float timer = 0.0f;

    private void Awake()
    {
        info = FindObjectOfType<InfoCarry>().GetComponent<InfoCarry>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && timer >= 1.0f)
        {
            info.playerPosition = startingPosition;
            info.sceneName = nextScene;
            SceneManager.LoadScene(nextScene);
            for (int i = 0; i < LeadMovement.PrevPos.Count; i++)
            {
                LeadMovement.PrevPos[i] = startingPosition;
            }
        }
    }
}
