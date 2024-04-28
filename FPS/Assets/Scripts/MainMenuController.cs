using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    [Header("Level To Load")]
    public string _newgameLevel;
    private string levelToLoad;
    [SerializeField] private GameObject noSavedGameDialog = null;

    [SerializeField] private GameObject comfirmationPrompt = null;


    [Header("Volume Settings")]
    [SerializeField] public TMP_Text volumeTextValue = null;
    [SerializeField] public Slider volumeSliderValue = null;
    [SerializeField] float defualtVolume = 1.0f;

    [Header("Gameplay Settings")]
    [SerializeField] public TMP_Text controllerSenTextValue = null;
    [SerializeField] public Slider controllerSenSlider = null;
    [SerializeField] public int defaultSen = 4;
    public int mainControllerSen = 4;

    [Header("Toggle Settings")]
    [SerializeField] private Toggle invertYToggle = null;


    [Header("Graphis Settings")]
    [SerializeField] public Slider brightnessSlider = null;
    [SerializeField] public TMP_Text brightnessTextvalue = null;
    [SerializeField] public float defaultBrightness = 1f;

    [Space(10)]
    [SerializeField] private TMP_Dropdown qualityDropdown;
    [SerializeField] private Toggle fullScreenToggle;


    private int _qualityLevel;
    private bool _isFullScreen;
    private float _brightnessLevel;

    [Header("Reslution Dropdowns")]
    public TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;


    public void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {

            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }


    public void NewGameDialogYes()
    {
        SceneManager.LoadScene(_newgameLevel);
    }


    public void LoadGameDialogYes()
    {
        if (PlayerPrefs.HasKey("SavedLevel"))
        {
            levelToLoad = PlayerPrefs.GetString("SavedLevel");
            SceneManager.LoadScene(levelToLoad);
        }
        else
        {
            noSavedGameDialog.SetActive(true);
        }
    }

    public void Exitbutton()
    {
        Application.Quit();
    }


    //Volume
    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        volumeTextValue.text = volume.ToString("0.0");

    }

    public void VolumeApply()
    {
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
        StartCoroutine(ConfirmationBox());
    }


    public void SetControllerSen(float sensitivity)
    {
        mainControllerSen = Mathf.RoundToInt(sensitivity);
        controllerSenTextValue.text = sensitivity.ToString("0");
    }

    public void SetBrightness(float brightness)
    {
        _brightnessLevel = brightness;
        brightnessTextvalue.text = brightness.ToString("0.0");
    }

    public void SetFullScreen(bool isFullScreen)
    {
        _isFullScreen = isFullScreen;
    }

    public void SetQuality(int qualityIndex)
    {
        _qualityLevel = qualityIndex;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void GameplayApply()
    {
        if (invertYToggle.isOn)
        {
            PlayerPrefs.SetInt("masterInvertY", 1);
            //invertY
        }
        else
        {
            PlayerPrefs.SetInt("masterInvertY", 0);
            //no Invert
        }

        PlayerPrefs.SetFloat("masterSen", mainControllerSen);
        StartCoroutine(ConfirmationBox());
    }

    public void GraphicsApply()
    {
        PlayerPrefs.SetFloat("masterBrightness", _brightnessLevel);
        //change bright with post processing/or what eva it is

        PlayerPrefs.SetInt("masterQuality", _qualityLevel);
        QualitySettings.SetQualityLevel(_qualityLevel);

        PlayerPrefs.SetInt("masterFulllscreen", (_isFullScreen ? 1 : 0));
        Screen.fullScreen = _isFullScreen;

        StartCoroutine(ConfirmationBox());
    }


    public void ResetButton(string MenuType)
    {
        if (MenuType == "Audio")
        {
            AudioListener.volume = defualtVolume;
            volumeSliderValue.value = defualtVolume;
            volumeTextValue.text = defualtVolume.ToString("0.0");
            VolumeApply();
        }

        if (MenuType == "Gameplay")
        {
            controllerSenTextValue.text = defaultSen.ToString("0");
            controllerSenSlider.value = defaultSen;
            mainControllerSen = defaultSen;
            invertYToggle.isOn = false;
            GameplayApply();
        }

        if (MenuType == "Graphics")
        {
            brightnessSlider.value = defaultBrightness;
            brightnessTextvalue.text = defaultBrightness.ToString("0.0");

            qualityDropdown.value = 1;
            QualitySettings.SetQualityLevel(1);

            fullScreenToggle.isOn = false;
            Screen.fullScreen = false;

            Resolution currentResolution = Screen.currentResolution;
            Screen.SetResolution(currentResolution.width, currentResolution.height, Screen.fullScreen);
            resolutionDropdown.value = resolutions.Length;
            GraphicsApply();

        }

    }



    public IEnumerator ConfirmationBox()
    {
        comfirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(2);
        comfirmationPrompt.SetActive(false);



    }

}
