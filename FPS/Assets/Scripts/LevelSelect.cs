using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    [SerializeField] TMP_Text levelName;

    //public List<string> Levels;
    public List<SelectableLevel> Levels;

    SelectableLevel levelDisplay = null;
    GameObject[] toDelete;
    int selectedLevel;

    // Start is called before the first frame update
    void Start()
    {
        levelName.text = Levels[selectedLevel].levelName;
        toDelete = GameObject.FindGameObjectsWithTag("LevelDisplay");

        levelDisplay = Levels[selectedLevel];
        foreach (GameObject go in toDelete) { Destroy(go); }
        Instantiate(levelDisplay.levelModel, new Vector3(0, 0, 0), new Quaternion(-15, 180, -15, 15));
    }

    public void PreviousLevel()
    {
        if (selectedLevel > 0)
            selectedLevel--;
        else if (selectedLevel <= 0)
            selectedLevel = Levels.Count - 1;

        levelName.text = Levels[selectedLevel].levelName;
        toDelete = GameObject.FindGameObjectsWithTag("LevelDisplay");

        levelDisplay = Levels[selectedLevel];
        foreach (GameObject go in toDelete) { Destroy(go); }
        Instantiate(levelDisplay.levelModel, new Vector3(0, 0, 0), new Quaternion(-15, 180, -15, 15));
        //PlayerPrefs.SetInt("selectedLevel", selectedLevel);
    }
    public void NextLevel()
    {
        if (selectedLevel < Levels.Count - 1)
            selectedLevel++;
        else if (selectedLevel >= Levels.Count - 1)
            selectedLevel = 0;

        levelName.text = Levels[selectedLevel].levelName;
        toDelete = GameObject.FindGameObjectsWithTag("LevelDisplay");

        levelDisplay = Levels[selectedLevel];
        foreach (GameObject go in toDelete) { Destroy(go); }
        Instantiate(levelDisplay.levelModel, new Vector3(0, 0, 0), new Quaternion(-15, 180, -15, 15));
        //PlayerPrefs.SetInt("selectedLevel", selectedLevel);

    }

    public void SelectLevel()
    {
        levelDisplay = Levels[selectedLevel];
        foreach (GameObject go in toDelete) { Destroy(go); }
        SceneManager.LoadScene(Levels[selectedLevel].levelName);
    }
}
