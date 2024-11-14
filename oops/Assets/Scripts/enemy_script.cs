using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_script : MonoBehaviour
{
    public float speed = 25f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += -Vector3.up * speed * Time.deltaTime;

        if(transform.position.y < -9.5){
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider collision){
        Debug.Log("Ouch!");
        if(collision.gameObject.tag == "Bullet"){
            Destroy(gameObject);
        }
    }

}
