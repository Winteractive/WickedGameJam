using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullAudioProvider : IAudioService
{
    public void PlaySoundEvent(string ID, bool looping = false, float volume = 1)
    {
    }

    public void PlaySoundEvent(AudioClip clip, bool looping = false, float volume = 1)
    {
    }

    public void StopAll(string ID)
    {
    }

    public void StopSoundEvent(string ID)
    {
    }
}
