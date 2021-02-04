using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    // Start is called before the first frame update
    void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, "final.mp4");
        videoPlayer.Play();
    }


}
