using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject assaultSpawner;
    [SerializeField] GameObject ninjaSpawner;
    [SerializeField] GameObject sniperSpawner;
    [SerializeField] GameObject reaperSpawner;

    public static GameManager instance;

    [SerializeField] GameObject assaultEnemy;
    [SerializeField] GameObject ninjaEnemy;
    [SerializeField] GameObject sniperEnemy;
    [SerializeField] GameObject reaperEnemy;

    public GameObject Player;



    public bool isPaused;
    int playerType;
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
    void SpawnPlayers()
    {
        switch (playerType)
        {
            case 0:
                SpawnNinja();
                SpawnSniper();
                SpawnReaper();
                break;
            case 1:
                SpawnAssault();
                SpawnSniper();
                SpawnReaper();
                break;
            case 2:
                SpawnAssault();
                SpawnNinja();
                SpawnReaper();
                break;
            case 3:
                SpawnAssault();
                SpawnNinja();
                SpawnSniper();
                break;
        }
    }
    void SpawnAssault()
    {
        Instantiate(assaultEnemy, assaultSpawner.transform.position, transform.rotation);
    }
    void SpawnNinja()
    {
        Instantiate(ninjaEnemy, assaultSpawner.transform.position, transform.rotation);
    }
    void SpawnSniper()
    {
        Instantiate(sniperEnemy, assaultSpawner.transform.position, transform.rotation);
    }
    void SpawnReaper()
    {
        Instantiate(reaperEnemy, assaultSpawner.transform.position, transform.rotation);
    }
}
