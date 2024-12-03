using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomber_script : MonoBehaviour
{

    float speed = 1f;
    public int healthPoints = 5;

    public GameObject Bullet;
    public GameObject DeathEffects;
    public GameObject HPpickup;

    public Transform[] EXP_point = new Transform[4];

    public SOscore HpDroprate;

    GameObject Player;
    GameObject GM;


    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        GM = GameObject.FindWithTag("GameController");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = new Vector3(Player.transform.position.x, Player.transform.position.y, 1f);

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector3.Distance(targetPosition, transform.position) < 5)
        {
            explode();
        }

        if (healthPoints <= 0)
        {
            GM.GetComponent<game_master_script>().IncreaseScore(100);
            explode();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Bullet")
        {
            Destroy(other.gameObject);
            healthPoints--;
        }
    }

    void explode()
    {
        bullet_script bulletScript = Bullet.GetComponent<bullet_script>();
        float baseSpeed = bulletScript.speed;
        for(int i = 1; i < 4; i++) {
            bulletScript.speed = 1f * i;
            for (int j = 0; j < 2; j++)
            {
                bulletScript.speed *= -1;
                Instantiate(Bullet, transform.position, EXP_point[0].rotation);
                Instantiate(Bullet, transform.position, EXP_point[1].rotation);
                Instantiate(Bullet, transform.position, EXP_point[2].rotation);
                Instantiate(Bullet, transform.position, EXP_point[3].rotation);
            }
        }
        bulletScript.speed = baseSpeed;
        die();
    }

    void die()
    {
        Instantiate(DeathEffects, transform.position, Quaternion.identity);
        if(Random.Range(0, HpDroprate.oddsOfDrop) == 1)
        {
            Instantiate(HPpickup, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
