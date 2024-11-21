using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomber_script : MonoBehaviour
{
    public float minSpeed;
    public float maxSpeed;
    float speed;

    GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
