using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class camera_script : MonoBehaviour
{
    public GameObject player;
    public GameObject backgroundTransform;
    public float BorderHeight = 2f;
    public float BorderWidth = 2f;


    void Start(){
       Transform backgroundTransform = gameObject.GetComponentInParent<Transform>(); 
       Debug.Log(gameObject.GetComponent<Camera>().orthographicSize - (backgroundTransform.transform.localScale.y ));
    }

    void Update(){
        transform.position = new Vector3(Mathf.Clamp(player.transform.position.x, -BorderWidth, BorderWidth), Mathf.Clamp(player.transform.position.y, -BorderHeight, BorderHeight), -3f);
    }
}
