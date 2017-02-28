using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class OptionsScript : MonoBehaviour {
    [SerializeField]Slider masterSlider;
    [SerializeField]Slider sFXSlider;
    [SerializeField]Slider musicSlider;

	void Start ()
    {
        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        sFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
	}

	void Update ()
    { 
	
	}

    public void OnMasterValueChange()
    {
        PlayerPrefs.SetFloat("MasterVolume", masterSlider.value);
    }

    public void OnSFXValueChange()
    {
        PlayerPrefs.SetFloat("SFXVolume", sFXSlider.value);
    }

    public void OnMusicValueChange()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
    }
}
