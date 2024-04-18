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

    private void OnTriggerEnter(Collider other)
    {
        IDamage dmg = other.GetComponent<IDamage>();
        SniperPlayer isSniper = other.GetComponent<SniperPlayer>();
        if (dmg != null && isSniper==null)
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
