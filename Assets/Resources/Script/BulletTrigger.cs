using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BulletTrigger : MonoBehaviour
{
    AudioSource death;
    AudioSource spawn;
    Scene currentScene;

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "MainGame")
        {
            playAudio("gunfire");
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            Debug.Log("ll");

            if (currentScene.name == "MainGame")
            {
                playAudio("thud");
                death.time = 0.2f;
                StartCoroutine(ExecuteAfterTime(death.clip.length, this.gameObject));
                Destroy(this.gameObject.GetComponent<SpriteRenderer>());
            } else
            {
                Destroy(this.gameObject);
            }
        }

        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("ee");

            if (currentScene.name == "MainGame")
            {
                playAudio("hit");
            }

            StartCoroutine(ExecuteAfterTime(death.clip.length, this.gameObject));
            Destroy(this.gameObject.GetComponent<SpriteRenderer>());
            Destroy(this.gameObject.GetComponent<Collider2D>());
            Destroy(collision.gameObject);
        }
    }

    IEnumerator ExecuteAfterTime(float time, GameObject dead)
    {
        yield return new WaitForSeconds(time);
        Destroy(dead.gameObject);
        // Code to execute after the delay
    }

    public void playAudio (string audioName)
    {
        death = this.gameObject.AddComponent<AudioSource>();
        death.clip = (AudioClip)Resources.Load("Sound/" + audioName);
        death.volume = 0.15f;
        death.Play();
    }
}
