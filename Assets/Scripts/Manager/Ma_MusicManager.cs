using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ma_MusicManager : MonoBehaviour
{
    public AudioClip Base_01;
    public AudioClip[] Bass;
    public AudioClip[] Drums;
    public AudioClip[] Guitar;
    public AudioClip[] Synth;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        audioSource.clip = Base_01;
        audioSource.Play();
    }

    void Update()
    {
        
    }
}
