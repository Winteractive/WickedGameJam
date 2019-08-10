using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullAudioProvider : IAudioService
{
    public void PlaySoundEvent(string ID)
    {
    }

    public void PlaySoundEvent(AudioClip clip)
    {
    }

    public void StopAll(string ID)
    {
    }

    public void StopSoundEvent(string ID)
    {
    }
}
