using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAudioService
{
    void PlaySoundEvent(string ID);
    void PlaySoundEvent(AudioClip clip);
    void StopSoundEvent(string ID);
    void StopAll(string ID);
}
