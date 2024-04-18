using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultPlayer : BasePlayer
{
    // Start is called before the first frame update
    [SerializeField] Transform shootPos;
    [SerializeField] float shootRate;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject grenade;
    [SerializeField] float grenadeVelocity;
    [SerializeField] float grenadeCD;
    [SerializeField] Vector3 granadeDirection;
    bool isShooting;
    bool granadeOnCD;
    Recoil recoilScript;
    Camera mainCamera;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        base.jumpsAllowed = 1;
        recoilScript = transform.Find("Recoil").GetComponent<Recoil>();
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        mainCamera = Camera.main;
        base.Movement();

        if (Input.GetButton("Fire1") & !isShooting)
        {
            StartCoroutine(Shoot1());
        }
        else if (Input.GetButton("Fire2") & !granadeOnCD)
        {
            StartCoroutine(Shoot2());
        }
    }

    IEnumerator Shoot1()
    {
        isShooting = true;

        Instantiate(bullet, shootPos.position, shootPos.rotation);
        recoilScript.RecoilFire();
        yield return new WaitForSeconds(shootRate);
        isShooting = false;
    }
    IEnumerator Shoot2()
    {

        granadeOnCD = true;
        Vector3 spawnPosition = shootPos.position + mainCamera.transform.forward;
        GameObject spawnedGrenade = Instantiate(grenade, spawnPosition, mainCamera.transform.rotation);
        Rigidbody rb = spawnedGrenade.GetComponent<Rigidbody>();
        Vector3 finalThrowDirection = (mainCamera.transform.forward + granadeDirection).normalized;
        rb.AddForce(finalThrowDirection * grenadeVelocity, ForceMode.VelocityChange);
        yield return new WaitForSeconds(grenadeCD);
        granadeOnCD = false;
    }
}
