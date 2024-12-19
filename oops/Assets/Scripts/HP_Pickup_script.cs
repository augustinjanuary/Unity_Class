using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP_Pickup_script : MonoBehaviour
{

    public SOscore playerHealth;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 1, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<player_script>().playerHealth += 10;
            other.GetComponent<AudioSource>().Play();
            Destroy(gameObject);
        }
    }
}
