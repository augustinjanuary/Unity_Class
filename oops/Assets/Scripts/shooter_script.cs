using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooter_script : MonoBehaviour
{
    public float Maxspeed = 3f;
    public float minSpeed = 1.25f;
    public float speed = 1f;
    
    GameObject Player;
    public GameObject EnemyBullet;
    public GameObject Muzzle;
    

    bool shooting = false;
    bool onCooldown = false;

    void Start(){
        Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
       
        Vector3 targetPosition = new Vector3(Player.transform.position.x, Player.transform.position.y, 1f);
        transform.LookAt(targetPosition);

        float distanceBetween = Vector3.Distance(targetPosition, transform.position);

        if(((distanceBetween > 7f || distanceBetween < 3f)) && !shooting){
            speed = Mathf.MoveTowards(Mathf.Abs(speed), Maxspeed, .1f);
        }
        else{
            speed = Mathf.MoveTowards(Mathf.Abs(speed), minSpeed, .1f);
        }
        
        if (distanceBetween < 5f){
            speed = -Mathf.Abs(speed);
        }
        else{
            speed = Mathf.Abs(speed);
        }

        if((distanceBetween <= 8f && distanceBetween >= 4f) && !onCooldown){
            StartCoroutine(shootEnemyBullet());
        }

        transform.position += transform.forward * speed * Time.deltaTime;
    }
    
    void OnTriggerEnter(Collider collision){
        if(collision.gameObject.tag == "Bullet"){
            Destroy(gameObject);
        }
    }

    IEnumerator shootEnemyBullet()
    {
        shooting = true;
        onCooldown = true;
        yield return new WaitForSeconds(2f);
        Instantiate(EnemyBullet, Muzzle.transform.position, Muzzle.transform.rotation);
        shooting = false;
        yield return new WaitForSeconds(2f);
        onCooldown = false;
    }
}
