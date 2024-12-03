using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class game_master_script : MonoBehaviour
{
    static GameObject _player;
    public GameObject[] NPCs = new GameObject[3];
    GameObject score_value;
    public float xBorder, yBorder;
    public float rateOfEnemies = 2f;
    public int points = 3;
    string scene;
    int availableEnemys = 0;

    bool shooter = false;
    bool bomber = false;
    bool increasedPoints = false;

    public SOscore score;


    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 1){
            scene = "Moving camera scene";
            InvokeRepeating("InitialSpawnEnemy", 2f, 2f);
        } 
        else
        {
            scene = "Initial Scene";
            StartCoroutine(enemySpawnPurchasingList());
            InvokeRepeating("TimeAliveBonus", 10f, 10f);
        }
        _player = GameObject.FindWithTag("Player");
        score_value = GameObject.FindWithTag("ScoreValue");

        xBorder = _player.GetComponent<player_script>().screenBounds.x + 5;
        yBorder = _player.GetComponent<player_script>().screenBounds.y + 5;
    }

    // Update is called once per frame
    void Update()
    {
        score_value.GetComponent<TMP_Text>().text = score.value.ToString();

        if(Input.GetKeyDown(KeyCode.Escape)){
            SceneManager.LoadScene(scene);
        }
    }

    void InitialSpawnEnemy(){
        Vector3 pos = new Vector3(Random.Range(-xBorder, xBorder), yBorder, 1f);
        Instantiate(NPCs[1], pos, Quaternion.identity);
    }

    void MovementSpawnEnemy(int index){
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
        Instantiate(NPCs[index], pos, Quaternion.identity, gameObject.transform);
    }

    public void IncreaseScore(int valueToAdd) {
        score.value += valueToAdd;

        if (score.value > 3000 && !bomber)
        {
            availableEnemys = 2;
            MovementSpawnEnemy(2);
            bomber = true;
        }
        else if (score.value > 2500 && !shooter)
        {
            availableEnemys = 1;
            MovementSpawnEnemy(1);
            shooter = true;
        }
        else if (score.value > 500 && increasedPoints && score.value % 500 == 0)
        {
            rateOfEnemies -= 0.1f;
            increasedPoints = false;
        }
        else if (score.value > 500 && !increasedPoints && score.value % 500 == 0)
        {
            points++;
            increasedPoints = true;
        }
    }

    void TimeAliveBonus()
    {
        IncreaseScore(10);
    }

    IEnumerator enemySpawnPurchasingList()
    {
        /*this is probably the most complicated code I'm going to write for this whole project.
        What I want to do is have a list of my enemies and that the game will pick from that list of enemies
        in a draft pick sort of system. I'm going to give this function an amount of points it can "buy"
        enemies for. It will randomly pick an enemy and evaluate if it can buy it and then flip a coin unti 
        it purchases enough enemies to finish the list. Here's the price breakdown rq:
        Runner - 1
        Shooter - 2
        Bomber - 3
        This will result in a lot of runners but that's kind of what they're there for. If I have a variable to increase
        the point buy, this gives me another knob to twist for balance instead of increased enemy rate.
         */
        yield return new WaitForSeconds(rateOfEnemies);

        int spendingPoints = points;

        while (spendingPoints > 0)
        {
            int browsing = Random.Range(0, availableEnemys+1)+1;
            if (spendingPoints - browsing >= 0) {
                if(Random.Range(0, 2) == 0)
                {
                    spendingPoints -= browsing;
                    MovementSpawnEnemy(browsing - 1);
                }
            }
        }
        StartCoroutine(enemySpawnPurchasingList());
    }
}
