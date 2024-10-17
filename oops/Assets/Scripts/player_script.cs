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


    // Start is called before the first frame update
    void Start()
    {
        cooldown = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        float hori = Input.GetAxisRaw("Horizontal");
        float vert = Input.GetAxisRaw("Vertical");
        
        
        

        if(cooldown < 0){
            cooldown = 0;
        } 
        else if (cooldown != 0){
            cooldown--;
        } 

        mousePos = Input.mousePosition;
        direction = new Vector3(speed * hori * Time.deltaTime, speed * vert * Time.deltaTime);
        transform.position += direction;

        float posX = Mathf.Clamp(transform.position.x, 0, Screen.width/2);
        float posY = Mathf.Clamp(transform.position.y, 0, Screen.height/2);

        transform.position = Camera.main.WorldToScreenPoint((new Vector3(posX, posY, 1f));

        mousePos.z = 5.23f; //The distance between the camera and object
	    object_pos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - object_pos.x;
	    mousePos.y = mousePos.y - object_pos.y;

        angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
	    transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if((Input.GetAxisRaw("Fire3") != 0) && (direction != Vector3.zero) && (cooldown == 0)){
            transform.position = transform.position + direction * jumpDistance;
            cooldown = 100;
        }

        if(Input.GetAxisRaw("Jump") != 0){
            Debug.Log(Camera.main.WorldToScreenPoint(transform.position));
        }
    }
}


