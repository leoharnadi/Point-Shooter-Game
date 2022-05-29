using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OtherControls : MonoBehaviour
{
    GameObject pauseBackground;
    GameObject pauseText;
    GameObject restartButton;
    GameObject homeButton;
    GameObject pauseButton;
    GameObject unpauseButton;
    GameObject shopUI;

    AudioSource openShopAudio;
    AudioSource pauseAudio;

    public string homeScene;
    public bool isGameOver = false;
    public static bool isShopOpen;
    public static bool gameIsPaused;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        pauseBackground = GameObject.Find("PauseBackground");
        pauseText = GameObject.Find("PauseText");
        restartButton = GameObject.Find("RestartButton");
        homeButton = GameObject.Find("HomeButton");
        pauseButton = GameObject.Find("PauseButton");
        unpauseButton = GameObject.Find("UnPauseButton");
        shopUI = GameObject.Find("Shop");

        pauseBackground.SetActive(false);
        pauseText.SetActive(false);
        restartButton.SetActive(false);
        homeButton.SetActive(false);
        unpauseButton.SetActive(false);
        shopUI.SetActive(false);

        openShopAudio = this.gameObject.AddComponent<AudioSource>();
        openShopAudio.clip = (AudioClip)Resources.Load("Sound/shop");
        openShopAudio.volume = 0.25f;

        pauseAudio = this.gameObject.AddComponent<AudioSource>();
        pauseAudio.clip = (AudioClip)Resources.Load("Sound/pause");
        pauseAudio.volume = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isShopOpen == false)
            {
                PauseGame();
            } else
            {
                openShop();
            }
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            openShop();
        }
    }

    public void PauseGame()
    {
        if (isGameOver == false && isShopOpen == false) {

            gameIsPaused = !gameIsPaused;

            var AllAudio = FindObjectsOfType<AudioSource>() as AudioSource[];

            if (gameIsPaused)
            {
                Time.timeScale = 0f;

                foreach (AudioSource song in AllAudio)
                {
                    if(song.clip.name != "boom") {
                        song.Pause();
                    }
                }

                pauseButton.SetActive(false);
                unpauseButton.SetActive(true);
                pauseBackground.SetActive(true);
                pauseText.SetActive(true);
                restartButton.SetActive(true);
                homeButton.SetActive(true);
                //unpauseButtonChild.SetActive(true);

                pauseAudio.Play();
            }
            else
            {
                Time.timeScale = 1;

                foreach (AudioSource song in AllAudio)
                {
                    if (song.clip.name != "boom")
                    {
                        song.UnPause();
                    }
                }

                pauseButton.SetActive(true);
                unpauseButton.SetActive(false);
                pauseBackground.SetActive(false);
                pauseText.SetActive(false);
                restartButton.SetActive(false);
                homeButton.SetActive(false);
                //unpauseButtonChild.SetActive(false);
            }
        }
    }

    public void openShop()
    {
        if (isGameOver == false && gameIsPaused == false)
        {

            isShopOpen = !isShopOpen;

            var AllAudio = FindObjectsOfType<AudioSource>() as AudioSource[];

            if (isShopOpen)
            {
                Time.timeScale = 0f;

                foreach (AudioSource song in AllAudio)
                {
                    if (song.clip.name != "OG Song")
                    {
                        song.Pause();
                    }
                }

                openShopAudio.Stop();
                openShopAudio.Play();

                shopUI.SetActive(true);
            }
            else
            {
                Time.timeScale = 1f;

                foreach (AudioSource song in AllAudio)
                {
                    if (song.clip.name != "OG Song")
                    {
                        song.UnPause();
                    }
                }
                
                shopUI.SetActive(false);
            }
        }
    }

    public void RestartGame()
    {
        isGameOver = false;
        PauseGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoHome()
    {
        PauseGame();
        SceneManager.LoadScene(homeScene);
    }
}
