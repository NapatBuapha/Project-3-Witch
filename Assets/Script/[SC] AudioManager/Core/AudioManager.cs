using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum SoundType
{
    Hit,
    Explosive,
    Player_Transform,
    Player_DeTransform,
    Player_BeastStomp,

    Spell_50Cal,
    Spell_Fireball,
    Spell_AK47Equip,
    Spell_AKBullet,
    Spell_LightnighBolt,

    Enemy_Spawn,
    Enemy_Broc_Slash,
    Enemy_Oion_Thrown,
    Enemy_Boss_Roar,
    Enemy_Mushroom_ExplosoveCharge,

    Hearth_Breaking,

    Enemy_Mushroom_Charge,
    Lever_Interact,
    Puzzle_Complete,

    Player_BeastScream
}

[RequireComponent(typeof(AudioSource)), ExecuteInEditMode]
public class AudioManager : MonoBehaviour
{
    [SerializeField] private SoundList[] soundList;
    private static AudioManager instance;
    private AudioSource audioSource;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public static void PlaySound(SoundType sound, float volume = 1)
    {
        AudioClip[] clips = instance.soundList[(int)sound].Sounds;
        AudioClip randomClip = clips[UnityEngine.Random.Range(0, clips.Length)];
        instance.audioSource.PlayOneShot(randomClip, volume);
    }

#if UNITY_EDITOR
    private void OnEnable()
    {
        string[] names = Enum.GetNames(typeof(SoundType));
        Array.Resize(ref soundList, names.Length);
        for (int i = 0; i < soundList.Length; i++)
        {
            soundList[i].name = names[i];
        }
    }
#endif

}

[Serializable]
public struct SoundList
{
    public AudioClip[] Sounds { get => sounds; }
    [HideInInspector] public string name;
    [SerializeField] private AudioClip[] sounds;
}
