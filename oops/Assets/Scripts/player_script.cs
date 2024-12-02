using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class player_script : MonoBehaviour
{
    public int jumpDistance = 50;
    public int dashCooldownLength = 100;
    public int dashCooldown = 0;
    public int shootCooldown = 10;
    public int playerHealth = 100;
    public float speed = 5.0f;

    public TrailRenderer[] tr = new TrailRenderer[3];

    public GameObject Bullet;
    public GameObject Muzzle_One;
    public GameObject Muzzle_Two;
    public GameObject TP_effect;
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
        if (SceneManager.GetActiveScene().name == "Initial Scene") {
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



        //Jump with mouse 1
        if ((Input.GetAxisRaw("Fire1") != 0) && (dashCooldown <= 0))
        {

            StartCoroutine(teleportAnimation());
            Instantiate(TP_effect, transform.position, Quaternion.identity);

            transform.position = transform.position + Vector3.ClampMagnitude(new Vector3(mousePos.x, mousePos.y, 0f).normalized * jumpDistance, 5.0f);
            dashCooldown = dashCooldownLength;
            Instantiate(TP_effect, transform.position, Quaternion.identity);


        }

        if ((Input.GetAxisRaw("Jump") != 0) && shootCooldown <= 0) {
            shootCooldown = 10;
            shoot();
        }
        HealthBar.GetComponent<Image>().fillAmount = (float)playerHealth / 100;
        HealthBarValue.GetComponent<TMP_Text>().text = playerHealth.ToString();
    }

    public void CalculateScreenBoundaries()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 1f));
    }


    void shoot() {
        Instantiate(Bullet, Muzzle_One.transform.position, Muzzle_One.transform.rotation);
        Instantiate(Bullet, Muzzle_Two.transform.position, Muzzle_Two.transform.rotation);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if ((collider.tag == "Enemy") || (collider.tag == "EnemyBullet") || (collider.tag == "Bomber"))
        {
            playerHealth -= 10;
        }
    }

    IEnumerator teleportAnimation()
    {
        for(int i = 0; i < tr.Length; i++)
        {
            tr[i].emitting=true;
        }
        yield return new WaitForSeconds(.1f);
        for (int i = 0; i < tr.Length; i++)
        {
            tr[i].emitting = false;
           // tr[i].Clear();
        }
        
    }

    IEnumerator drainHealth(){
        yield return new WaitForSeconds(2f);
        playerHealth -=1;
        StartCoroutine(drainHealth());
    }
}


