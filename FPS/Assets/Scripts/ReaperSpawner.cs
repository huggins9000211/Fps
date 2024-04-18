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
    public GameObject SpawnNPC()
    {
        return Instantiate(reaperEnemy, transform.position, Quaternion.identity).gameObject;
    }
    public GameObject SpawnPlayer()
    {
        return Instantiate(reaperPlayer, transform.position, Quaternion.identity).gameObject;
    }
}
