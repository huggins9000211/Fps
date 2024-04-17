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


    public void NewGameDialogYes()
    {
        SceneManager.LoadScene(_newgameLevel);
    }


    public void LoadGameDaillogYes()
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


}
