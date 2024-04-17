using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class LoadPrefs : MonoBehaviour
{
    public static LoadPrefs instance;

    [Header("General Setting")]
    [SerializeField] private bool canUse = false;
    [SerializeField] private MainMenuController mainMenuController;
    [SerializeField] private CharacterSelection characterSelection;


    [Header("Volume Setting")]
    [SerializeField] private TMP_Text volumeTextvalue = null;
    [SerializeField] private Slider volumeSlider = null;



    [Header("Brightness Setting")]
    [SerializeField] private Slider brightnessSlider = null;
    [SerializeField] private TMP_Text brightnessTextvalue = null;

    [Header("Quality Level Setting")]
    [SerializeField] private TMP_Dropdown qualityDropdown;

    [Header("Fullscreen Setting")]
    [SerializeField] private Toggle fullScreenToggle;

    [Header("Sensitivty  Setting")]
    [SerializeField] private TMP_Text controllerSenTextValue = null;
    [SerializeField] private Slider controllerSenSlider = null;

    [Header("Invert Y Setting")]
    [SerializeField] private Toggle invertYToggle = null;

    [Header("PlayerSelect")]
    [SerializeField] public GameObject[] _selectableCharacter;
    [SerializeField] private int _selectedCharacterInt = 0;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("masterVolume"))
        {
            float localVolume = PlayerPrefs.GetFloat("masterVolume");

            volumeTextvalue.text = localVolume.ToString("0,0");
            volumeSlider.value = localVolume;
            AudioListener.volume = localVolume;
        }
        else
        {
            mainMenuController.ResetButton("Audio");

        }

        if (PlayerPrefs.HasKey("masterQyaltiy"))
        {
            int localQuality = PlayerPrefs.GetInt("masterQuality");
            qualityDropdown.value = localQuality;
            QualitySettings.SetQualityLevel(localQuality);

        }

        if (PlayerPrefs.HasKey("masterFulllscreen"))
        {
            int localFullscreen = PlayerPrefs.GetInt("masterFulllscreen");

            if (localFullscreen == 1)
            {
                Screen.fullScreen = true;
                fullScreenToggle.isOn = true;

            }
            else
            {
                Screen.fullScreen = false;
                fullScreenToggle.isOn = false;
            }
        }


        if (PlayerPrefs.HasKey("masterBrightness"))
        {

            float localBrightness = PlayerPrefs.GetFloat("masterBrightness");

            brightnessTextvalue.text = localBrightness.ToString("0.0");
            brightnessSlider.value = localBrightness;

        }


        if (PlayerPrefs.HasKey("masterSen"))
        {
            float localSensitivity = PlayerPrefs.GetFloat("masterSen");

            controllerSenTextValue.text = localSensitivity.ToString("0");
            controllerSenSlider.value = localSensitivity;
            mainMenuController.mainControllerSen = Mathf.RoundToInt(localSensitivity);

        }


        if (PlayerPrefs.HasKey("masterInvetY"))
        {
            if (PlayerPrefs.GetInt("masterInvertY") == 1)
            {
                invertYToggle.isOn = true;
            }
            else
            {
                invertYToggle.isOn = false;
            }
        }

        if (PlayerPrefs.HasKey("selectedCharacterInt"))
        {
            _selectedCharacterInt = PlayerPrefs.GetInt("SelectedCharacterInt");
        }
        else  
            PlayerPrefs.GetInt("SelectedCharacterInt",_selectedCharacterInt);
    }


   
}
