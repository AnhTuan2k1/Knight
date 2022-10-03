using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public GameObject btnPause;
    public GameObject btnUnPause;
    //public AudioClip audioClipPaused;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Destroy(this);
            return;
        }

        DontDestroyOnLoad(this);
        foreach (Sound s in sounds)
        {
            s.audioSource = gameObject.AddComponent<AudioSource>();
            s.audioSource.clip = s.clip;

            s.audioSource.volume = s.volume;
            s.audioSource.pitch = s.pitch;
            s.audioSource.spatialBlend = s.spatialBlend;
            s.audioSource.loop = s.loop;
            s.audioSource.playOnAwake = false;
        }
    }

    private void Start()
    {
        Play("Maybe", transform.position/*GameObject.FindWithTag("Player").transform.position*/);
    }

    public void Play(string name, Vector3 position)
    {
        Sound s = Array.Find(sounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Sound " + name + ": not found");
            return;
        }

        if (s.audioSource.isVirtual)
        {
            AudioSource audio = gameObject.AddComponent<AudioSource>();
            audio.clip = s.audioSource.clip;
            audio.volume = s.audioSource.volume;
            audio.pitch = s.audioSource.pitch;
            audio.spatialBlend = s.audioSource.spatialBlend;
            audio.loop = false;

            audio.transform.position = position;
            audio.Play();
            Destroy(audio, audio.clip.length);
        }
        else
        {
            s.audioSource.transform.position = position;
            s.audioSource.Play();
        }

    }

    public void Pause(string name)
    {
        Sound s = Array.Find(sounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Sound " + name + ": not found");
            return;
        }

        btnPause.SetActive(false);
        btnUnPause.SetActive(true);
        s.audioSource.Pause();
        //audioClipPaused = s.audioSource.clip;
    }

    public void UnPause(string name)
    {
        Sound s = Array.Find(sounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Sound " + name + ": not found");
            return;
        }

        btnPause.SetActive(true);
        btnUnPause.SetActive(false);
        s.audioSource.UnPause();
        //audioClipPaused = null;
    }
}
