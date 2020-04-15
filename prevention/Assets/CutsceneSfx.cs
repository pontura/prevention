using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneSfx : MonoBehaviour {

	public AudioClip[] clips;
	AudioSource source;

	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Play(int i){
		source.PlayOneShot (clips [i]);
	}

    public void Pitch(float pitch) {
        source.pitch = pitch;
    }

    public void PlayIngameMusic() {
        Events.PlayMusic("ingame");
    }

    public void PlayMusic(string name) {
        Events.PlayMusic(name);
    }

    public void PlayUISfx(string name) {
        Events.PlayUISfx(name);
    }

    public void PlaySfx(string name) {
        Events.PlaySfx(name);
    }

    public void StopMusic() {
        Events.StopMusic();
    }
}
