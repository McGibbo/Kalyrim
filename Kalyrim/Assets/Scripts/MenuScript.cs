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

    Animator cameraAnimator;
    Camera menuCamera;
    Camera playCamera;
    enum SelectedPanel {play, options, none};
    SelectedPanel selectedPanel;
    enum SelectedLevel {Level1, Level2 };
    SelectedLevel selectedLevel;

    [SerializeField]Slider masterVolumeSlider;
    [SerializeField]GameObject exitButton;
    [SerializeField]GameObject playButton;
    [SerializeField]GameObject optionsButton;
    [SerializeField]GameObject optionsPanel;
    [SerializeField]GameObject playPanel;
    [SerializeField]GameObject loadingScreen;
    [SerializeField]GameObject ScreenCover;
    ScreenCoverScript screenCoverScript;

    void Start ()
    {
        optionsPanelScript = optionsPanel.GetComponent<PanelScript>();
        playPanelScript = playPanel.GetComponent<PanelScript>();
        selectedPanel = SelectedPanel.none;

        menuCamera = GameObject.FindWithTag("MenuCamera").GetComponent<Camera>();
        screenCoverScript = ScreenCover.GetComponent<ScreenCoverScript>();
        cameraAnimator = menuCamera.GetComponent<Animator>();
        EventSystem.current.SetSelectedGameObject(playButton);
	}
	
	void Update ()
    {
        if (cameraAnimator.GetCurrentAnimatorStateInfo(0).IsName("PlayCamera") && selectedLevel == SelectedLevel.Level1)
        {
            //loadingScreen.SetActive(true);
            SceneManager.LoadScene(1);
        }
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
        optionsButton.GetComponent<AudioSource>().Play();
        optionsPanel.GetComponent<AudioSource>().Play();
    }

    public void OnClickPlayButton()
    {
        playPanelScript.PanelMoveRight();
        optionsPanelScript.PanelMoveLeft();
        selectedPanel = SelectedPanel.play;
        playButton.GetComponent<AudioSource>().Play();
        
    }

    public void OnClickLevel1Button()
    {
        playButton.GetComponent<AudioSource>().Play();
        cameraAnimator.SetBool("MoveCamera", true);
        screenCoverScript.CoverScreen();
        selectedLevel = SelectedLevel.Level1;
        playButton.SetActive(false);
    }
}
