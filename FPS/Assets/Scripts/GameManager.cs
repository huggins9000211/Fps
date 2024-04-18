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

    public GameObject Player;

    int assaultScore;
    int ninjaScore;
    int sniperScore;
    int reaperScore;



    public bool isPaused;
    public int playerType;
    int enemyCount;
    int spawnDoorCount;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        SpawnPlayers();
        //SetPlayer();
    }
    public void SetPlayer()
    {
        Player = GameObject.FindWithTag("Player");
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
    public void SpawnAssault(bool isPlayer)
    {
        if (isPlayer)
        {
            assaultSpawner.SpawnPlayer();
        }
        else
        {
            assaultSpawner.SpawnNPC();
        }
    }
    public void SpawnNinja(bool isPlayer)
    {
        if (isPlayer)
        {
            ninjaSpawner.SpawnPlayer();
        }
        else
        {
            ninjaSpawner.SpawnNPC();
        }
    }
    public void SpawnSniper(bool isPlayer)
    {
        if (isPlayer)
        {
            sniperSpawner.SpawnPlayer();
        }
        else
        {
            sniperSpawner.SpawnNPC();
        }
    }
    public void SpawnReaper(bool isPlayer)
    {
        if (isPlayer)
        {
            reaperSpawner.SpawnPlayer();
        }
        else
        {
            reaperSpawner.SpawnNPC();
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
