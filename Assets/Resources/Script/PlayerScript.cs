using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    float horizontal;
    float vertical;
    float diagonalSpeed = 0.7f;
    Rigidbody2D body;
    AudioSource hurt;
    AudioSource death;
    Text gameOverText;
    public Text coins;
    Image livesImage;
    Scene currentScene;

    OtherControls otherControls;
    FillbarScript fillbarScript;

    public int Health = 3;
    public float moveSpeed = 3;
    public float bulletSpeed = 750;
    public float fireRate = 1.5f;
    public float nextFire;
    public GameObject bulletPrefab = null;
    public Transform gun = null;

    public int coinAmount = 5;
    public Text lives;

    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();

        body = GetComponent<Rigidbody2D>();

        if (currentScene.name == "MainGame")
        {
            coins = GameObject.Find("Coins").GetComponent<Text>();
            coins.text = coinAmount.ToString();

            lives = GameObject.Find("Lives").GetComponent<Text>();
            livesImage = GameObject.Find("LivesImage").GetComponent<Image>();

            death = this.gameObject.AddComponent<AudioSource>();
            death.clip = (AudioClip)Resources.Load("Sound/boom");
            death.volume = 0.5f;

            hurt = this.gameObject.AddComponent<AudioSource>();
            hurt.clip = (AudioClip)Resources.Load("Sound/oof");
            hurt.volume = 0.5f;

            GameObject target = GameObject.Find("Canvas");
            otherControls = target.GetComponent<OtherControls>();
            gameOverText = GameObject.Find("PauseText").GetComponent<Text>();

            fillbarScript = GameObject.Find("ReloadBar").GetComponent<FillbarScript>();
        }

        

        Debug.Log("hello" + body);
    }
    
    // Update is called once per frame
    void Update()
    {
        if (OtherControls.gameIsPaused != true && OtherControls.isShopOpen != true)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");

            Vector3 mousePos = Input.mousePosition;

            Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
            mousePos.x -= objectPos.x;
            mousePos.y -= objectPos.y;

            float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
            this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));

            /*if (Input.GetMouseButtonDown(0))
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameObject obj = Instantiate(bulletPrefab, gun.position, gun.rotation);
                obj.name = "bullet";
                obj.GetComponent<Rigidbody2D>().AddForce(gun.up * bulletSpeed);
            }*/

            Rect bounds = new Rect(0, 0, Screen.width, Screen.height * 0.89f);
            if (Input.GetMouseButton(0) && bounds.Contains(Input.mousePosition) && coinAmount > 0 && Time.time > nextFire)
            {
                fillbarScript.ActiveTime = 0f;
                nextFire = Time.time + fireRate;
                GameObject obj = Instantiate(bulletPrefab, gun.position, gun.rotation);
                obj.name = "bullet";
                obj.GetComponent<Rigidbody2D>().AddForce(gun.up * bulletSpeed);
                //coinAmount--;
                if (currentScene.name == "MainGame")
                {
                    coinAmount--;
                }
                coins.text = coinAmount.ToString();
            }
        }
    }

    void FixedUpdate()
    {
        if (horizontal != 0 && vertical != 0) // Check for diagonal movement
        {
            // limit movement speed diagonally, so you move at 70% speed
            horizontal *= diagonalSpeed;
            vertical *= diagonalSpeed;
        }

        body.velocity = new Vector2(horizontal * moveSpeed, vertical * moveSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            //Debug.Log("ouch");


            Health--;
            lives.text = Health.ToString();
            Destroy(collision.gameObject);

            if (Health == 0)
            {
                death.Play();
                this.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                livesImage.color = new Color(0.5546498f, 0, 1, 1);
                gameOver();
            } else
            {
                hurt.Play();
            }
        }
    }

    private void gameOver()
    {
        gameOverText.text = "YOU DIED!";
        gameOverText.fontStyle = FontStyle.Bold;
        otherControls.PauseGame();
        otherControls.isGameOver = true;
    }
}

