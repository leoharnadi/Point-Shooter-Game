using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{
    PlayerScript playerScript;
    FillbarScript fillbarScript;

    public AudioSource buyAudio;
    public AudioSource noMoneyAudio;

    GameObject fireRateUp1;
    GameObject fireRateUp2;
    GameObject fireRateUp3;
    GameObject fireRateUp4;
    GameObject speedUp1;
    GameObject speedUp2;
    GameObject speedUp3;
    GameObject speedUp4;

    int fireRateLvl = 1;
    int moveSpeedLvl = 1;
    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<PlayerScript>();
        fillbarScript = GameObject.Find("ReloadBar").GetComponent<FillbarScript>();

        fireRateUp1 = GameObject.Find("FireRateUp");
        fireRateUp2 = GameObject.Find("FireRateUp2");
        fireRateUp3 = GameObject.Find("FireRateUp3");
        fireRateUp4 = GameObject.Find("FireRateUp4");

        speedUp1 = GameObject.Find("SpeedUp");
        speedUp2 = GameObject.Find("SpeedUp2");
        speedUp3 = GameObject.Find("SpeedUp3");
        speedUp4 = GameObject.Find("SpeedUp4");

        fireRateUp2.SetActive(false);
        fireRateUp3.SetActive(false);
        fireRateUp4.SetActive(false);
        speedUp2.SetActive(false);
        speedUp3.SetActive(false);
        speedUp4.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LifeUp()
    {
        if(playerScript.coinAmount >= 10)
        {
            playerScript.Health++;
            playerScript.coinAmount -= 10;
            playerScript.coins.text = playerScript.coinAmount.ToString();
            playerScript.lives.text = playerScript.Health.ToString();
            buyAudio.Play();
        } else
        {
            noMoneyAudio.Play();
        }
    }

    public void FireRateUp()
    {
        switch(fireRateLvl)
        {
            case 1:
                if (playerScript.coinAmount >= 15)
                {
                    playerScript.fireRate = 1f;
                    playerScript.coinAmount -= 15;
                    playerScript.coins.text = playerScript.coinAmount.ToString();

                    fillbarScript.MaxTime = 1f;

                    fireRateLvl++;

                    buyAudio.Play();

                    fireRateUp1.SetActive(false);
                    fireRateUp2.SetActive(true);
                }
                else
                {
                    noMoneyAudio.Play();
                }
                break;
            case 2:
                if (playerScript.coinAmount >= 30)
                {
                    playerScript.fireRate = 0.5f;
                    playerScript.coinAmount -= 30;
                    playerScript.coins.text = playerScript.coinAmount.ToString();

                    fillbarScript.MaxTime = 0.5f;

                    fireRateLvl++;

                    buyAudio.Play();

                    fireRateUp2.SetActive(false);
                    fireRateUp3.SetActive(true);
                }
                else
                {
                    noMoneyAudio.Play();
                }
                break;
            case 3:
                if (playerScript.coinAmount >= 50)
                {
                    playerScript.fireRate = 0.25f;
                    playerScript.coinAmount -= 50;
                    playerScript.coins.text = playerScript.coinAmount.ToString();

                    fillbarScript.MaxTime = 0.25f;

                    fireRateLvl++;

                    buyAudio.Play();

                    fireRateUp3.SetActive(false);
                    fireRateUp4.SetActive(true);
                }
                else
                {
                    noMoneyAudio.Play();
                }
                break;
            default:
                break;
        }
    }

    public void moveSpeedUp()
    {
        switch (moveSpeedLvl)
        {
            case 1:
                if (playerScript.coinAmount >= 15)
                {
                    playerScript.moveSpeed = 5f;
                    playerScript.coinAmount -= 15;
                    playerScript.coins.text = playerScript.coinAmount.ToString();

                    moveSpeedLvl++;

                    buyAudio.Play();

                    speedUp1.SetActive(false);
                    speedUp2.SetActive(true);
                }
                else
                {
                    noMoneyAudio.Play();
                }
                break;
            case 2:
                if (playerScript.coinAmount >= 25)
                {
                    playerScript.moveSpeed = 7f;
                    playerScript.coinAmount -= 25;
                    playerScript.coins.text = playerScript.coinAmount.ToString();

                    moveSpeedLvl++;

                    buyAudio.Play();

                    speedUp2.SetActive(false);
                    speedUp3.SetActive(true);
                }
                else
                {
                    noMoneyAudio.Play();
                }
                break;
            case 3:
                if (playerScript.coinAmount >= 50)
                {
                    playerScript.moveSpeed = 10f;
                    playerScript.coinAmount -= 50;
                    playerScript.coins.text = playerScript.coinAmount.ToString();

                    moveSpeedLvl++;

                    buyAudio.Play();

                    speedUp3.SetActive(false);
                    speedUp4.SetActive(true);
                }
                else
                {
                    noMoneyAudio.Play();
                }
                break;
            default:
                break;
        }
    }
}
