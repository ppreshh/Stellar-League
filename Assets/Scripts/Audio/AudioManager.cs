using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    private List<EventInstance> m_EventInstances = null;
    private EventInstance m_AmbientAudioEventInstance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one AudioManager in the scene.");
        }
        instance = this;

        m_EventInstances = new List<EventInstance>();
    }

    private void Start()
    {
        InitializeAmbientAudio(FMODEvents.instance.SpaceAmbience);
    }

    private void OnDestroy()
    {
        CleanUp();
    }

    public void PlayOneShot(EventReference sound)
    {
        RuntimeManager.PlayOneShot(sound);
    }

    public EventInstance CreateEventInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        m_EventInstances.Add(eventInstance);
        return eventInstance;
    }

    private void CleanUp()
    {
        foreach(EventInstance eventInstance in m_EventInstances)
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            eventInstance.release();
        }
    }

    private void InitializeAmbientAudio(EventReference ambientAudioEventReference)
    {
        m_AmbientAudioEventInstance = CreateEventInstance(ambientAudioEventReference);
        m_AmbientAudioEventInstance.start();
    }

    public void PlayLoop(bool shouldPlayLoop, EventInstance eventInstance, bool allowFadeOut = true)
    {
        if (allowFadeOut)
        {
            if (shouldPlayLoop)
            {
                PLAYBACK_STATE playbackState;
                eventInstance.getPlaybackState(out playbackState);
                if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
                {
                    eventInstance.start();
                }
                else if (playbackState.Equals(PLAYBACK_STATE.STOPPING))
                {
                    eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                    eventInstance.start();
                }
            }
            else
            {
                eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            }
        }
        else
        {
            if (shouldPlayLoop)
            {
                PLAYBACK_STATE playbackState;
                eventInstance.getPlaybackState(out playbackState);
                if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
                {
                    eventInstance.start();
                }
            }
            else
            {
                eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            }
        }
    }
}
