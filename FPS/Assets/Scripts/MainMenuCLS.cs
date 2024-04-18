using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCLS : MonoBehaviour
{
    public GameObject[] _characterArr;
    public GameObject _instantiatePoint = null;



    void Awake()
    {
        foreach (GameObject i in _characterArr)
        {
            i.GetComponent<Rigidbody>().isKinematic = false;
            i.GetComponent<Rigidbody>().useGravity = true;
        }

        if (_instantiatePoint == null)
            _instantiatePoint = GameObject.FindGameObjectWithTag("InstantiatePoint").gameObject;
    }

    void Start()
    {
        GameObject playerCharacter = Instantiate(_characterArr[PlayerPrefs.GetInt("selectedCharacterInt")], _instantiatePoint.transform.position, _instantiatePoint.transform.rotation);
        playerCharacter.name = "Player";
    }



    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {

    }
}
