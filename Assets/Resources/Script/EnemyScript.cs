using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    GameObject target;

    PlayerScript playerScript;

    int value = 3;

    float moveSpeed = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player");

        playerScript = target.GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = (Vector2)target.GetComponent<Transform>().position - (Vector2)transform.position;
        transform.up = direction;

        transform.Translate(Vector2.up * Time.deltaTime * moveSpeed);
    }

    private void OnDestroy()
    {
        playerScript.coinAmount = playerScript.coinAmount + value;
        playerScript.coins.GetComponent<Text>().text = playerScript.coinAmount.ToString();

    }
}
