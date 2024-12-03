using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_script : MonoBehaviour
{
    public float speed = 2.5f;
    public float maxSpeed = 5f;
    public int healthPoints = 3;

    public SOscore HpDroprate;

    public GameObject HPpickup;

    GameObject Player;
    Rigidbody rbody;
    GameObject GM;


    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        Player = GameObject.FindWithTag("Player");
        GM = GameObject.FindWithTag("GameController");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = new Vector3(Player.transform.position.x, Player.transform.position.y, 1f);
        transform.LookAt(targetPosition);
        rbody.AddForce(transform.forward * speed);
        rbody.velocity = Vector3.ClampMagnitude(rbody.velocity, maxSpeed);

        if(healthPoints <= 0)
        {
            GM.GetComponent<game_master_script>().IncreaseScore(100);
            Destroy(gameObject);

            if (Random.Range(0, HpDroprate.oddsOfDrop) == 1)
            {
                Instantiate(HPpickup, transform.position, Quaternion.identity);
            }

        }
    }

    void OnTriggerEnter(Collider collision){
        if(collision.gameObject.tag == "Bullet"){
            healthPoints--;
            Destroy(collision.gameObject);
        }
    }

}
