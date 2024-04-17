using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaSpawner : MonoBehaviour
{
    [SerializeField] BaseNPC ninjaEnemy;
    [SerializeField] NinjaPlayer ninjaPlayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SpawnNPC()
    {
        Instantiate(ninjaEnemy, transform.position, Quaternion.identity);
    }
    public void SpawnPlayer()
    {
        Instantiate(ninjaPlayer, transform.position, Quaternion.identity);
        GameManager.instance.SetPlayer();
    }
}
