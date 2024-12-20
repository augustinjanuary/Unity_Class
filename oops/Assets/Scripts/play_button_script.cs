using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class play_button_script : MonoBehaviour
{    
    [SerializeField]
    public int sceneIndex;
    public SOscore score;

    // Start is called before the first frame update
    void Start()
    {
        Button btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TaskOnClick(){
        GetComponent<AudioSource>().Play();
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            score.value = 0;
            score.health = 100;
        }
        SceneManager.LoadScene(sceneIndex);

    }
}
