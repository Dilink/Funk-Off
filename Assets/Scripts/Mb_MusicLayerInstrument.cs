using Sirenix.OdinInspector;
using UnityEngine;

public class Mb_MusicLayerInstrument : MonoBehaviour
{
    [ReadOnly] [SerializeField] [ShowInInspector] private bool keepFadingIn;
    [ReadOnly] [SerializeField] [ShowInInspector] private bool keepFadingOut;
    [ReadOnly] [SerializeField] [ShowInInspector] private float playbackTime;
    [ReadOnly] [SerializeField] [ShowInInspector] private bool ignoreTimeLimit;

    public AudioSource audioSource;

    void Awake()
    {
        this.enabled = false;
        audioSource.volume = 0.0f;
        audioSource.mute = false;
    }

    public void Init(AudioSource audioSource)
    {
        this.audioSource = audioSource;
    }

    void Update()
    {
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

    private void FadeInProcess()
    {
        if (audioSource.volume >= 1.0f || !keepFadingIn)
            return;

        if (audioSource.mute)
        {
            audioSource.mute = false;
            return;
        }

        if (audioSource.volume >= 1.0f)
        {
            keepFadingIn = false;
            return;
        }

        float startVolume = 1f;

        float fadeInDuration = GameManager.Instance.musicManager.fadeInDuration;

        audioSource.volume += startVolume * Time.deltaTime / fadeInDuration;
    }

    private void FadeOutProcess()
    {
        if (audioSource.mute || !keepFadingOut)
            return;

        if (audioSource.volume <= 0.01f)
        {
            audioSource.mute = true;
            this.enabled = false;
            keepFadingOut = false;
            return;
        }

        float fadeOutDuration = GameManager.Instance.musicManager.fadeOutDuration;

        float startVolume = audioSource.volume;

        audioSource.volume -= startVolume * Time.deltaTime / fadeOutDuration;
    }
}
