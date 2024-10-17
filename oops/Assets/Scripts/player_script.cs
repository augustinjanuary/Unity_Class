using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_script : MonoBehaviour
{ 
    float speed = 5.0f;
    float angle;
    Vector3 mousePos;
    Vector3 object_pos;
    Vector3 direction;
    int jumpDistance = 50;
    int cooldown = 100;

    // Start is called before the first frame update
    void Start()
    {
        cooldown = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(cooldown != 0){
            cooldown--;
        } 
        else if (cooldown < 0){
            cooldown = 0;
        } 

        mousePos = Input.mousePosition;
        direction = new Vector3(speed * Input.GetAxisRaw("Horizontal") * Time.deltaTime, speed * Input.GetAxisRaw("Vertical") * Time.deltaTime);
        transform.position += direction;

        mousePos.z = 5.23f; //The distance between the camera and object
	    object_pos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - object_pos.x;
	    mousePos.y = mousePos.y - object_pos.y;

        if((Input.GetAxisRaw("Fire3") != 0) && (direction != Vector3.zero) && (cooldown == 0)){
            transform.position = transform.position + direction * jumpDistance;
            cooldown = 100;
        }

        angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
	    transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

    }
}

