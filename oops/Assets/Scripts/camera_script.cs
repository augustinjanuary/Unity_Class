using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class camera_script : MonoBehaviour
{
    public GameObject player;
    public float yBorderHeight = 3.9f;

    void Start(){
    }

    void Update(){
        transform.position = new Vector3(Mathf.Clamp(player.transform.position.x, -5.9f, 5.9f), Mathf.Clamp(player.transform.position.y, -yBorderHeight, yBorderHeight), 0);
    }
}
