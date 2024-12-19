using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class videoCamera : MonoBehaviour
{
    VideoPlayer _vPlayer;
    GameObject Fade;

    // Start is called before the first frame update
    void Start()
    {
        _vPlayer = GetComponent<VideoPlayer>();
        Fade = GameObject.FindWithTag("Fade");

        _vPlayer.loopPointReached += EndReached;
    }

    private void Update()
    {
        if(Input.GetAxisRaw("Fire1") != 0)
        {
            Fade.GetComponent<fadeInOut>().FadeOut(2);
        }
    }

    void EndReached(UnityEngine.Video.VideoPlayer player)
    {
        Fade.GetComponent<fadeInOut>().FadeOut(2);
    }
}
