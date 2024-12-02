using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_script : MonoBehaviour
{

    public float speed = 10.0f;
    public float timeTilDespawn = 2.5f;

    float BorderX, BorderY;

    public Vector3 direction;

    void Start()
    {

        BorderX = 17; 
        BorderY = 12;

        Destroy(gameObject, timeTilDespawn);

        if(gameObject.tag == "Enemy"){
            direction = transform.forward;
        } else {
            direction = -transform.up;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        //not bothered to find the actual ends of the border, should be close enough
        if(transform.position.x > BorderX*2 ||  transform.position.y > BorderY*2 ||  transform.position.x < -BorderX*2 || transform.position.y < -BorderY * 2)
        {
            Destroy(gameObject);
        }
    }
}
