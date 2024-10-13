using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameManager gameManager;
    // Start is called before the first frame update

    public float speed = 5.0f;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    public virtual void Movement()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }


 void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision != null)
        {
            Debug.Log("Collided with : " + collision.gameObject.name);

            if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Meteor"))
            {
                //Destroy the enemy
                gameManager.AddScore(10);
                gameManager.player.AddPowerUp();
                Destroy(collision.gameObject);
                Destroy(this.gameObject);
                
            }

        }
       
    }

}
