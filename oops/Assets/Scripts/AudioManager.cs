using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource[] musicSources = new AudioSource[2];

    int toggle;
    public int trackList = 0;
    public double goalTime;
    public double musicDuration;
    public AudioClip splashScreen, initialScene, movingScene, currentClip;
    
    // Start is called before the first frame update

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        currentClip = splashScreen;
    }

    // Update is called once per frame
    void Update()
    {
        if(AudioSettings.dspTime > goalTime - 1) 
        {
            PlayScheduledClip();
        }

        if(Input.GetKeyUp(KeyCode.Escape))
        {
            trackList++;
            changeTrack(trackList);
        }
    }

    private void PlayScheduledClip()
    {
        goalTime = AudioSettings.dspTime + 1;

        musicSources[toggle].clip = currentClip;
        musicSources[toggle].PlayScheduled(goalTime);

        musicDuration = (double)currentClip.samples / currentClip.frequency;
        goalTime = goalTime + musicDuration;

        toggle = 1 - toggle;
    }

    public void checkScene()
    {
        //1 is initial, 2/3 is moving, else que up menu theme
        changeTrack(SceneManager.GetActiveScene().buildIndex);
    }

    void changeTrack(int index)
    {
        if (index == 1)
        {
            currentClip = initialScene;
        }
        else if (index == 2 || index == 3)
        {
            currentClip = movingScene;
        }
        else
        {
            currentClip = splashScreen;
        }
    }
}
