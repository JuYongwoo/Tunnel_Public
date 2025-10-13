using System.Collections.Generic;
using UnityEngine;

public class SoundManager
{
    private float masterVolume = 1.0f;
    private Dictionary<AudioClip, AudioSource> soundSources = new Dictionary<AudioClip, AudioSource>();


    public void PlayAudioClip(AudioClip clip, float volume, bool isLoop)
    {
        if (!soundSources.ContainsKey(clip))
        {
            soundSources.Add(clip, ManagerObject.instance.gameObject.AddComponent<AudioSource>());
        }


        soundSources[clip].volume = volume * masterVolume;
        soundSources[clip].loop = isLoop;


        soundSources[clip].Stop();
        soundSources[clip].clip = clip;
        soundSources[clip].Play();


    }
    public void StopAudioClip(AudioClip clip)
    {
        if (soundSources.ContainsKey(clip))
        {
            soundSources[clip].Stop();
        }
    }

    public void StopAllAudioClip()
    {
        foreach (var ssc in soundSources)
        {
            ssc.Value.Stop();
        }
    }

    public void UpdateVolume(float volume)
    {
        masterVolume = volume;

    }


}