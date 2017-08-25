using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongPicker : MonoBehaviour {

    public AudioClip[] musics;
    AudioSource source;

	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void changeMusic(int index)
    {
        if (index < musics.Length)
        {
            source.clip = musics[index];
            source.Play();
        }

    }
}
