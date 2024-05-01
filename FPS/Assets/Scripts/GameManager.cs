using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    [Header("----- GM -----")]
    public static GameManager instance;

    [Header("----- Menus -----")]
    [SerializeField] GameObject menuActive;
    [SerializeField] GameObject menuPause;
    [SerializeField] GameObject menuWin;

    [Header("----- Spawners -----")]
    [SerializeField] AssaultSpawner assaultSpawner;
    [SerializeField] NinjaSpawner ninjaSpawner;
    [SerializeField] SniperSpawner sniperSpawner;
    [SerializeField] ReaperSpawner reaperSpawner;

    [Header("----- HUD -----")]
    public GameObject playerDamageScreen; 
    public Image hpBar;
    [SerializeField] TMP_Text AssultScoreHUD;
    [SerializeField] TMP_Text ReaperScoreHUD;
    [SerializeField] TMP_Text NinjaScoreHUD;
    [SerializeField] TMP_Text SniperScoreHUD;
    [SerializeField] TMP_Text whoWon;
    [SerializeField] int winNumber;

    [Header("----- Ammo -----")]
    public TMP_Text ammoCurText;
    public TMP_Text ammoMaxText;

    [Header("----- Player/NPCs -----")]
    public BasePlayer basePlayer;
    public AssaultPlayer assaultPlayer;
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
        playerType = PlayerPrefs.GetInt("selectedCharacterInt", 0);
        SpawnPlayers();
        basePlayer = player.GetComponent<BasePlayer>();
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
                player = SpawnAssault(true);
                assaultPlayer = player.GetComponent<AssaultPlayer>();
                npc1 = SpawnNinja(false);
                npc2 = SpawnSniper(false);
                npc3 = SpawnReaper(false);
                break;
            case 1:
                npc1 = SpawnAssault(false);
                player = SpawnNinja(true);
                npc2 = SpawnSniper(false);
                npc3 = SpawnReaper(false);
                break;
            case 2:
                npc2 = SpawnAssault(false);
                npc1 = SpawnNinja(false);
                player = SpawnSniper(true);
                npc3 = SpawnReaper(false);
                break;
            case 3:
                npc3 = SpawnAssault(false);
                npc1 = SpawnNinja(false);
                npc2 = SpawnSniper(false);
                player = SpawnReaper(true);
                break;
        }
    }
    public GameObject SpawnAssault(bool isPlayer)
    {
        if (isPlayer)
        {
           player = assaultSpawner.SpawnPlayer();
           return player;
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
            player = ninjaSpawner.SpawnPlayer();
            return player;
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
            player = sniperSpawner.SpawnPlayer();
            return player;
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
            player = reaperSpawner.SpawnPlayer();
            return player;
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
                AssultScoreHUD.text = assaultScore.ToString("F0");
                if (assaultScore >= winNumber)
                {
                    whoWon.text = "Assult Won!";
                    StatePaused();
                    menuActive = menuWin;
                    menuActive.SetActive(isPaused);
                }
                break;
            case 1:
                ninjaScore += points;
                NinjaScoreHUD.text = ninjaScore.ToString("F0");
                if (ninjaScore >= winNumber)
                {
                    whoWon.text = "Ninja Won!";
                    StatePaused();
                    menuActive = menuWin;
                    menuActive.SetActive(isPaused);
                }
                break;
            case 2:
                sniperScore += points;
                SniperScoreHUD.text = sniperScore.ToString("F0");
                if (sniperScore >= winNumber)
                {
                    whoWon.text = "Sniper Won!";
                    StatePaused();
                    menuActive = menuWin;
                    menuActive.SetActive(isPaused);
                }
                break;
            case 3:
                reaperScore += points;
                ReaperScoreHUD.text = reaperScore.ToString("F0");
                if (sniperScore >= winNumber)
                {
                    whoWon.text = "Reaper Won!";
                    StatePaused();
                    menuActive = menuWin;
                    menuActive.SetActive(isPaused);
                }
                break;
        }
    }
}
