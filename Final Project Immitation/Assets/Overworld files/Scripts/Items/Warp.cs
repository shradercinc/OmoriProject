using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Warp : MonoBehaviour
{
    public Transform otherWarp;
    public Transform Player;
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
            info.playerPosition = otherWarp.transform.position;
            Player.transform.position = otherWarp.transform.position;
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

}
