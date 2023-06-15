using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource tap;
    void Start()
    {
        instance = this;
    }

    public void Tap()
    {
        tap.Play();
    }
}
