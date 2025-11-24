using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BGManager : MonoBehaviour
{
    public static BGManager instance;
    AudioSource audioSource;
    [SerializeField] private float maxAudioV = 0.5f;
    [SerializeField] public float duration = 2f;

    [Serializable]
    public struct MusicList
    {
        public AudioClip music;
        public string tag;
    }

    public MusicList[] musics;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        ChangeMusic("Default");
    }

    public void ChangeMusic(string musicTag)
    {
        foreach(MusicList music in musics)
        {
            if(music.tag == musicTag)
            {
                StartCoroutine(ChangeMusic(music));
            }
        }
    }

    private IEnumerator ChangeMusic(MusicList music)
    {
        float startVolume = audioSource.volume;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0, time / duration);
            yield return null;
        }

        audioSource.volume = 0;
        audioSource.clip = music.music;
        audioSource.Play();


        startVolume = audioSource.volume;
        time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, maxAudioV, time / duration);
            yield return null;
        }

        audioSource.volume = maxAudioV;
    }


}
