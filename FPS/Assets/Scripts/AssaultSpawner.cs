using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultSpawner : MonoBehaviour
{
    [SerializeField] BaseNPC assaultEnemy;
    [SerializeField] AssaultPlayer assaultPlayer;
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
        return Instantiate(assaultEnemy, transform.position, Quaternion.identity).gameObject;
    }
    public GameObject SpawnPlayer()
    {
        return Instantiate(assaultPlayer, transform.position, Quaternion.identity).gameObject;
    }
}
