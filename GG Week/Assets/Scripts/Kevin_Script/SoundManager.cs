using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class SoundManager : MonoBehaviour
{
    public Sound[] son;

    void Start()
    {
        Play("main");
    }

    void Awake()
    {
        foreach(Sound x in son)
        {
            x.source = gameObject.AddComponent<AudioSource>();
            x.source.clip = x.clip;
            x.source.volume = x.volume;
            x.source.loop = x.loop;
        }
    }

    public void Play(string name)
    {
        Sound x = Array.Find(son, sound => sound.name == name);
        x.source.Play();
    }

    //FindObjectOfType<SoundManager>().Play("");
}
