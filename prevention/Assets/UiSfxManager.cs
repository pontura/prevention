using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UiSfxManager : MonoBehaviour
{
    public List<UISfxClip> uiClips;
    AudioSource asource;

    [Serializable]
    public class UISfxClip {
        public AudioClip clip;
        public string name;
    }


    // Start is called before the first frame update
    void Start() {
        Events.PlayUISfx += PlayUISfx;
        asource = GetComponent<AudioSource>();
    }

    private void OnDestroy() {
        Events.PlayUISfx -= PlayUISfx;
    }

    // Update is called once per frame
    void Update() {

    }

    void PlayUISfx(string name) {
        UISfxClip sc = uiClips.Find(x => x.name == name);
        if (sc != null)
            asource.PlayOneShot(sc.clip);
    }
}