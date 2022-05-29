using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Waves : MonoBehaviour
{
    Text waves;
    Transform leftHole;
    Transform rightHole;
    Transform topHole;
    Transform bottomHole;

    Transform[] spawnLocations;

    public GameObject enemy1Prefab;
    System.Random rng = new System.Random();

    int waveNumber = 1;
    int waveCount = 3;
    int activeCount = 0;
    int previousSpawner = -1;
    int currentSpawner = -1;
    //float[] spawnInterval = { 2, 1.75f, 1.5f, 1.25f, 1, 0.75f, 0.5f, 0.25f, 0.25f };

    // Start is called before the first frame update
    void Start()
    {
        waves = GameObject.Find("Waves").GetComponent<Text>();

        leftHole = GameObject.Find("Left Hole").transform;
        rightHole = GameObject.Find("Right Hole").transform;
        topHole = GameObject.Find("Top Hole").transform;
        bottomHole = GameObject.Find("Bottom Hole").transform;

        spawnLocations = new Transform[] {leftHole,rightHole,topHole,bottomHole};

        //StartCoroutine(SpawnTime(spawnInterval[0], enemy1Prefab));
        StartCoroutine(SpawnTime(2, enemy1Prefab));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            do
            {
                currentSpawner = rng.Next(0, 4);
            } while (currentSpawner == previousSpawner);
            Transform spawn = spawnLocations[currentSpawner];
            Debug.Log(currentSpawner);
            Debug.Log(previousSpawner);
            previousSpawner = currentSpawner;
            GameObject obj = Instantiate(enemy1Prefab, spawn.position, spawn.rotation);
        }
        
    }

    IEnumerator SpawnTime(float time, GameObject enemy)
    {
        //yield return new WaitForSeconds(time);
        while (true)
        {
            //time = spawnInterval[waveNumber - 1];
            time = Mathf.Max(0.25f, time);
            waveCount = Mathf.Min(15, waveCount);
            for (int i = 0; i < waveCount; i++)
            {
                yield return new WaitForSeconds(time);
                do
                {
                    currentSpawner = rng.Next(0, 4);
                } while (currentSpawner == previousSpawner);
                Transform spawn = spawnLocations[currentSpawner];
                //Debug.Log(currentSpawner + "->" + previousSpawner);
                previousSpawner = currentSpawner;
                GameObject obj = Instantiate(enemy1Prefab, spawn.position, spawn.rotation);

                //yield return new WaitForSeconds(time);
            }
            waveNumber++;
            waves.text = waveNumber.ToString();

            time -= 0.25f;
            waveCount += 2;

            yield return new WaitForSeconds(2);
        }
    }
}
