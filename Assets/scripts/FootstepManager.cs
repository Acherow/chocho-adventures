using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepManager : MonoBehaviour
{
    public AudioClip stpClip;
    AudioSource footstepSource;

    bool pitch;

    private void Start()
    {
        footstepSource = GetComponentInChildren<AudioSource>();
    }

    public void Step()
    {
        footstepSource.pitch = pitch ? .5f : .6f;
        footstepSource.Play();
        pitch = !pitch;
    }
}
