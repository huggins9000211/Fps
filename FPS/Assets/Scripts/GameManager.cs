using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject Player;



    public bool isPaused;
    int enemyCount;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel") && Time.timeScale == 1) // Temp replacement // && menuActive == null)
        {
            StatePaused();
            //menuActive = menuPause;
            //menuActive.SetActive(isPaused);
        }
        else if (Input.GetButtonDown("Cancel") && Time.timeScale == 0) // Temp replacement // && menuActive == menuPause)
        {
            StateUnpaused();
        }
    }
    public void StatePaused()
    {
        isPaused = !isPaused;
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void StateUnpaused()
    {
        isPaused = !isPaused;
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        //menuActive.SetActive(isPaused);
        //menuActive = null;
    }
}
