using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MusicManager : MonoBehaviour
{
    public List<MusicClip> musicClips;
    AudioSource asource;

    [Serializable]
    public class MusicClip {
        public AudioClip clip;
        public string name;
    }


    // Start is called before the first frame update
    void Start()
    {
        Events.PlayMusic += PlayMusic;
        Events.PlayMusicOnce += PlayMusicOnce;
        Events.StopMusic += StopMusic;
        asource = GetComponent<AudioSource>();
    }

    private void OnDestroy() {
        Events.PlayMusic -= PlayMusic;
        Events.StopMusic -= StopMusic;
        Events.PlayMusicOnce -= PlayMusicOnce;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayMusic(string name) {
        MusicClip mc = musicClips.Find(x => x.name == name);
        if (mc != null) {
            asource.clip = mc.clip;
            asource.Play();
        }
    }

    void PlayMusicOnce(string name) {
        MusicClip mc = musicClips.Find(x => x.name == name);
        if (mc != null) {
            asource.PlayOneShot(mc.clip);
        }
    }

    void StopMusic() {
        asource.Stop();
    }
}
