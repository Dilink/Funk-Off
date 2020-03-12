using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Mb_MusicLayer : MonoBehaviour
{
    public List<AudioSource> audioSources = new List<AudioSource>();

    [ReadOnly] [SerializeField] [ShowInInspector] private bool keepFadingIn;
    [ReadOnly] [SerializeField] [ShowInInspector] private bool keepFadingOut;
    [ReadOnly] [SerializeField] [ShowInInspector] private float playbackTime;
    [ReadOnly] [SerializeField] [ShowInInspector] private bool ignoreTimeLimit;

    void Awake()
    {
        this.enabled = false;
        UpdateAudioSourcesVolume(0.0f);
        UpdateAudioSourcesMuteState(false);
    }

    void Update()
    {
        if (audioSources.Count == 0)
            return;

        FadeInProcess();
        FadeOutProcess();

        if (!ignoreTimeLimit)
        {
            if (keepFadingIn)
            {
                playbackTime += Time.deltaTime;
            }

            Ma_MusicManager musicManager = GameManager.Instance.musicManager;

            if (playbackTime >= musicManager.layersDuration)
                Stop();
        }
    }

    public void PlayWithoutManagingTime()
    {
        ignoreTimeLimit = true;
        Play();
    }

    public void Play()
    {
        this.enabled = true;
        keepFadingIn = true;
        keepFadingOut = false;
        playbackTime = 0.0f;
    }

    public void Stop()
    {
        keepFadingIn = false;
        keepFadingOut = true;
    }

    public void StartPlay()
    {
        foreach (var item in audioSources)
            item.Play();
    }

    public void AddAudioSource(AudioSource audioSource)
    {
        audioSources.Add(audioSource);
    }

    private void UpdateAudioSourcesVolume(float volume)
    {
        foreach (var item in audioSources)
            item.volume = volume;
    }

    private void UpdateAudioSourcesMuteState(bool flag)
    {
        foreach (var item in audioSources)
            item.mute = flag;
    }

    private void FadeInProcess()
    {
        if (audioSources[0].volume >= 1.0f || !keepFadingIn)
            return;

        if (audioSources[0].mute)
        {
            UpdateAudioSourcesMuteState(false);
            return;
        }

        if (audioSources[0].volume >= 1.0f)
        {
            keepFadingIn = false;
            return;
        }

        float startVolume = 1f;

        const float fadeInDuration = 0.5f;

        foreach (var item in audioSources)
        {
            item.volume += startVolume * Time.deltaTime / fadeInDuration;
        }
    }

    private void FadeOutProcess()
    {
        if (audioSources[0].mute || !keepFadingOut)
            return;

        if (audioSources[0].volume <= 0.01f)
        {
            UpdateAudioSourcesMuteState(true);
            this.enabled = false;
            keepFadingOut = false;
            return;
        }

        const float fadeOutDuration = 0.5f;

        foreach (var item in audioSources)
        {
            float startVolume = item.volume;

            item.volume -= startVolume * Time.deltaTime / fadeOutDuration;
        }
    }
}
