using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooter_script : MonoBehaviour
{
    public float Maxspeed = 3f;
    public float minSpeed = 1.25f;
    public float speed = 1f;
    public int healthPoints = 3;
    
    GameObject Player;
    GameObject GM;

    public GameObject EnemyBullet;
    public GameObject Muzzle;
    public GameObject DeathEffects;
    public GameObject HPpickup;

    ParticleSystem ChargeUpEffect;

    public SOscore HpDroprate;

    bool shooting = false;
    bool onCooldown = false;

    void Start(){
        Player = GameObject.FindWithTag("Player");
        GM = GameObject.FindWithTag("GameController");
        ChargeUpEffect = GetComponentInChildren<ParticleSystem>();
        
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

        if (healthPoints <= 0)
        {
            GM.GetComponent<game_master_script>().IncreaseScore(100);
            Instantiate(DeathEffects, transform.position, Quaternion.identity);

            if (Random.Range(0, HpDroprate.oddsOfDrop) == 0)
            {
                Instantiate(HPpickup, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }
    
    void OnTriggerEnter(Collider collision){
        if(collision.gameObject.tag == "Bullet"){
            healthPoints--;
            Destroy(collision.gameObject);
        }
    }

    IEnumerator shootEnemyBullet()
    {
        var emission = ChargeUpEffect.emission;
        shooting = true;
        onCooldown = true;
        emission.enabled = true;
        yield return new WaitForSeconds(1f);
        Instantiate(EnemyBullet, Muzzle.transform.position, Muzzle.transform.rotation);
        shooting = false;
        emission.enabled = false;
        yield return new WaitForSeconds(2f);
        onCooldown = false;
    }
}
