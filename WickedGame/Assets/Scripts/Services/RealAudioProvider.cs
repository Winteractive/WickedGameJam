using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealAudioProvider : IAudioService
{

    private Queue<AudioSource> sources;
    GameObject audioSourceHolder;
    private List<AudioClip> soundClips;
    private Dictionary<string, AudioClip> nameToClip;
    AudioClip[] loadedClips;

    private int sourceAmount = 20;

    public RealAudioProvider()
    {
        sources = new Queue<AudioSource>();
        audioSourceHolder = new GameObject("@Audio Source Holder");
        audioSourceHolder.transform.position = Vector3.zero;
        for (int i = 0; i < sourceAmount; i++)
        {
            CreateSource();
        }

        nameToClip = new Dictionary<string, AudioClip>();

        loadedClips = Resources.LoadAll<AudioClip>("Audio/SFX");
        foreach (var clip in loadedClips)
        {
            nameToClip.Add(clip.name.ToLower(), clip);
        }
    }

    private void CreateSource()
    {
        AudioSource source = new GameObject("Source").AddComponent<AudioSource>();
        source.transform.SetParent(audioSourceHolder.transform);
        source.transform.position = Vector3.zero;
        sources.Enqueue(source);
    }

    public void PlaySoundEvent(string ID, bool looping = false)
    {
        if (nameToClip.ContainsKey(ID.ToLower()))
        {
            AudioSource selectedSource = sources.Dequeue();
            if (!looping)
            {
                sources.Enqueue(selectedSource);
            }
            else
            {
                CreateSource();
            }
            if (selectedSource.isPlaying)
            {
                selectedSource.Stop();
            }
            selectedSource.clip = nameToClip[ID.ToLower()];
            selectedSource.loop = looping;
            selectedSource.Play();
        }
        else
        {
            ServiceLocator.GetDebugProvider().Log("audioclip: " + ID + " did not exist");
        }
    }

    public void PlaySoundEvent(AudioClip clip, bool looping = false)
    {

        AudioSource selectedSource = sources.Dequeue();
        if (!looping)
        {
            sources.Enqueue(selectedSource);
        }
        else
        {
            CreateSource();
        }
        if (selectedSource.isPlaying)
        {
            selectedSource.Stop();
        }
        selectedSource.loop = looping;
        selectedSource.clip = clip;
        selectedSource.Play();
    }


    public void StopAll(string ID)
    {
        for (int i = 0; i < sourceAmount; i++)
        {
            AudioSource source = sources.Dequeue();
            source.Stop();
            sources.Enqueue(source);
        }
    }

    public void StopSoundEvent(string ID)
    {
        if (nameToClip.ContainsKey(ID.ToLower()))
        {
            AudioClip selectedClip = nameToClip[ID.ToLower()];
            for (int i = 0; i < sourceAmount; i++)
            {
                AudioSource source = sources.Dequeue();
                if (source.isPlaying)
                {
                    if (source.clip == selectedClip)
                    {
                        source.Stop();
                    }
                }
                sources.Enqueue(source);
            }
        }
        else
        {
            ServiceLocator.GetDebugProvider().Log("audioclip: " + ID + " did not exist");
        }
    }
}
