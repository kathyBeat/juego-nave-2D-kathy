using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Bullet
{
   public Vector2 direction;
    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    //override the movement method to move the bullet in a wave patern
    public override void Movement()
    {
        //use direction to move the bullet in a straight line pattern 
        //(o.5,1)
        transform.Translate(direction * speed * Time.deltaTime);
    }

}
