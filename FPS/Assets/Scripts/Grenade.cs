using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] float explosionDelay;
    [SerializeField] float explosionForce;
    [SerializeField] float explosionRadius;


    float countdown;
    bool hasExploded;
    // Start is called before the first frame update
    void Start()
    {
        countdown = explosionDelay;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hasExploded == false)
        {
            countdown -= Time.deltaTime;
            if (countdown <= 0)
            {
                hasExploded = true;
            }
        }
    }

    void Explode()
    {

    }
}

