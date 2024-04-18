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
    public GameObject SpawnNPC()
    {
        return Instantiate(ninjaEnemy, transform.position, Quaternion.identity).gameObject;
    }
    public GameObject SpawnPlayer()
    {
        return Instantiate(ninjaPlayer, transform.position, Quaternion.identity).gameObject;
    }
}
