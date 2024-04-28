using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    [SerializeField] TMP_Text levelName;

    public List<string> Levels;

    int selectedLevel;

    // Start is called before the first frame update
    void Start()
    {
        levelName.text = Levels[selectedLevel];
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PreviousLevel()
    {
        if (selectedLevel > 0)
            selectedLevel--;
        else if (selectedLevel <= 0)
            selectedLevel = Levels.Count - 1;

        levelName.text = Levels[selectedLevel];
        //PlayerPrefs.SetInt("selectedLevel", selectedLevel);
    }
    public void NextLevel()
    {
        if (selectedLevel < Levels.Count - 1)
            selectedLevel++;
        else if (selectedLevel >= Levels.Count - 1)
            selectedLevel = 0;

        levelName.text = Levels[selectedLevel];
        //PlayerPrefs.SetInt("selectedLevel", selectedLevel);

    }

    public void SelectLevel()
    {
        SceneManager.LoadScene(Levels[selectedLevel]);
    }
}
