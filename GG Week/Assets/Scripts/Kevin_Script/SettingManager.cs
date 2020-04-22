using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SettingManager : MonoBehaviour
{
    public Toggle fullscreenToggle;
    public Dropdown resolutionDropdown;
    public Slider musiqueSlider;
    

    public Resolution[] resolution;
    public AudioSource musicSource;
    private GameSetting gameSettings;
    //public Button applyButton;

    void OnEnable()
    {
        gameSettings = new GameSetting();

        fullscreenToggle.onValueChanged.AddListener(delegate { OnFullscreenToggle(); });
        resolutionDropdown.onValueChanged.AddListener(delegate { OnResolutionChange(); });
        musiqueSlider.onValueChanged.AddListener(delegate { OnMusiqueVolumeChange(); });


        resolution = Screen.resolutions;
        foreach(Resolution resolution in resolution)
        {
            resolutionDropdown.options.Add(new Dropdown.OptionData(resolution.ToString()));
        }
        /*if (File.Exists(Application.persistentDataPath + "/gamesettings.json") == true)
        {
            LoadSettings();
        }*/
    }

    /*
    public void OnsensiMouseChange()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensi.mouseSnesy;
        float mouseY = Input.GetAxis("Mouse Y") * sensi.mouseSnesy;
    }*/

    public void OnFullscreenToggle()
    {
       gameSettings.fullscreen = Screen.fullScreen = fullscreenToggle.isOn;
    }

    public void OnResolutionChange()
    {
        Screen.SetResolution(resolution[resolutionDropdown.value].width, resolution[resolutionDropdown.value].height, Screen.fullScreen);
    }

    public void OnMusiqueVolumeChange()
    {
        musicSource.volume = gameSettings.musicVolume = musiqueSlider.value;
    }

    


    /*public void OnApplyButtonClick()
    {
        SaveSetting();
    }

    public void SaveSetting()
    {
        string jsonData = JsonUtility.ToJson(gameSettings, true);
        File.WriteAllText(Application.persistentDataPath + "/gamesettings.json", jsonData);
    }
    public void LoadSettings()
    {
        gameSettings = JsonUtility.FromJson<GameSetting>(File.ReadAllText(Application.persistentDataPath + "/gamesettings.json"));

        musiqueSlider.value = gameSettings.musicVolume;
        resolutionDropdown.value = gameSettings.idexResolution;
        fullscreenToggle.isOn = gameSettings.fullscreen;

    }*/

}
