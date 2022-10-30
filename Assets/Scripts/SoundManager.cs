using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  사운드 매니저
/// </summary>

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;

    public AudioSource bgm;
    public AudioSource dropSound;
    public AudioSource lineClear;

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
    }

}
