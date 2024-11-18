using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooter_script : MonoBehaviour
{
    public float Maxspeed = 3f;
    public float minSpeed = 1.25f;
    public float speed = 1f;
    
    public GameObject Player;
    public float distanceBetween;

    bool shooting = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = new Vector3(Player.transform.position.x, Player.transform.position.y, 2f);
        transform.LookAt(targetPosition);

        distanceBetween = Vector3.Distance(targetPosition, transform.position);

        if(((Vector3.Distance(targetPosition, transform.position) > 7f ||Vector3.Distance(targetPosition, transform.position) < 3f)) && !shooting){
            speed = Mathf.MoveTowards(Mathf.Abs(speed), Maxspeed, .1f);
        }
        else{
            speed = Mathf.MoveTowards(Mathf.Abs(speed), minSpeed, .1f);
        }
        
        if (Vector3.Distance(targetPosition, transform.position) < 5f){
            speed = -Mathf.Abs(speed);
        }
        else{
            speed = Mathf.Abs(speed);
        }

            transform.position += transform.forward * speed * Time.deltaTime;
    }
    
    void OnTriggerEnter(Collider collision){
        Debug.Log("Ouch!");
        if(collision.gameObject.tag == "Bullet"){
            Destroy(gameObject);
        }
    }


    void shootEnemyBullet(){

    }

}
