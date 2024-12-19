using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class fadeInOut : MonoBehaviour
{
    public float timeForFade;

    public bool fadeOut = false;
    public bool fadeIn = true;

    CanvasGroup canvasGroup;
    //Fade in manages entering and exiting scenes so it's convenient for music to be here
    GameObject audioManager;
    //BuildIndex
    int sceneToTransition;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.FindWithTag("Audio");
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1.0f;
        audioManager.GetComponent<AudioManager>().checkScene();
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeIn && !fadeOut)
        {
            canvasGroup.alpha -= timeForFade * Time.deltaTime;
            if (canvasGroup.alpha <= 0)
            {
                fadeIn = false;
            }
        }
        if (fadeOut)
        {
            canvasGroup.alpha += timeForFade * Time.deltaTime;
            if (canvasGroup.alpha >= 1)
            {
                fadeOut = false;
                SceneManager.LoadScene(sceneToTransition);
            }
        }
    }

    public void FadeOut(int scene)
    {
        Debug.Log(scene);
        fadeOut = true;
        sceneToTransition = scene;
    }
}
