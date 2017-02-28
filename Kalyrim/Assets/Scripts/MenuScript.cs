using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

[System.Serializable]
public class MenuScript : MonoBehaviour {

    // Use this for initialization
    PanelScript optionsPanelScript;
    PanelScript playPanelScript;
    enum SelectedPanel {play, options, none};
    SelectedPanel selectedPanel;

    [SerializeField]Slider masterVolumeSlider;
    [SerializeField]GameObject exitButton;
    [SerializeField]GameObject playButton;

    void Start ()
    {
        optionsPanelScript = GameObject.Find("OptionsPanel").GetComponent<PanelScript>();
        playPanelScript = GameObject.Find("PlayPanel").GetComponent<PanelScript>();
        selectedPanel = SelectedPanel.none;

        EventSystem.current.SetSelectedGameObject(playButton);
	}
	
	void Update ()
    {
	    
	}

    public void OnClickExitButton()
    {
        Debug.Log("ExitGame");
        Application.Quit();
    }

    public void OnClickOptionsButton()
    {
        optionsPanelScript.PanelMoveRight();
        playPanelScript.PanelMoveLeft();
        selectedPanel = SelectedPanel.options;


        Navigation customNav = new Navigation();
        customNav.mode = Navigation.Mode.Explicit;
        customNav.selectOnDown = masterVolumeSlider;
        exitButton.GetComponent<Button>().navigation = customNav;

    }

    public void OnClickPlayButton()
    {
        playPanelScript.PanelMoveRight();
        optionsPanelScript.PanelMoveLeft();
        selectedPanel = SelectedPanel.play;
    }
}
