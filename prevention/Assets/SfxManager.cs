using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SfxManager : MonoBehaviour {

    public List<SfxClip> sfxClips;
    AudioSource asource;

    [Serializable]
    public class SfxClip {
        public AudioClip clip;
        public string name;
    }


    // Start is called before the first frame update
    void Start() {
        Events.PlaySfx += PlaySfx;
        Events.PlaySfxRandom += PlaySfxRandom;
        asource = GetComponent<AudioSource>();
    }

    private void OnDestroy() {
        Events.PlaySfx -= PlaySfx;
        Events.PlaySfxRandom -= PlaySfxRandom;
    }

    // Update is called once per frame
    void Update() {

    }

    void PlaySfx(string name) {
        SfxClip sc = sfxClips.Find(x => x.name == name);
        if (sc != null) {
            asource.pitch = 1f;
            if (!asource.isPlaying)
                asource.PlayOneShot(sc.clip);
            else if (asource.clip != sc.clip)
                asource.PlayOneShot(sc.clip);
        }
    }

    void PlaySfxRandom(string name) {
        SfxClip sc = sfxClips.Find(x => x.name == name);
        if (sc != null) {
            if (!asource.isPlaying) {
                asource.pitch = 0.9f + UnityEngine.Random.value * 0.2f;
                asource.PlayOneShot(sc.clip);
            }else if (asource.clip != sc.clip) {
                asource.pitch = 0.9f + UnityEngine.Random.value * 0.2f;
                asource.PlayOneShot(sc.clip);
            }
        }
    }
}

