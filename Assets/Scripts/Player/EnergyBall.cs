using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class EnergyBall : Bullet
{

public Vector2 direction; 
    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    public override void Movement()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

}
