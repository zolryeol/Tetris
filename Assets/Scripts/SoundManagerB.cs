using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  사운드 매니저2
/// </summary>

public class SoundManagerB : MonoBehaviour
{
    public static SoundManagerB instance = null;

    public AudioSource bgm;
    public AudioSource dropSound;
    public AudioSource lineClear;
    AudioSource comboSound;
    AudioClip[] comboSoundClip;


    public void ComboSoundPlay(int _CBCount)
    {
        comboSound.clip = comboSoundClip[_CBCount - 2];

        comboSound.Play();
    }

    void ComboSoundLoad()
    {
        for (int i = 0; i < 8; ++i)
        {
            comboSoundClip[i] = Resources.Load<AudioClip>("Sound/Combo/000" + (2 + i));
        }
        comboSoundClip[8] = Resources.Load<AudioClip>("Sound/Combo/0010");
        comboSoundClip[9] = Resources.Load<AudioClip>("Sound/Combo/0052");

    }

    private void Awake()
    {
        comboSound = gameObject.AddComponent<AudioSource>();

        comboSoundClip = new AudioClip[10];

        ComboSoundLoad();

        if (instance == null)
        {
            instance = this;
        }
    }

}
