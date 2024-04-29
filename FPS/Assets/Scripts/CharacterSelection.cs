using System.Collections;
using System.Collections.Generic;
using TMPro;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class CharacterSelection : MonoBehaviour
{
    [SerializeField] TMP_Text typeText;
    public SelectableCharacter[] characterType;
    static int _selectedCharacterInt;
    SelectableCharacter typeDisplay = null;

    GameObject[] toDelete;


    private void Start()
    {
        PlayerPrefs.SetInt("selectedCharacterInt", _selectedCharacterInt);
        typeDisplay = characterType[_selectedCharacterInt];
        typeText.text = typeDisplay.characterType;

        toDelete = GameObject.FindGameObjectsWithTag("CharacterType");
        foreach (GameObject go in toDelete) { Destroy(go); }
        Instantiate(typeDisplay.characterModel, new Vector3(0, 0, 0), new Quaternion(0, 180, 0, 0));
    }

    public void PrevousButton()
    {
        toDelete = GameObject.FindGameObjectsWithTag("CharacterType");
        foreach (GameObject go in toDelete) { Destroy(go); }

        if (_selectedCharacterInt > 0)
            _selectedCharacterInt--;
        else if (_selectedCharacterInt <= 0)
            _selectedCharacterInt = characterType.Length - 1;


        typeDisplay = characterType[_selectedCharacterInt];
        typeText.text = typeDisplay.characterType;
        Instantiate(typeDisplay.characterModel, new Vector3(0,0,0), new Quaternion(0,180,0,0));

        PlayerPrefs.SetInt("selectedCharacterInt",_selectedCharacterInt);
    }


    public void NextButton()
    {
        toDelete = GameObject.FindGameObjectsWithTag("CharacterType");
        foreach (GameObject go in toDelete) { Destroy(go); }

        if (_selectedCharacterInt < characterType.Length) 
            _selectedCharacterInt++;

        if (_selectedCharacterInt >= characterType.Length)
            _selectedCharacterInt = 0;

        typeDisplay = characterType[_selectedCharacterInt];
        typeText.text = typeDisplay.characterType;
        Instantiate(typeDisplay.characterModel, new Vector3(0, 0, 0), new Quaternion(0, 180, 0, 0));

        PlayerPrefs.SetInt("selectedCharacterInt", _selectedCharacterInt);

    }

        
}
