using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Events;

public class VideoManager : MonoBehaviour
{
    public UnityEvent callbacks;

    private VideoPlayer video;

    private void Start()
    {
        video = GetComponent<VideoPlayer>();
        video.loopPointReached += CheckOver;
    }

    private void CheckOver(VideoPlayer vp)
    {
        callbacks.Invoke();
    }
}
