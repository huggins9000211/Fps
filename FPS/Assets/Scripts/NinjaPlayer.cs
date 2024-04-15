using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaPlayer : BasePlayer
{
    [SerializeField] Transform shootPos;
    [SerializeField] float shootRate;
    [SerializeField] GameObject bullet;

    bool isShooting; 
    // Start is called before the first frame update
    void Start()
    {
        base.jumpsAllowed = 2;
    }

    // Update is called once per frame
    void Update()
    {
        base.Movement();

        if (Input.GetButton("Fire1") & !isShooting)
        {
            StartCoroutine(Shoot1());
        }
        else if (Input.GetButton("Fire2") & !isShooting)
        {
            StartCoroutine(Shoot2());
        }
    }

    IEnumerator Shoot1()
    {
        isShooting = true;

        Instantiate(bullet, shootPos.position, shootPos.rotation);
        yield return new WaitForSeconds(.15f);
        Instantiate(bullet, shootPos.position, shootPos.rotation);
        yield return new WaitForSeconds(.15f);
        Instantiate(bullet, shootPos.position, shootPos.rotation);
        yield return new WaitForSeconds(shootRate);
        isShooting = false;
    }
    IEnumerator Shoot2()
    {
        isShooting = true;

        Instantiate(bullet, shootPos.position, shootPos.rotation);

        Instantiate(bullet, shootPos.position, shootPos.rotation).transform.Rotate(0, 20, 0);

        Instantiate(bullet, shootPos.position, shootPos.rotation).transform.Rotate(0, -20, 0);
        yield return new WaitForSeconds(shootRate);
        isShooting = false;
    }
}
