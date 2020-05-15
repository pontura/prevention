using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SfxManager : MonoBehaviour {

    public List<SfxClip> sfxClips;
    AudioSource asource;

    float pitchStep = 0.0833333f;

    int maxSfxVoices = 16;

    [Serializable]
    public class SfxClip {
        public AudioClip clip;
        public string name;
    }


    // Start is called before the first frame update
    void Start() {
        Events.PlaySfx += PlaySfx;
        Events.PlaySfxRandom += PlaySfxRandom;
        Events.PlaySfxTransp += PlaySfxTransp;
        Events.PlayNextSliderSfx += PlayNextSliderSfx;
        asource = GetComponent<AudioSource>();
    }

    private void OnDestroy() {
        Events.PlaySfx -= PlaySfx;
        Events.PlaySfxRandom -= PlaySfxRandom;
        Events.PlaySfxTransp -= PlaySfxTransp;
        Events.PlayNextSliderSfx -= PlayNextSliderSfx;
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

    int count;
    void PlaySfxRandom(string name) {
        if (count < maxSfxVoices) {
            SfxClip sc = sfxClips.Find(x => x.name == name);
            if (sc != null) {
                if (!asource.isPlaying) {
                    asource.pitch = 0.9f + UnityEngine.Random.value * 0.2f;
                    asource.PlayOneShot(sc.clip, 0.5f + UnityEngine.Random.value * 0.5f);
                    count++;
                    StartCoroutine(StartMethod(sc.clip.length));
                } else if (asource.clip != sc.clip) {
                    asource.pitch = 0.9f + UnityEngine.Random.value * 0.2f;
                    asource.PlayOneShot(sc.clip, 0.5f + UnityEngine.Random.value * 0.5f);
                    count++;
                    StartCoroutine(StartMethod(sc.clip.length));
                }
            }
        }
    }

    private IEnumerator StartMethod(float clipLength) {
        yield return new WaitForSeconds(clipLength);

        count--;
    }

    void PlaySfxTransp(string name,int pitch) {
        SfxClip sc = sfxClips.Find(x => x.name == name);
        if (sc != null) {
            asource.pitch = 1f + pitch * pitchStep;
            asource.PlayOneShot(sc.clip);
        }
    }

    int sliderIndex;
    int[] sliderValues = {-2,0,2,3,5};
    void PlayNextSliderSfx() {
       // Debug.Log("Index: " + sliderIndex+" / val: " + sliderValues[sliderIndex]);
        SfxClip sc = sfxClips.Find(x => x.name == "step");
        asource.pitch = 1f + sliderValues[sliderIndex] * pitchStep;
        asource.PlayOneShot(sc.clip);
        sliderIndex++;
        if (sliderIndex >= sliderValues.Length)
            sliderIndex = 0;
    }
}

