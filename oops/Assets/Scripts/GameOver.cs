using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public SOscore score;

    GameObject Below3K, Above3k, scoreValue;

    // Start is called before the first frame update
    void Start()
    {
        Below3K = GameObject.FindWithTag("Below3K");
        Above3k = GameObject.FindWithTag("Above3K");
        scoreValue = GameObject.FindWithTag("ScoreValue");

        if(score.value >= 10000)
        {
            Above3k.GetComponent<TMP_Text>().enabled = true;
        }
        else
        {
            Below3K.GetComponent<TMP_Text>().enabled = true;
        }

        scoreValue.GetComponent<TMP_Text>().text = score.value.ToString();
    }
}
