using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickupp : MonoBehaviour
{
    [SerializeField] GunStats gun;
    // Start is called before the first frame update
    void Start()
    {
        gun.ammoCur = gun.ammoMax;
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<AssaultPlayer>() != null)
        {
            GameManager.instance.assaultPlayer.getGunStats(gun);
            Destroy(gameObject);
        }
    }
}
