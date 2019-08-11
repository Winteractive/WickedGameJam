using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAudioService
{
    void PlaySoundEvent(string ID, bool looping = false);
    void PlaySoundEvent(AudioClip clip, bool looping = false);
    void StopSoundEvent(string ID);
    void StopAll(string ID);
}
