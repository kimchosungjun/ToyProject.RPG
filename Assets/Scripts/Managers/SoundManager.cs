using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager 
{
    //Audiosource = 소리의 근원지 (MP3 Player)
    //AudioClip = 음반
    //AudioListner = 귀
    AudioSource[] audioSource = new AudioSource[(int)Sound.MaxCound];
    Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();

    public void Init()
    {
        GameObject root = GameObject.Find("Sound");
        if (root == null)
        {
            root = new GameObject { name = "Sound" };
            Object.DontDestroyOnLoad(root);
            string[] soundNames = System.Enum.GetNames(typeof(Sound));
            for(int i =0; i<soundNames.Length-1; i++)
            {
                GameObject go = new GameObject { name = soundNames[i] };
                audioSource[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;
                // setparent는 recttransform에서 사용
                audioSource[(int)Sound.Bgm].loop = true;
            }
        }
    }

    public void Clear()
    {
        foreach(AudioSource source in audioSource)
        {
            source.clip = null;
            source.Stop();
        }
        audioClips.Clear();
    }

    public void Play(string path, Sound type = Sound.Effect, float pitch = 1.0f)
    {
        if (path.Contains("Audio/") == false)
            path = $"Audio/{path}";
        if (type == Sound.Bgm)
        {
            AudioClip audioClip = MasterManager.Resource.Load<AudioClip>(path);
            if (audioClip == null)
                return;
            AudioSource source = audioSource[(int)Sound.Bgm];
            if (source.isPlaying)
                source.Stop();  
            source.pitch = pitch;
            source.clip = audioClip;
            source.Play();
        }
        else
        {
            AudioClip audioClip = GetOrAddAudioClip(path);
            if (audioClip == null)
                return;
            AudioSource source = audioSource[(int)Sound.Effect];
            source.pitch = pitch;
            source.PlayOneShot(audioClip);
        }
    }
    AudioClip GetOrAddAudioClip(string path)
    {
        AudioClip audioClip = null;
        if (audioClips.TryGetValue(path, out audioClip)==false)
        {
            audioClip = MasterManager.Resource.Load<AudioClip>(path);
            audioClips.Add(path, audioClip);
        }
        return audioClip;
    }
}

