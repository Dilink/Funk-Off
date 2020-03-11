using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

public class Ma_MusicManager : MonoBehaviour
{
    [Header("Audio Clips")]
    public AudioClip[] BaseClips;
    public AudioClip[] BassClips;
    public AudioClip[] DrumsClips;
    public AudioClip[] GuitarClips;
    public AudioClip[] SynthClips;

    [Header("Settings")]
    [Range(0, 1)]
    public float volume = 1;

    [PropertyRange(0, "GetMaxDuration")]
    public float duration;

    [Header("Debug")]
    [ReadOnly] public List<AudioSource> BaseAudioSource;
    [ReadOnly] public List<AudioSource> BassAudioSource;
    [ReadOnly] public List<AudioSource> DrumsAudioSource;
    [ReadOnly] public List<AudioSource> GuitarAudioSource;
    [ReadOnly] public List<AudioSource> SynthAudioSource;
    [ReadOnly] public List<AudioSource> AllAudioSources;

    void Awake()
    {
        foreach (var e in AllAudioSources)
        {
            e.volume = volume;
            e.Play();
        }

        foreach (var e in BaseAudioSource)
            e.mute = false;
    }

    [Button(ButtonSizes.Medium), GUIColor(0.89f, 0.14f, 0.14f)]
    private void Populate()
    {
        ClearChildren();

        AllAudioSources = new List<AudioSource>();

        // Base
        BaseAudioSource = new List<AudioSource>();
        for (int i = 0; i < BaseClips.Length; i++)
            BaseAudioSource.Add(InitAudioSource("Base_" + (i + 1), BaseClips[i]));
        // Bass
        BassAudioSource = new List<AudioSource>();
        for (int i = 0; i < BassClips.Length; i++)
            BassAudioSource.Add(InitAudioSource("Bass_" + (i + 1), BassClips[i]));
        // Drums
        DrumsAudioSource = new List<AudioSource>();
        for (int i = 0; i < DrumsClips.Length; i++)
            DrumsAudioSource.Add(InitAudioSource("Drums_" + (i + 1), DrumsClips[i]));
        // Guitar
        GuitarAudioSource = new List<AudioSource>();
        for (int i = 0; i < GuitarClips.Length; i++)
            GuitarAudioSource.Add(InitAudioSource("Guitar_" + (i + 1), GuitarClips[i]));
        // Synth
        SynthAudioSource = new List<AudioSource>();
        for (int i = 0; i < SynthClips.Length; i++)
            SynthAudioSource.Add(InitAudioSource("Synth_" + (i + 1), SynthClips[i]));
    }

    private AudioSource InitAudioSource(string name, AudioClip clip)
    {
        GameObject go = new GameObject(name);
        go.transform.SetParent(transform);

        AudioSource audioSource = go.AddComponent<AudioSource>();
        audioSource.volume = volume;
        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.mute = true;
        
        AllAudioSources.Add(audioSource);
        return audioSource;
    }

    private void ClearChildren()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
            SafeDestroyGameObject(transform.GetChild(i));
    }

    private static T SafeDestroy<T>(T obj) where T : UnityEngine.Object
    {
        if (Application.isEditor)
            UnityEngine.Object.DestroyImmediate(obj);
        else
            UnityEngine.Object.Destroy(obj);
        return null;
    }
    private static T SafeDestroyGameObject<T>(T component) where T : Component
    {
        if (component != null)
            SafeDestroy(component.gameObject);
        return null;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            PlayLayer(1);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            PlayLayer(2);
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            PlayLayer(3);
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            PlayLayer(4);
        else if (Input.GetKeyDown(KeyCode.Alpha5))
            PlayLayer(5);
    }

    private void PlayLayer(int index)
    {
        switch (index)
        {
            case 1:
                // Bass_01
                BassAudioSource[0].mute = false;
                // Drums_01
                DrumsAudioSource[0].mute = false;
                // Guitar_01
                GuitarAudioSource[0].mute = false;
                // Synth_01
                SynthAudioSource[0].mute = false;
                // Synth_02
                SynthAudioSource[1].mute = false;
                break;
            case 2:
                // Bass_02
                BassAudioSource[1].mute = false;
                // Drums_02
                DrumsAudioSource[1].mute = false;
                // Guitar_01
                GuitarAudioSource[0].mute = false;
                // Synth_01
                SynthAudioSource[0].mute = false;
                // Synth_02
                SynthAudioSource[1].mute = false;
                break;
            case 3:
                // Bass_03
                BassAudioSource[2].mute = false;
                // Drums_03
                DrumsAudioSource[2].mute = false;
                // Guitar_01
                GuitarAudioSource[0].mute = false;
                // Synth_02
                SynthAudioSource[1].mute = false;
                // Synth_03
                SynthAudioSource[2].mute = false;
                break;
            case 4:
                // Bass_01
                BassAudioSource[0].mute = false;
                // Bass_02
                BassAudioSource[1].mute = false;
                // Drums_04
                DrumsAudioSource[3].mute = false;
                // Guitar_02
                GuitarAudioSource[1].mute = false;
                // Synth_02
                SynthAudioSource[1].mute = false;
                // Synth_03
                SynthAudioSource[2].mute = false;
                break;
            case 5:
                // Bass_04
                BassAudioSource[3].mute = false;
                // Drums_04
                DrumsAudioSource[3].mute = false;
                // Guitar_03
                GuitarAudioSource[2].mute = false;
                // Synth_02
                SynthAudioSource[1].mute = false;
                // Synth_03
                SynthAudioSource[2].mute = false;
                // Synth_04
                SynthAudioSource[3].mute = false;
                break;
            default:
                throw new IndexOutOfRangeException("Invalid layer index!");
        }
    }

    private float GetMaxDuration()
    {
        return BaseClips[0].length;
    }
}
