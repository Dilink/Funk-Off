using System.IO;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using System;
using UnityEngine;
using UnityEditor;


[System.Serializable]
public class MusicLayer
{
    public List<AudioSource> audioSources;
    public List<Coroutine> runningCoroutines;

    [ReadOnly]
    public bool keepFadingIn;
    public bool keepFadingOut;

    public MusicLayer(List<AudioSource> audioSources)
    {
        this.audioSources = audioSources;
        this.runningCoroutines = new List<Coroutine>();
    }

    public void Play()
    {
        GameManager.Instance.musicManager.PlayLayerProcess(this);
    }

    public void PlayAll()
    {
        foreach (AudioSource e in audioSources)
            GameManager.Instance.musicManager.FadeIn(e, this);
    }

    public void StopAll()
    {
        foreach (AudioSource e in audioSources)
            GameManager.Instance.musicManager.FadeOut(e, this);
    }
}


public class Ma_MusicManager : MonoBehaviour
{
    [Header("Settings")]

    [Range(0, 1)]
    public float volume = 1;

    public float fadeInDuration = 0.5f;
    public float fadeOutDuration = 0.5f;

    [PropertyRange(0, "GetMaxDuration")]
    public float layersDuration;

    [Header("[Debug] Audio Clips")]
    [ReadOnly] public AudioClip[] BaseClips;
    [ReadOnly] public AudioClip[] BassClips;
    [ReadOnly] public AudioClip[] DrumsClips;
    [ReadOnly] public AudioClip[] GuitarClips;
    [ReadOnly] public AudioClip[] SynthClips;

    [Header("[Debug] Audio Sources")]
    [ReadOnly] public List<AudioSource> BaseAudioSource;
    [ReadOnly] public List<AudioSource> BassAudioSource;
    [ReadOnly] public List<AudioSource> DrumsAudioSource;
    [ReadOnly] public List<AudioSource> GuitarAudioSource;
    [ReadOnly] public List<AudioSource> SynthAudioSource;

    [Header("[Debug] Other")]
    [ReadOnly] public List<AudioSource> AllAudioSources;
    [ReadOnly] public List<MusicLayer> musicLayers;

    void Awake()
    {
        for (int i = 0; i <= 5; i++)
            musicLayers.Add(new MusicLayer(GetAudioSourcesForLayer(i)));

        musicLayers[0].PlayAll();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            musicLayers[1].Play();
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            musicLayers[2].Play();
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            musicLayers[3].Play();
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            musicLayers[4].Play();
        else if (Input.GetKeyDown(KeyCode.Alpha5))
            musicLayers[5].Play();

        if (Input.GetKeyDown(KeyCode.Alpha6))
            musicLayers[1].StopAll();
        else if (Input.GetKeyDown(KeyCode.Alpha7))
            musicLayers[2].StopAll();
        else if (Input.GetKeyDown(KeyCode.Alpha8))
            musicLayers[3].StopAll();
        else if (Input.GetKeyDown(KeyCode.Alpha9))
            musicLayers[4].StopAll();
        else if (Input.GetKeyDown(KeyCode.Alpha0))
            musicLayers[5].StopAll();
    }

    private List<AudioSource> GetAudioSourcesForLayer(int index)
    {
        switch (index)
        {
            case 0:
                return new List<AudioSource>()
                {
                    // Base_01
                    BaseAudioSource[0],
                };
            case 1:
                return new List<AudioSource>()
                {
                    // Bass_01
                    BassAudioSource[0],
                    // Drums_01
                    DrumsAudioSource[0],
                    // Guitar_01
                    GuitarAudioSource[0],
                    // Synth_01
                    SynthAudioSource[0],
                    // Synth_02
                    SynthAudioSource[1],
                };
            case 2:
                return new List<AudioSource>()
                {
                    // Bass_02
                    BassAudioSource[1],
                    // Drums_02
                    DrumsAudioSource[1],
                    // Guitar_01
                    GuitarAudioSource[0],
                    // Synth_01
                    SynthAudioSource[0],
                    // Synth_02
                    SynthAudioSource[1],
                };
            case 3:
                return new List<AudioSource>()
                {
                    // Bass_03
                    BassAudioSource[2],
                    // Drums_03
                    DrumsAudioSource[2],
                    // Guitar_01
                    GuitarAudioSource[0],
                    // Synth_02
                    SynthAudioSource[1],
                    // Synth_03
                    SynthAudioSource[2],
                };
            case 4:
                return new List<AudioSource>()
                {
                    // Bass_01
                    BassAudioSource[0],
                    // Bass_02
                    BassAudioSource[1],
                    // Drums_04
                    DrumsAudioSource[3],
                    // Guitar_02
                    GuitarAudioSource[1],
                    // Synth_02
                    SynthAudioSource[1],
                    // Synth_03
                    SynthAudioSource[2],
                };
            case 5:
                return new List<AudioSource>()
                {
                    // Bass_04
                    BassAudioSource[3],
                    // Drums_04
                    DrumsAudioSource[3],
                    // Guitar_03
                    GuitarAudioSource[2],
                    // Synth_02
                    SynthAudioSource[1],
                    // Synth_03
                    SynthAudioSource[2],
                    // Synth_04
                    SynthAudioSource[3],
                };
            default:
                throw new IndexOutOfRangeException("Invalid layer index!");
        }
    }
#if UNITY_EDITOR

    [Button(ButtonSizes.Medium), GUIColor(0.89f, 0.14f, 0.14f)]
    private void Populate()
    {
        bool isInPrefab = PrefabUtility.GetPrefabInstanceStatus(gameObject) == PrefabInstanceStatus.Connected;
        if (!isInPrefab)
        {
            ClearChildren();
        }
        else
        {
            if (transform.childCount > 0) {
                EditorUtility.DisplayDialog("Warning", "Please remove children before running this tool!", "OK, I will do that");
                return;
            }
        }

        AllAudioSources = new List<AudioSource>();
        BaseAudioSource = new List<AudioSource>();
        BassAudioSource = new List<AudioSource>();
        DrumsAudioSource = new List<AudioSource>();
        GuitarAudioSource = new List<AudioSource>();
        SynthAudioSource = new List<AudioSource>();

        string[] guids = AssetDatabase.FindAssets("t:AudioClip", new[] { "Assets/Sounds/Music" });
        foreach (var i in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(i);
            string fullname = Path.GetFileNameWithoutExtension(path);
            string name = fullname.Split('_')[0].ToLower();

            AudioClip clip = AssetDatabase.LoadAssetAtPath<AudioClip>(path);
            switch (name)
            {
                case "base":
                    BaseAudioSource.Add(InitAudioSource(fullname, clip));
                    break;
                case "bass":
                    BassAudioSource.Add(InitAudioSource(fullname, clip));
                    break;
                case "drums":
                    DrumsAudioSource.Add(InitAudioSource(fullname, clip));
                    break;
                case "guitar":
                    GuitarAudioSource.Add(InitAudioSource(fullname, clip));
                    break;
                case "synth":
                    SynthAudioSource.Add(InitAudioSource(fullname, clip));
                    break;
            }
        }
    }
#endif
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

    private float GetMaxDuration()
    {
        return BaseClips[0].length;
    }
   

    #region ROUTINES
    public Coroutine PlayLayerProcess(MusicLayer layer)
    {
        //Debug.LogError("size: " + layer.runningCoroutines.Count);
        foreach (Coroutine coroutine in layer.runningCoroutines)
        {
            //Debug.LogError("will stop coroutine: " + coroutine);
            StopCoroutine(coroutine);
        }
        layer.runningCoroutines.Clear();

        Coroutine co = StartCoroutine(PlayLayerProcessRoutine(layer));
        layer.runningCoroutines.Add(co);
        return co;
    }

    public Coroutine FadeIn(AudioSource audioSource, MusicLayer layer)
    {
        return StartCoroutine(FadeInRoutine(audioSource, layer));
    }

    public Coroutine FadeOut(AudioSource audioSource, MusicLayer layer)
    {
        return StartCoroutine(FadeOutRoutine(audioSource, layer));
    }

    private IEnumerator PlayLayerProcessRoutine(MusicLayer layer)
    {
        foreach (var e in layer.audioSources)
            layer.runningCoroutines.Add(FadeIn(e, layer));

        yield return new WaitForSeconds(layersDuration);

        foreach (var e in layer.audioSources)
            layer.runningCoroutines.Add(FadeOut(e, layer));
    }

    private IEnumerator FadeInRoutine(AudioSource audioSource, MusicLayer layer)
    {
        layer.keepFadingIn = true;
        layer.keepFadingOut = false;

        float startVolume = 1f;

        audioSource.mute = false;
        //audioSource.volume = 0;

        while (audioSource.volume < 1.0f && layer.keepFadingIn)
        {
            audioSource.volume += startVolume * Time.deltaTime / fadeInDuration;
            yield return null;
        }

        audioSource.volume = 1f;
    }

    private IEnumerator FadeOutRoutine(AudioSource audioSource, MusicLayer layer)
    {
        layer.keepFadingIn = false;
        layer.keepFadingOut = true;

        float startVolume = audioSource.volume;

        while (audioSource.volume > 0.01f && layer.keepFadingOut)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeOutDuration;
            yield return null;
        }

        //audioSource.volume = 0f;
        audioSource.mute = true;
    }
    #endregion
}
