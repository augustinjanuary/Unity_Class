using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_script : MonoBehaviour
{ 
    public int jumpDistance = 50;
    public int cooldown = 100;
    public float speed = 5.0f;

    float angle;

    Vector3 direction;
    Vector3 mousePos;
    Vector3 object_pos;
    public Vector3 screenBounds;
    


    // Start is called before the first frame update
    void Start()
    {
        cooldown = 0;
        CalculateScreenBoundaries();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if(cooldown < 0){
            cooldown = 0;
        } 
        else if (cooldown != 0){
            cooldown--;
        } 

        mousePos = Input.mousePosition;
        direction = new Vector3(speed * Input.GetAxisRaw("Horizontal") * Time.deltaTime, speed * Input.GetAxisRaw("Vertical") * Time.deltaTime);
        transform.position += direction;

        Vector3 transPos = transform.position;

        transPos.x = Mathf.Clamp(transPos.x, screenBounds.x*-1, screenBounds.x);
        transPos.y = Mathf.Clamp(transPos.y, screenBounds.y*-1, screenBounds.y);

        transform.position = transPos;

        mousePos.z = 5.23f; //The distance between the camera and object

	    object_pos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - object_pos.x;
	    mousePos.y = mousePos.y - object_pos.y;

        angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
	    transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));



        if ((Input.GetAxisRaw("Fire3") != 0) && (direction != Vector3.zero) && (cooldown == 0))
        {
            Debug.Log(direction);
            Debug.Log(direction * jumpDistance);
            transform.position = transform.position + direction * jumpDistance;
            cooldown = 100;
        }

        else if ((Input.GetAxisRaw("Fire1") != 0) && (cooldown == 0))
        {
            Debug.Log(Vector3.ClampMagnitude(new Vector3(mousePos.x, mousePos.y, 0f).normalized * jumpDistance, 3.5f));
            transform.position = transform.position + Vector3.ClampMagnitude(new Vector3(mousePos.x, mousePos.y, 0f).normalized * jumpDistance, 5.0f);
            cooldown = 100;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Camera.main.orthographicSize += 1;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Camera.main.orthographicSize += 1;
            CalculateScreenBoundaries();
        }

    }

    void CalculateScreenBoundaries()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 1f));
    }
}


