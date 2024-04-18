using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] GameObject menuActive;
    [SerializeField] GameObject menuPause;
    [SerializeField] GameObject menuWin;
    [SerializeField] GameObject menuLose;

    [SerializeField] AssaultSpawner assaultSpawner;
    [SerializeField] NinjaSpawner ninjaSpawner;
    [SerializeField] SniperSpawner sniperSpawner;
    [SerializeField] ReaperSpawner reaperSpawner;

    public static GameManager instance;

    public GameObject player;
    public GameObject npc1;
    public GameObject npc2;
    public GameObject npc3;

    int assaultScore;
    int ninjaScore;
    int sniperScore;
    int reaperScore;



    public bool isPaused;
    public int playerType = 0;
    int enemyCount;
    int spawnDoorCount;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        SpawnPlayers();
        //SetPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel") && Time.timeScale == 1) // Temp replacement // && menuActive == null)
        {
            StatePaused();
            menuActive = menuPause;
            menuActive.SetActive(isPaused);
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
        menuActive.SetActive(isPaused);
        menuActive = null;
    }
    public void SpawnDoorCount(int amount)
    {
        spawnDoorCount += amount;

        if(spawnDoorCount <= 0)
        {
            //SpawnPlayers();
        }
    }
    void SpawnPlayers()
    {
        switch (playerType)
        {
            case 0:
                SpawnAssault(true);
                SpawnNinja(false);
                SpawnSniper(false);
                SpawnReaper(false);
                break;
            case 1:
                SpawnAssault(false);
                SpawnNinja(true);
                SpawnSniper(false);
                SpawnReaper(false);
                break;
            case 2:
                SpawnAssault(false);
                SpawnNinja(false);
                SpawnSniper(true);
                SpawnReaper(false);
                break;
            case 3:
                SpawnAssault(false);
                SpawnNinja(false);
                SpawnSniper(false);
                SpawnReaper(true);
                break;
        }
    }
    public GameObject SpawnAssault(bool isPlayer)
    {
        if (isPlayer)
        {
           return assaultSpawner.SpawnPlayer();
        }
        else
        {
            return assaultSpawner.SpawnNPC();
        }
    }
    public GameObject SpawnNinja(bool isPlayer)
    {
        if (isPlayer)
        {
            return ninjaSpawner.SpawnPlayer();
        }
        else
        {
            return ninjaSpawner.SpawnNPC();
        }
    }
    public GameObject SpawnSniper(bool isPlayer)
    {
        if (isPlayer)
        {
            return sniperSpawner.SpawnPlayer();
        }
        else
        {
            return sniperSpawner.SpawnNPC();
        }
    }
    public GameObject SpawnReaper(bool isPlayer)
    {
        if (isPlayer)
        {
            return reaperSpawner.SpawnPlayer();
        }
        else
        {
            return reaperSpawner.SpawnNPC();
        }
    }
    public void AddScore(int scoreTeam, int points)
    {
        switch (scoreTeam)
        {
            case 0:
                assaultScore += points;
                break;
            case 1:
                ninjaScore += points;
                break;
            case 2:
                sniperScore += points;
                break;
            case 3:
                reaperScore += points;
                break;
        }
    }
}
