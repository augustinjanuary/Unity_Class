using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class game_master_script : MonoBehaviour
{
    static GameObject _player;
    public GameObject NPC;
    GameObject score_value;
    public float xBorder, yBorder;
    public float score = 0;
    string scene;

    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().name == "Initial Scene"){
            scene = "Moving camera scene";
            InvokeRepeating("InitialSpawnEnemy", 2f, 2f);
        } 
        else
        {
            scene = "Initial Scene";
            InvokeRepeating("MovementSpawnEnemy", 2f, 2f);

        }
        _player = GameObject.FindWithTag("Player");
        score_value = GameObject.FindWithTag("ScoreValue");

        xBorder = _player.GetComponent<player_script>().screenBounds.x + 5;
        yBorder = _player.GetComponent<player_script>().screenBounds.y + 5;
    }

    // Update is called once per frame
    void Update()
    {
        score_value.GetComponent<TMP_Text>().text = score.ToString();

        if(Input.GetKeyDown(KeyCode.Escape)){
            SceneManager.LoadScene(scene);
        }
    }

    void InitialSpawnEnemy(){
        Vector3 pos = new Vector3(Random.Range(-xBorder, xBorder), yBorder, 1f);
        Instantiate(NPC, pos, Quaternion.identity);
    }

    void MovementSpawnEnemy(){
        Vector3 pos = Vector3.zero;
        switch(Random.Range(0, 3)){
            case 0:
                pos = new Vector3(-xBorder, Random.Range(-yBorder, yBorder), 1f);
                break;
            case 1:
                pos = new Vector3(Random.Range(-xBorder, xBorder), yBorder, 1f);
                break;
            case 2:
                pos = new Vector3(xBorder, Random.Range(-yBorder, yBorder), 1f);
                break;
            case 3:
                pos = new Vector3(Random.Range(-xBorder, xBorder), -yBorder, 1f);
                break;
        }
        Instantiate(NPC, pos, Quaternion.identity);
    }

    void IncreaseScore(int value){
        score += value;
    }
}
