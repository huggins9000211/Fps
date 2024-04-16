using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperMine : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
         Collider other = collision.collider;
        IDamage dmg = other.GetComponent<IDamage>();

        if (dmg != null)
        {
            BasePlayer hitPlayer = other.GetComponent<BasePlayer>();
            if (hitPlayer == null)
                other.GetComponent<BaseNPC>().Stun(3f);
            else
                hitPlayer.Stun(3f);
            Destroy(gameObject);
        }


    }
}
