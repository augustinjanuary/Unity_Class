using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_script : MonoBehaviour
{

    public float speed = 10.0f;
    public float timeTilDespawn = 2.5f;

    void Start()
    {
        Destroy(gameObject, timeTilDespawn);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += -transform.up * speed * Time.deltaTime;
    }
}
