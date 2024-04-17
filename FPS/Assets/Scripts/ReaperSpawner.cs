using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperSpawner : MonoBehaviour
{
    [SerializeField] BaseNPC reaperEnemy;
    [SerializeField] ReaperPlayer reaperPlayer;
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
        Instantiate(reaperEnemy, gameObject.transform);
    }
    public void SpawnPlayer()
    {
        Instantiate(reaperPlayer, gameObject.transform);
        GameManager.instance.SetPlayer();
    }
}
