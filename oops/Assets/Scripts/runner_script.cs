using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_script : MonoBehaviour
{
    public float speed = 2.5f;
    public float maxSpeed = 5f;
    public GameObject Player;

    Rigidbody rbody;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = new Vector3(Player.transform.position.x, Player.transform.position.y, 2f);
        transform.LookAt(targetPosition);
        rbody.AddForce(transform.forward * speed);
        rbody.velocity = Vector3.ClampMagnitude(rbody.velocity, maxSpeed);


      /*  if(transform.position.y < -9.5){
            Destroy(gameObject);
            }
      */  
    }

    void OnTriggerEnter(Collider collision){
        Debug.Log("Ouch!");
        if(collision.gameObject.tag == "Bullet"){
            Destroy(gameObject);
        }
    }

}
