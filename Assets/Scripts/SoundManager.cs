using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  ���� �Ŵ���
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
