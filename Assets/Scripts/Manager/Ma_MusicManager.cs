using System.IO;
using System.Linq;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using System;
using UnityEngine;
using UnityEditor;

[System.Serializable]
struct MusicLayerItem
{
    public List<Mb_MusicLayerInstrument> instruments;
}

public class Ma_MusicManager : MonoBehaviour
{
    [Header("Settings")]

    [Range(0, 1)]
    [InfoBox("You will need to re-populate if you change this property.")]
    public float volume = 1;

    [PropertyRange(0, "GetMaxDuration")]
    public float layersDuration;

    public float fadeInDuration = 0.5f;
    public float fadeOutDuration = 0.5f;

    [ReadOnly] [SerializeField] [ShowInInspector] private AudioClip SampleClip;

    [Header("[Debug] Audio Clips")]
    [ReadOnly] [SerializeField] [ShowInInspector] private List<Mb_MusicLayerInstrument> AllAudioInstruments;
    [ReadOnly] [SerializeField] [ShowInInspector] private List<Mb_MusicLayerInstrument> BaseAudioClip;
    [ReadOnly] [SerializeField] [ShowInInspector] private List<Mb_MusicLayerInstrument> BassAudioClip;
    [ReadOnly] [SerializeField] [ShowInInspector] private List<Mb_MusicLayerInstrument> DrumsAudioClip;
    [ReadOnly] [SerializeField] [ShowInInspector] private List<Mb_MusicLayerInstrument> GuitarAudioClip;
    [ReadOnly] [SerializeField] [ShowInInspector] private List<Mb_MusicLayerInstrument> SynthAudioClip;

    [Header("[Debug] Other")]
    [ReadOnly] [SerializeField] [ShowInInspector] private List<MusicLayerItem> MusicLayers;
    private int CurrentLayerIndex = -1;

    void Start()
    {
        PlayLayer(0, true);
    }

    public void PlayLayer(int newLayerIndex, bool ignoreTimeLimit=false)
    {
        List<Mb_MusicLayerInstrument> oldSources;
        if (CurrentLayerIndex > -1)
        {
            oldSources = MusicLayers[CurrentLayerIndex].instruments;
        }
        else
        {
            oldSources = new List<Mb_MusicLayerInstrument>();
        }

        var newSources = MusicLayers[newLayerIndex].instruments;

        // Fade out those AudioSource that are not inside the new layer
        var before = oldSources.Where(p => newSources.All(p2 => p2 != p)).ToList();
        foreach (var item in before)
            item.Stop();

        // Fade in those AudioSource that are inside the current layer and not inside the old one
        var current = newSources.Where(p => oldSources.All(p2 => p2 != p)).ToList();
        foreach (var item in current)
            if (ignoreTimeLimit)
                item.PlayWithoutManagingTime();
            else
                item.Play();

        CurrentLayerIndex = newLayerIndex;
    }

    void Update()
    {
        for (int i = 0; i < 5; i++)
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
                PlayLayer(i);
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
            if (transform.childCount > 0)
            {
                EditorUtility.DisplayDialog("Warning", "Please remove children before running this tool!", "OK, I will do that");
                return;
            }
        }

        AllAudioInstruments = new List<Mb_MusicLayerInstrument>();
        BaseAudioClip = new List<Mb_MusicLayerInstrument>();
        BassAudioClip = new List<Mb_MusicLayerInstrument>();
        DrumsAudioClip = new List<Mb_MusicLayerInstrument>();
        GuitarAudioClip = new List<Mb_MusicLayerInstrument>();
        SynthAudioClip = new List<Mb_MusicLayerInstrument>();

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
                    BaseAudioClip.Add(InitAudioInstrument(fullname, clip));
                    break;
                case "bass":
                    BassAudioClip.Add(InitAudioInstrument(fullname, clip));
                    break;
                case "drums":
                    DrumsAudioClip.Add(InitAudioInstrument(fullname, clip));
                    break;
                case "guitar":
                    GuitarAudioClip.Add(InitAudioInstrument(fullname, clip));
                    break;
                case "synth":
                    SynthAudioClip.Add(InitAudioInstrument(fullname, clip));
                    break;
            }
        }

        SampleClip = BaseAudioClip.Count > 0 ? BaseAudioClip[0].audioSource.clip : null;

        MusicLayers = new List<MusicLayerItem>();
        for (int i = 0; i < 5; i++)
        {
            MusicLayerItem item = new MusicLayerItem();
            item.instruments = GetAudioSourcesForLayer(i);
            MusicLayers.Add(item);
        }
    }

    private List<Mb_MusicLayerInstrument> GetAudioSourcesForLayer(int index)
    {
        switch (index)
        {
            case 0:
                return new List<Mb_MusicLayerInstrument>()
                {
                    // Base_01
                    BaseAudioClip[0],
                    // Bass_01
                    BassAudioClip[0],
                    // Drums_01
                    DrumsAudioClip[0],
                    // Guitar_01
                    GuitarAudioClip[0],
                    // Synth_01
                    SynthAudioClip[0],
                    // Synth_02
                    SynthAudioClip[1],
                };
            case 1:
                return new List<Mb_MusicLayerInstrument>()
                {
                    // Base_01
                    BaseAudioClip[0],
                    // Bass_02
                    BassAudioClip[1],
                    // Drums_02
                    DrumsAudioClip[1],
                    // Guitar_01
                    GuitarAudioClip[0],
                    // Synth_01
                    SynthAudioClip[0],
                    // Synth_02
                    SynthAudioClip[1],
                };
            case 2:
                return new List<Mb_MusicLayerInstrument>()
                {
                    // Base_01
                    BaseAudioClip[0],
                    // Bass_03
                    BassAudioClip[2],
                    // Drums_03
                    DrumsAudioClip[2],
                    // Guitar_01
                    GuitarAudioClip[0],
                    // Synth_02
                    SynthAudioClip[1],
                    // Synth_03
                    SynthAudioClip[2],
                };
            case 3:
                return new List<Mb_MusicLayerInstrument>()
                {
                    // Base_01
                    BaseAudioClip[0],
                    // Bass_01
                    BassAudioClip[0],
                    // Bass_02
                    BassAudioClip[1],
                    // Drums_04
                    DrumsAudioClip[3],
                    // Guitar_02
                    GuitarAudioClip[1],
                    // Synth_02
                    SynthAudioClip[1],
                    // Synth_03
                    SynthAudioClip[2],
                };
            case 4:
                return new List<Mb_MusicLayerInstrument>()
                {
                    // Base_01
                    BaseAudioClip[0],
                    // Bass_04
                    BassAudioClip[3],
                    // Drums_04
                    DrumsAudioClip[3],
                    // Guitar_03
                    GuitarAudioClip[2],
                    // Synth_02
                    SynthAudioClip[1],
                    // Synth_03
                    SynthAudioClip[2],
                    // Synth_04
                    SynthAudioClip[3],
                };
            default:
                throw new IndexOutOfRangeException("Invalid layer index!");
        }
    }

    private Mb_MusicLayerInstrument InitAudioInstrument(string name, AudioClip clip)
    {
        GameObject go = new GameObject(name);
        go.transform.SetParent(transform);

        AudioSource audioSource = go.AddComponent<AudioSource>();
        audioSource.volume = volume;
        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.mute = true;

        Mb_MusicLayerInstrument instrument = go.AddComponent<Mb_MusicLayerInstrument>();
        instrument.Init(audioSource);

        AllAudioInstruments.Add(instrument);
        return instrument;
    }

    private void ClearChildren()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
            SafeDestroyGameObject(transform.GetChild(i));
    }

    private static T SafeDestroyGameObject<T>(T component) where T : Component
    {
        if (component != null)
            SafeDestroy(component.gameObject);
        return null;
    }

    private static T SafeDestroy<T>(T obj) where T : UnityEngine.Object
    {
        if (Application.isEditor)
            UnityEngine.Object.DestroyImmediate(obj);
        else
            UnityEngine.Object.Destroy(obj);
        return null;
    }

    private float GetMaxDuration()
    {
        if (SampleClip)
        {
            return SampleClip.length;
        }
        return 0.0f;
    }
#endif
}
