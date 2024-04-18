using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] float explosionDelay;
    [SerializeField] int explosionDamage;
    [SerializeField] float explosionRadius;

    [SerializeField] GameObject explosionPrefab;


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
       
            countdown -= Time.deltaTime;
            if (countdown <= 0)
            {
                Explode();
            }
    
    }

    void Explode()
    {

        Instantiate(explosionPrefab, transform.position, transform.rotation);
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var hitCollider in hitColliders)
        {

            IDamage dmg = hitCollider.GetComponent<IDamage>();

            if (dmg != null)
            {
                dmg.TakeDamage(explosionDamage, gameObject);
                Destroy(gameObject);
            }
        }
        Destroy(gameObject);
    }
}

