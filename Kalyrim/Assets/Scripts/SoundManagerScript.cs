using UnityEngine;
using System.Collections;

[System.Serializable]
public class SoundManagerScript : MonoBehaviour {
    AudioSource AS;

	void Start ()
    {
        AS = GetComponent<AudioSource>();
        
	}
	
	void Update ()
    {
        SetVolume(AS, PlayerPrefs.GetFloat("MasterVolume") * PlayerPrefs.GetFloat("MusicVolume"));
    }

    float SetVolume(AudioSource audioSource, float volume)
    {
        audioSource.volume = volume;
        return audioSource.volume;
    }
}
