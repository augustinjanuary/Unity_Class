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
    public int roundScore = 1;

    public int availableEnemys = 0;

    bool shooter = false;
    bool bomber = false;
    bool increasedPoints = false;

    public SOscore score;
    public GameObject fade;


    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 1){
            InvokeRepeating("InitialSpawnEnemy", 2f, 2f);
        } 
        else
        {
            StartCoroutine(enemySpawnPurchasingList());
            InvokeRepeating("TimeAliveBonus", 10f, 10f);
        }

        _player = GameObject.FindWithTag("Player");
        score_value = GameObject.FindWithTag("ScoreValue");
        fade = GameObject.FindWithTag("Fade");

        xBorder = _player.GetComponent<player_script>().screenBounds.x + 5;
        yBorder = _player.GetComponent<player_script>().screenBounds.y + 5;
    }

    // Update is called once per frame
    void Update()
    {
        score_value.GetComponent<TMP_Text>().text = score.value.ToString();

       
    }

    void InitialSpawnEnemy(){
        if(score.value >= 2000)
        {
            //Stops the player from getting hit while fading
            _player.GetComponent<BoxCollider>().enabled = false;
            //here's where I'd put my animation scene
            //IF I HAD ONE
            
            fade.GetComponent<fadeInOut>().FadeOut(3);
        }

        Vector3 pos = new Vector3(Random.Range(-xBorder, xBorder), yBorder, 1f);
        Instantiate(NPCs[1], pos, Quaternion.identity);
    }

    void MovementSpawnEnemy(int index){
        Vector3 pos = Vector3.zero;
        switch(Random.Range(0, 4)){
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

        if ((score.value) > 3000 && !bomber && shooter)
        {
            availableEnemys = 2;
            MovementSpawnEnemy(2);
            bomber = true;
            roundScore++;
        }
        else if ((score.value > 2500) && !shooter)
        {
            availableEnemys = 1;
            MovementSpawnEnemy(1);
            shooter = true;
            roundScore++;
        }
        else if (score.value > 2000 && (increasedPoints && rateOfEnemies >= 0f) && score.value >= roundScore*500)
        {
            rateOfEnemies -= 0.1f;
            increasedPoints = false;
            roundScore++;
        }
        else if (score.value > 2000 && (!increasedPoints || rateOfEnemies <= 0f) && score.value >= roundScore*500)
        {
            points++;
            increasedPoints = true;
            roundScore++;
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
            int browsing = Random.Range(0, availableEnemys+1);
            if(browsing == 2)
            {
                Debug.Log(browsing);
            }
            if (spendingPoints - (browsing + 1) >= 0) {
                if(Random.Range(0, 2) == 0)
                {
                    spendingPoints -= browsing+1;
                    MovementSpawnEnemy(browsing);
                    yield return new WaitForSeconds(Random.Range(0f, rateOfEnemies + 1));
                    
                }
            }
        }
        StartCoroutine(enemySpawnPurchasingList());
    }
}
