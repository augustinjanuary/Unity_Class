using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class player_script : MonoBehaviour
{
    public int jumpDistance = 50;
    public int dashCooldown = 100;
    public int shootCooldown = 10;
    public int playerHealth = 100;
    public float speed = 5.0f;


    public GameObject Bullet;
    public GameObject Muzzle_One;
    public GameObject Muzzle_Two;
    public GameObject[] TP_point = new GameObject[3];
    //1 is poof, 2 is line
    public GameObject[] TP_effects = new GameObject[2];
    GameObject HealthBar;
    GameObject HealthBarValue;

    public float angle;

    Vector3 direction;
    Vector3 mousePos;
    Vector3 object_pos;
    public Vector3 screenBounds;
    public Vector3 negativeBounds;
    



    // Start is called before the first frame update
    void Start()
    {
        HealthBar = GameObject.FindWithTag("HealthBar");
        HealthBarValue = GameObject.FindWithTag("HealthBarValue");

        dashCooldown = 0;
        if (SceneManager.GetActiveScene().name == "Initial Scene"){
            Camera.main.orthographicSize = 5f;
            CalculateScreenBoundaries();
        }
        else {
            Camera.main.orthographicSize = 8.9f;
            CalculateScreenBoundaries();
            Camera.main.orthographicSize = 7f;
            StartCoroutine(drainHealth());
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (dashCooldown > 0)
        {
            dashCooldown--;
        }
        if (shootCooldown > 0)
        {
            shootCooldown--;
        }

        mousePos = Input.mousePosition;

        //Get player direction and apply force
        direction = new Vector3(speed * Input.GetAxisRaw("Horizontal") * Time.deltaTime, speed * Input.GetAxisRaw("Vertical") * Time.deltaTime);
        transform.position += direction;

        //Clamp position to screen borders
        Vector3 transPos = transform.position;

        transPos.x = Mathf.Clamp(transPos.x, -screenBounds.x, screenBounds.x);
        transPos.y = Mathf.Clamp(transPos.y, -screenBounds.y, screenBounds.y);
        

        transform.position = transPos;

        //Get mouse position and point at it
        object_pos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - object_pos.x;
        mousePos.y = mousePos.y - object_pos.y;

        angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));


        //Jump with keyboard
        if ((Input.GetAxisRaw("Fire3") != 0) && (direction != Vector3.zero) && (dashCooldown <= 0))
        {
            Instantiate(TP_effects[0], transform.position, Quaternion.identity);
            transform.position = transform.position + direction * jumpDistance;
            Instantiate(TP_effects[1], TP_point[0].transform.position, Quaternion.Euler(direction));
            Instantiate(TP_effects[1], TP_point[1].transform.position, transform.rotation);
            Instantiate(TP_effects[1], TP_point[2].transform.position, transform.rotation);
            dashCooldown = 100;
        }

        //Jump with mouse
        if ((Input.GetAxisRaw("Fire1") != 0) && (dashCooldown <= 0))
        {
            transform.position = transform.position + Vector3.ClampMagnitude(new Vector3(mousePos.x, mousePos.y, 0f).normalized * jumpDistance, 5.0f);
            dashCooldown = 100;
        }

        if ((Input.GetAxisRaw("Jump") != 0) && shootCooldown <= 0){
            shootCooldown = 10;
            shoot();
        }
        HealthBar.GetComponent<Image>().fillAmount = (float)playerHealth/100;
        HealthBarValue.GetComponent<TMP_Text>().text = playerHealth.ToString();
    }
    
    public void CalculateScreenBoundaries()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 1f));
    }


    void shoot(){
        Instantiate(Bullet, Muzzle_One.transform.position, Muzzle_One.transform.rotation);
        Instantiate(Bullet, Muzzle_Two.transform.position, Muzzle_Two.transform.rotation);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if((collider.tag == "Enemy") || (collider.tag == "EnemyBullet"))
        {
            playerHealth -= 10;
        }
    }

    IEnumerator drainHealth(){
        yield return new WaitForSeconds(2f);
        playerHealth -=1;
        StartCoroutine(drainHealth());
    }
}


