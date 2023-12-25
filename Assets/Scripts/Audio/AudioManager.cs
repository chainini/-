using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("ÊÂ¼þ¼àÌý")]
    public PlayAudioEventSO FXEvent;
    public PlayAudioEventSO BGMEvent;


    public AudioSource BGMAudio;
    public AudioSource FXAudio;

    private void OnEnable()
    {
        FXEvent.OnEventRaised += OnFXEvent;
        BGMEvent.OnEventRaised += OnBGMEvent;
    }
    private void OnDisable()
    {
        FXEvent.OnEventRaised -= OnFXEvent;
        BGMEvent.OnEventRaised -= OnBGMEvent;
    }

    private void OnBGMEvent(AudioClip audioClip)
    {
        BGMAudio.clip = audioClip;
        BGMAudio.Play();
    }

    private void OnFXEvent(AudioClip audioClip)
    {
        FXAudio.clip = audioClip;
        FXAudio.Play();
    }
}
