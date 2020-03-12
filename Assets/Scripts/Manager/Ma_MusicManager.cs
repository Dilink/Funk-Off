using System.IO;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using System;
using UnityEngine;
using UnityEditor;

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
    [ReadOnly] [SerializeField] [ShowInInspector] private List<AudioClip> BaseAudioClip;
    [ReadOnly] [SerializeField] [ShowInInspector] private List<AudioClip> BassAudioClip;
    [ReadOnly] [SerializeField] [ShowInInspector] private List<AudioClip> DrumsAudioClip;
    [ReadOnly] [SerializeField] [ShowInInspector] private List<AudioClip> GuitarAudioClip;
    [ReadOnly] [SerializeField] [ShowInInspector] private List<AudioClip> SynthAudioClip;

    [Header("[Debug] Other")]
    [ReadOnly] [SerializeField] [ShowInInspector] [InlineEditor] private List<Mb_MusicLayer> MusicLayers;

    void Awake()
    {
        if (MusicLayers == null || MusicLayers.Count == 0 || MusicLayers[0] == null)
        {
            throw new InvalidDataException("No MusicLayer found, you perhaps have to populate the MusicManager.");
        }

        MusicLayers[0].PlayWithoutManagingTime();

        for (int i = 1; i <= 5; i++)
            MusicLayers[i].StartPlay();
    }

    public void PlayerLayer(int layerIndex)
    {
        if (layerIndex < 0 && layerIndex >= MusicLayers.Count)
        {
            throw new IndexOutOfRangeException("Invalid layer index!");
        }

        MusicLayers[layerIndex].Play();
    }

    void Update()
    {
        for (int i = 0; i < 5; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
                MusicLayers[i + 1].Play();
            else if (Input.GetKeyDown(KeyCode.Alpha6 + i) || Input.GetKeyDown(KeyCode.Alpha0))
                MusicLayers[i + 1].Stop();
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
            if (transform.childCount > 0)
            {
                EditorUtility.DisplayDialog("Warning", "Please remove children before running this tool!", "OK, I will do that");
                return;
            }
        }

        MusicLayers = new List<Mb_MusicLayer>();
        BaseAudioClip = new List<AudioClip>();
        BassAudioClip = new List<AudioClip>();
        DrumsAudioClip = new List<AudioClip>();
        GuitarAudioClip = new List<AudioClip>();
        SynthAudioClip = new List<AudioClip>();

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
                    BaseAudioClip.Add(clip);
                    break;
                case "bass":
                    BassAudioClip.Add(clip);
                    break;
                case "drums":
                    DrumsAudioClip.Add(clip);
                    break;
                case "guitar":
                    GuitarAudioClip.Add(clip);
                    break;
                case "synth":
                    SynthAudioClip.Add(clip);
                    break;
            }
        }

        SampleClip = BaseAudioClip.Count > 0 ? BaseAudioClip[0] : null;

        for (int i = 0; i <= 5; i++)
        {
            MusicLayers.Add(InitAudioSource("Layer " + i, GetAudioClipsForLayer(i)));
        }
    }

    private List<AudioClip> GetAudioClipsForLayer(int index)
    {
        switch (index)
        {
            case 0:
                return new List<AudioClip>()
                {
                    // Base_01
                    BaseAudioClip[0],
                };
            case 1:
                return new List<AudioClip>()
                {
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
            case 2:
                return new List<AudioClip>()
                {
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
            case 3:
                return new List<AudioClip>()
                {
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
            case 4:
                return new List<AudioClip>()
                {
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
            case 5:
                return new List<AudioClip>()
                {
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

    private Mb_MusicLayer InitAudioSource(string name, List<AudioClip> audioClips)
    {
        GameObject go = new GameObject(name);
        go.transform.SetParent(transform);

        Mb_MusicLayer layer = go.AddComponent<Mb_MusicLayer>();

        foreach (var clip in audioClips)
        {
            AudioSource audioSource = go.AddComponent<AudioSource>();
            audioSource.volume = volume;
            audioSource.clip = clip;
            audioSource.loop = true;
            audioSource.mute = true;
            layer.AddAudioSource(audioSource);
        }

        return layer;
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
