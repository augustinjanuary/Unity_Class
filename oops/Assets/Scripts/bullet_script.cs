using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_script : MonoBehaviour
{

    public float speed = 10.0f;
    public float timeTilDespawn = 2.5f;

    Vector3 direction;

    void Start()
    {
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
    }
}
