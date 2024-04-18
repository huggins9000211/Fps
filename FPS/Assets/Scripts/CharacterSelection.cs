using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    public GameObject[] _selectableCharacter;
    static int _selectedCharacterInt = 0;
    public GameObject _selectionDisplayObj = null;

    private void Start()
    {
        foreach(GameObject i in _selectableCharacter)
        {
            i.GetComponent<Rigidbody>().useGravity = false;
        }
        InstantiateChacacter();
        PlayerPrefs.SetInt("selectedCharacterInt", 0);
    }
    void Update()
    {
        
        Debug.Log("Character Selected =" + _selectedCharacterInt);
    }

    void InstantiateChacacter()
    {
        //only destroys if child is present
       if(_selectionDisplayObj.transform.childCount > 0)
       {
            //destroy the child at pos 0
            Destroy(_selectionDisplayObj.transform.GetChild(0).transform.gameObject);
       }
       
       //create game object before instantiate
        
        GameObject playerSelection = Instantiate(_selectableCharacter[_selectedCharacterInt]);
        playerSelection.name = "Player Character selection";
        playerSelection.transform.SetParent(_selectionDisplayObj.transform);
        playerSelection.transform.localPosition = Vector3.zero;


        return;
        
    }

    public void PrevousButton()
    {
        if(_selectedCharacterInt > 0)
        _selectedCharacterInt--;
        else if (_selectedCharacterInt <= 0)
            _selectedCharacterInt = _selectableCharacter.Length - 1;

        PlayerPrefs.SetInt("selectedCharacterInt",_selectedCharacterInt);
        InstantiateChacacter();
    }


    public void NextButton()
    {
        if(_selectedCharacterInt < _selectableCharacter.Length) 
            _selectedCharacterInt++;

        if (_selectedCharacterInt >= _selectableCharacter.Length)
            _selectedCharacterInt = 0;

        PlayerPrefs.SetInt("selectedCharacterInt", _selectedCharacterInt);
        InstantiateChacacter();

    }

    public void ApplyButton()
    {
        Debug.Log(PlayerPrefs.GetInt("selectedCharacterInt"));
        SceneManager.LoadScene(1);
    }
        
}
