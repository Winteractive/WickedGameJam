using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAudioService
{
    void PlaySoundEvent(string ID, bool looping = false, float volume = 1);
    void PlaySoundEvent(AudioClip clip, bool looping = false, float volume = 1);
    void StopSoundEvent(string ID);
    void StopAll(string ID);
}
