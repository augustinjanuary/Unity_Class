using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_script : MonoBehaviour
{
    public int jumpDistance = 50;
    public int dashCooldown = 100;
    public int shootCooldown = 10;
    public float speed = 5.0f;

    public GameObject Bullet;
    public GameObject Muzzle_One;
    public GameObject Muzzle_Two;

    public float angle;

    Vector3 direction;
    Vector3 mousePos;
    Vector3 object_pos;
    Vector3 screenBounds;



    // Start is called before the first frame update
    void Start()
    {
        dashCooldown = 0;
        CalculateScreenBoundaries();


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

        transPos.x = Mathf.Clamp(transPos.x, screenBounds.x * -1, screenBounds.x);
        transPos.y = Mathf.Clamp(transPos.y, screenBounds.y * -1, screenBounds.y);

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
            
            transform.position = transform.position + direction * jumpDistance;
            dashCooldown = 100;
        }

        //Jump with mouse
        if ((Input.GetAxisRaw("Fire1") != 0) && (dashCooldown <= 0))
        {
            transform.position = transform.position + Vector3.ClampMagnitude(new Vector3(mousePos.x, mousePos.y, 0f).normalized * jumpDistance, 5.0f);
            dashCooldown = 100;
        }

        //Change camera size and recalculate boundaries
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Camera.main.orthographicSize += 1;
            CalculateScreenBoundaries();
        }

        if ((Input.GetAxisRaw("Jump") != 0) && shootCooldown <= 0){
            shootCooldown = 10;
            shoot();
        }
    }
    
    void CalculateScreenBoundaries()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 1f));
    }

    void shoot(){
        Instantiate(Bullet, Muzzle_One.transform.position, Muzzle_One.transform.rotation);
        Instantiate(Bullet, Muzzle_Two.transform.position, Muzzle_Two.transform.rotation);
    }
}


