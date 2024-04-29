using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuCLS : MonoBehaviour
{
    public static MainMenuCLS instance;

    public TMP_Text characterType;

    void Awake()
    {
        instance = this;

    }



    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {

    }
}
