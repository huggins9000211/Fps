using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperSpawner : MonoBehaviour
{
    [SerializeField] BaseNPC sniperEnemy;
    [SerializeField] SniperPlayer sniperPlayer;
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
        Instantiate(sniperEnemy, transform.position, Quaternion.identity);
    }
    public void SpawnPlayer()
    {
        Instantiate(sniperPlayer, transform.position, Quaternion.identity);
        GameManager.instance.SetPlayer();
    }
}
