using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float speed = 10f;
    public int health = 1;
  
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     Movement();   
    }


    public void Movement()
    {
        //transform.Translate(Vector3.down * speed * Time.deltaTime);
        if(gameObject.CompareTag("Enemy")){
            transform.Translate(new Vector3(Mathf.Sin(Time.time*0.5f), -1, 0)* speed * Time.deltaTime);
        }else if(gameObject.CompareTag("Meteor")){
            transform.Translate(new Vector3(Mathf.Sin(Time.time*1.5f), -1, 0)* speed * Time.deltaTime);
        }
        
    }



}



