using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Warp : MonoBehaviour
{
    private AudioSource aud;
    public AudioClip sound;
    public Transform otherWarp;
    public Transform Player;
    public Transform Camera;
    InfoCarry info;
    float timer = 0.0f;

    private void Awake()
    {
        info = FindObjectOfType<InfoCarry>().GetComponent<InfoCarry>();
        aud = GetComponent<AudioSource>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && timer >= 1.0f)
        {
            aud.PlayOneShot(sound);
            info.playerPosition = otherWarp.transform.position;
            Player.transform.position = otherWarp.transform.position;
            Camera.transform.position = otherWarp.transform.position;
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

}
