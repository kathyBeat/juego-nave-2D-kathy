using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //Variables
    public float speed = 5.5f;
    public float fireRate = 0.25f;
    public int lives = 3;
    public int shieldsAmount = 3;
    public float canFire = 0.0f; //Time to fire again
    public float shieldDuration = 5.0f;
    public GameObject BulletPref;
    public AudioManager audioManager;
    public AudioSource actualAudio;
    public GameObject shield;
    public Image star;

    private Animator starAnimator;

    public float powerUPCount = 0;
    public float powerUplimit = 1;
    

    public enum ShipState
    {
        FullHealth,
        SlightlyDamaged,
        Damaged,
        HeavilyDamaged,
        Destroyed
    }

    public ShipState shipState;
    public List<Sprite> shipSprites = new List<Sprite>();

    private GameManager gameManager; // Referencia al GameManager

    private void Start()
    {
        shield.SetActive(false);
        gameManager = FindObjectOfType<GameManager>(); // Encuentra el GameManager en la escena
        starAnimator = star.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        CheckBoundaries();
        ChangeWeapon();
        UseShields();
        Fire();
    }

    //Character Movement, use WASD keys to move the player
    void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.right * speed * horizontalInput * Time.deltaTime);
        transform.Translate(Vector3.up * speed * verticalInput * Time.deltaTime);
    }

    void UseShields()
    {
        if (Input.GetKeyDown(KeyCode.Z) && shieldsAmount > 0)
        {
            shieldsAmount--;
            shield.SetActive(true);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }

        if(shield.activeSelf)
        {
            shieldDuration -= Time.deltaTime;
            if (shieldDuration < 0)
            {
                shield.SetActive(false);
                shieldDuration = 5.0f;
                gameObject.GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }

    void CheckBoundaries()
    {
        //Check for boundaries of the game, use Main Camera to set the boundaries
        var cam = Camera.main;
        float xMax = cam.orthographicSize * cam.aspect;
        float yMax = cam.orthographicSize;
        if (transform.position.x > xMax)
        {
            transform.position = new Vector3(-xMax, transform.position.y, 0);
        }
        else if (transform.position.x < -xMax)
        {
            transform.position = new Vector3(xMax, transform.position.y, 0);
        }
        if (transform.position.y > yMax)
        {
            transform.position = new Vector3(transform.position.x, -yMax, 0);
        }
        else if (transform.position.y < -yMax)
        {
            transform.position = new Vector3(transform.position.x, yMax, 0);
        }
    }

    //Player Fire
    void Fire()
    {
     
       if (Input.GetKeyDown(KeyCode.Space) && Time.time > canFire)
       {

        switch (BulletPref.name)
        {
            case "Bullet": //Instantiate the bullet in the center
            Instantiate(BulletPref, transform.position + new Vector3(0, 0.8f, 0),quaternion.identity);
            canFire = Time.time + fireRate;
            //Play the sound of the bullet
             actualAudio.Play();
             break;

             case "Missile": //Intantiate 3 bullets, one in the center and the other two
             // intantiate the bullet in the center
             var bullet1 = Instantiate(BulletPref, transform.position + new Vector3(0, 0.8f,0),quaternion.identity);
             bullet1.GetComponent<Missile>().direction = Vector2.up;

             //Intantiate the bullet in the right
             var bullet2 = Instantiate(BulletPref, transform.position + new Vector3(0.5f, 0.8f,0), quaternion.identity);
             bullet2.GetComponent<Missile>().direction = new Vector2(0.5f, 1);

             //Instantiate the bulleet in the left
             var bullet3 = Instantiate(BulletPref, transform.position + new Vector3(-0.5f, 0.8f,0), quaternion.identity);
             bullet3.GetComponent<Missile>().direction = new Vector2(-0.5f, 1);

             canFire= Time.time + fireRate;
             actualAudio.Play();
             break; 

             case "EnergyBall": //Intantiate 2 bullets, one in the center and the other two
             //Intantiate the bullet in the right
             var bullet4 = Instantiate(BulletPref, transform.position + new Vector3(0.5f, 0.8f,0), quaternion.identity);
             bullet4.GetComponent<EnergyBall>().direction = new Vector2(-1, 0);

             //Instantiate the bulleet in the left
             var bullet5 = Instantiate(BulletPref, transform.position + new Vector3(-0.5f, 0.8f,0), quaternion.identity);
             bullet5.GetComponent<EnergyBall>().direction = new Vector2(1, 0);

             canFire= Time.time + fireRate;
             actualAudio.Play();
             break; 

            case "EnergyCamp": //Intantiate 3 bullets, one in the center and the other two
            if (powerUPCount == powerUplimit)
            {
                 // intantiate the bullet up
                var bullet6 = Instantiate(BulletPref, transform.position + new Vector3(0, -1.2f, 0), quaternion.identity);
                bullet6.GetComponent<EnergyCamp>().direction = new Vector2(0, 1);

                //Instantiate the bulleet on the right
                var bullet7 = Instantiate(BulletPref, transform.position + new Vector3(-1.2f, 0.5f, 0), quaternion.identity);
                var bullet7Component = bullet7.GetComponent<EnergyCamp>();
                bullet7Component.transform.rotation = Quaternion.Euler(0, 0, -90);
                bullet7Component.direction = new Vector2(0, 1);

                // intantiate the bullet down
                var bullet8 = Instantiate(BulletPref, transform.position + new Vector3(0, 2.0f, 0), quaternion.identity);
                var bullet8Component = bullet8.GetComponent<EnergyCamp>();
                bullet8Component.transform.rotation = Quaternion.Euler(0, 0, -180);
                bullet8Component.direction = new Vector2(0, 1);

                ////Instantiate the bulleet on the left
                var bullet9 = Instantiate(BulletPref, transform.position + new Vector3(1.2f, 0.5f, 0), quaternion.identity);
                var bullet9Component = bullet9.GetComponent<EnergyCamp>();
                bullet9Component.transform.rotation = Quaternion.Euler(0, 0, 90);
                bullet9Component.direction = new Vector2(0, 1);

                canFire= Time.time + fireRate;
                actualAudio.Play();
                ResetPowerUP();
                BulletPref = bullets[0].gameObject;
            }
             break; 
        }
       }



    }


    public List<Bullet> bullets;

    public void ChangeWeapon()
    {
        //For changing weapons, use the number keys 1, 2, 3

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            BulletPref = bullets[0].gameObject;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            BulletPref = bullets[1].gameObject;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            BulletPref = bullets[2].gameObject;
        } 
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            BulletPref = bullets[3].gameObject;
        }

    }

    void ChangeShipState()
    {
        var currentState = shipState;
        Debug.Log(currentState);

        //search by name
        var newSprite = shipSprites.Find(x => x.name == currentState.ToString());

        //search by id
        //var newsprite = shipSprites[(int)currentState];

        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = newSprite;

        switch (currentState)
        {
            case ShipState.FullHealth:
                shipState = ShipState.SlightlyDamaged;
                 break;
            case ShipState.SlightlyDamaged:
                shipState = ShipState.Damaged;
                break;
            case ShipState.Damaged:
                shipState = ShipState.HeavilyDamaged;
                break;
            case ShipState.Destroyed:
                break;
        }
    }

    public void AddPowerUp()
    {
        Debug.Log("powerUp " + powerUPCount + "Limit " + powerUplimit);
        if(powerUPCount < powerUplimit)
        {
            powerUPCount += 0.2f;
        }
        
        if(powerUPCount == powerUplimit)
        {
            star.gameObject.SetActive(true);
            starAnimator.enabled = true;
            starAnimator.Play("startAnim");
            gameManager.ActivePowerUpText();
        }
       
    }

    public void ResetPowerUP()
    {
        powerUPCount = 0;
        star.gameObject.SetActive(false);
        starAnimator.enabled = false;
        gameManager.DesactivePowerUpText();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision != null)
        {
            if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Meteor"))
            {
                //Destroy the enemy
                 Destroy(collision.gameObject);
                 ChangeShipState();
                
            if(lives > 1)
            {
                lives--;
                Debug.Log("Lives: " + lives);
                gameManager.UpdateLivesDisplay(); // Actualiza la visualización de vidas
            }
            else
            {
                lives--;
                gameManager.UpdateLivesDisplay(); // Actualiza la UI después de perder una vida
                Destroy(this.gameObject);
                
            }
            }

        }
       
    }

}
